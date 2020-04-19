using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication14.Models;

namespace WebApplication14.Controllers
{
    public class ProjeOnerilerimController : Controller
    {
        private readonly AraProjeContext _context;

        public ProjeOnerilerimController(AraProjeContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Akademisyen")]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("akademisyen") == null)
            {
                return RedirectToAction("Logout", "Login");
            }

            int id = Convert.ToInt32(HttpContext.Session.GetInt32("id"));
            AkademikPersonel a = _context.AkademikPersonel.Find(id);

            Takvim takvim = _context.Takvim.FirstOrDefault(x => x.Id == 1);
            ViewBag.takvim = takvim;
            if (IsForm2ExpiredDate())//tarih geçtiyse
            {
                List<ProjeAl> kabulEdilenProjeler = _context.ProjeAl.Where(x => x.ProjeNoNavigation.DanismanId == id && x.KabulDurumu == "Kabul").ToList();
                List<ProjeAl> kabulEdilenOgrProjeleri = _context.ProjeAl.Where(x => x.OgrenciOneriNoNavigation.Danismanid == id && x.KabulDurumu == "Kabul").ToList();
                foreach (ProjeAl proje in kabulEdilenOgrProjeleri)
                {
                    kabulEdilenProjeler.Add(proje);
                }

                ViewBag.kabulEdilenProjeler = kabulEdilenProjeler;
                ViewBag.geç = 1;
                return View();
            }
            else
            {
                List<ProjeOnerileri> projeOnerilerimList = _context.ProjeOnerileri.Where(x => x.DanismanId == id).ToList();

                List<int> atananProjeSayisi = new List<int>();
                foreach (ProjeOnerileri proje in projeOnerilerimList)
                {
                    atananProjeSayisi.Add(_context.ProjeAl.Where(x => x.ProjeNo == proje.Id).Count());
                }
                List<int> istekGonderilmisMi = new List<int>();
                foreach (ProjeOnerileri proje in projeOnerilerimList)
                {
                    Istek istek = _context.Istek.FirstOrDefault(x => x.ProjeId == proje.Id);
                    if (istek != null)
                        istekGonderilmisMi.Add(1);
                    else
                        istekGonderilmisMi.Add(0);
                }
                ViewBag.istekGonderilmisMi = istekGonderilmisMi;
                ViewBag.AtamaSayısı = atananProjeSayisi.ToList();
                ViewBag.DanismaniOldugumProjeler = _context.ProjeAl.Where(x => x.ProjeNoNavigation.DanismanId == id || x.OgrenciOneriNoNavigation.Danismanid == id).ToList();
                var projeOnerilerim = projeOnerilerimList;//.OrderBy(x => x.OturumNo)
                return View(projeOnerilerim);
            }
        }

        public IActionResult AtananProjeSil()
        {
            int id = 1;
            try
            {
                id = Convert.ToInt32(HttpContext.Request.Query["id"].ToString());
            }
            catch (NullReferenceException nre)
            {
                return RedirectToAction("Index", "Home");
            }
            ProjeAl alınanProje = _context.ProjeAl.FirstOrDefault(x => x.Id == id);
            if (alınanProje != null)
                _context.ProjeAl.Remove(alınanProje);
            _context.SaveChanges();

            return RedirectToAction("Index", "ProjeOnerilerim");
        }

        public IActionResult ProjeEkle()
        {
            if (HttpContext.Session.GetInt32("akademisyen") == null)//akademisyen giriş yapmadıysa
            {
                return RedirectToAction("Logout", "Login");
            }

            if (IsForm2ExpiredDate())//Form2 teslim tarihinden önce proje eklenmeli
                return RedirectToAction("Logout", "Login");

            ViewBag.oturumlar = _context.AlanOturum.ToList();
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> ProjeEkleAsync(ProjeOnerileri proje, IFormFile Form1, String Kategori)
        {
            int DanısmanId = Convert.ToInt32(HttpContext.Session.GetInt32("id"));
            proje.DanismanId = DanısmanId;

            AkademikPersonel akademisyen = _context.AkademikPersonel.FirstOrDefault(x => x.Id == DanısmanId);

            if (Form1.Length > 0)
            {
                // full path to file in temp location
                var path = Path.Combine("AkademikPersonel", akademisyen.Kisaltma, proje.Isim + ".pdf");
                var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await Form1.CopyToAsync(stream);
                }
                proje.Form1 = path;
            }

            proje.Kategori = Kategori;

            //akademisyenin eklediği proje direkt eski projeye akatarılır
            EskiKabulGorenProjeler eskiProje = new EskiKabulGorenProjeler();
            eskiProje.Isim = proje.Isim;
            eskiProje.DanismanId = proje.DanismanId;
            eskiProje.GrupSayisi = proje.GrupSayisi;
            eskiProje.KisiSayisi = proje.KisiSayisi;
            eskiProje.OturumNo = proje.OturumNo;
            eskiProje.Turu = proje.Kategori;
            eskiProje.Form1 = proje.Form1;

            _context.EskiKabulGorenProjeler.Add(eskiProje);

            _context.Add(proje);
            _context.SaveChanges();

            return RedirectToAction("Index", "ProjeOnerilerim");
        }
        
        public IActionResult EskiOnerilerim()
        {
            if (HttpContext.Session.GetInt32("akademisyen") == null)
            {
                return RedirectToAction("Logout", "Login");
            }
            else if (IsForm2ExpiredDate())
            {
                return RedirectToAction("Logout", "Login");
            }

            int id = Convert.ToInt32(HttpContext.Session.GetInt32("id"));//giriş yapan akademisyenin id'si
            ViewBag.eskiOnerilerim = _context.EskiKabulGorenProjeler.Where(x => x.DanismanId == id).ToList();
            return View();
        }
        
        [HttpPost]
        public IActionResult EskiOnerilerim(int id)
        {
            EskiKabulGorenProjeler eskiKabulOlanProje = _context.EskiKabulGorenProjeler.FirstOrDefault(x => x.Id == id);

            //Proje Önerilerine, eski proje önerisini ekle
            ProjeOnerileri proje = new ProjeOnerileri();
            proje.Isim = eskiKabulOlanProje.Isim;
            proje.DanismanId = eskiKabulOlanProje.DanismanId;
            proje.OturumNo = eskiKabulOlanProje.OturumNo;
            proje.KisiSayisi = eskiKabulOlanProje.KisiSayisi;
            proje.GrupSayisi = eskiKabulOlanProje.GrupSayisi;
            proje.Kategori = eskiKabulOlanProje.Turu;
            proje.Form1 = eskiKabulOlanProje.Form1;

            _context.ProjeOnerileri.Add(proje);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Akademisyen")]
        public IActionResult ProjeDuzenle()
        {
            if (HttpContext.Session.GetInt32("akademisyen") == null)
            {
                return RedirectToAction("Logout", "Login");
            }

            if (IsForm2ExpiredDate())//Form2 Teslim Tarihinden önce proje düzenlenmeli
                return RedirectToAction("Logout", "Login");

            int id = 1;
            try
            {
                id = Convert.ToInt32(HttpContext.Request.Query["id"].ToString());
            }
            catch (NullReferenceException nre)
            {
                return RedirectToAction("Index", "Home");
            }

            ProjeOnerileri proje = _context.ProjeOnerileri.FirstOrDefault(x => x.Id == id);
            if (proje == null)//böyle bir proje veritabanında bulunmadı
                return RedirectToAction("Index");
            else if (proje.DanismanId != HttpContext.Session.GetInt32("id"))
                return RedirectToAction("Logout", "Login");

            ViewBag.Oturumlar = _context.AlanOturum.ToList();

            HttpContext.Session.SetInt32("ProjeID", id);
            return View(proje);
        }
        
        [HttpPost]
        public async Task<IActionResult> ProjeDuzenleAsync(ProjeOnerileri proje, IFormFile Form1, String Kategori)
        {
            int idAkademisyen = Convert.ToInt32(HttpContext.Session.GetInt32("id"));
            AkademikPersonel a = _context.AkademikPersonel.Find(idAkademisyen);

            int id = Convert.ToInt32(HttpContext.Session.GetInt32("ProjeID"));
            ProjeOnerileri project = _context.ProjeOnerileri.FirstOrDefault(x => x.Id == id);
            if (proje.Isim != null)
                project.Isim = proje.Isim;
            if (proje.OturumNo != null)
                project.OturumNo = proje.OturumNo;
            if (Kategori != null)
                project.Kategori = Kategori;
            if (proje.KisiSayisi != null)
                project.KisiSayisi = proje.KisiSayisi;
            if (proje.GrupSayisi != null)
                project.GrupSayisi = proje.GrupSayisi;
            if (Form1 != null)
            {
                if (Form1.Length > 0)
                {
                    // full path to file in temp location
                    //proje.isim null ise isimlendirmeyi yanlış yapıyor
                    var path = Path.Combine("AkademikPersonel", a.Kisaltma, project.Isim + ".pdf");
                    var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await Form1.CopyToAsync(stream);
                    }
                    project.Form1 = path;
                }
            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult ProjeAta()
        {
            if (HttpContext.Session.GetInt32("akademisyen") == null)
            {
                return RedirectToAction("Logout", "Login");
            }
            //Form2 teslim tarihinden önce proje ataması yapılır
            if (IsForm2ExpiredDate())
                return RedirectToAction("Logout", "Login");

            int id = 1;
            try
            {
                id = Convert.ToInt32(HttpContext.Request.Query["id"].ToString());
            }
            catch (NullReferenceException nre)
            {
                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Login");
            }
            int Danismanid = Convert.ToInt32(HttpContext.Session.GetInt32("id"));
            ProjeOnerileri proje = _context.ProjeOnerileri.FirstOrDefault(x => x.Id == id);
            if (proje == null)//böyle bir proje veritabanında bulunmadı
                return RedirectToAction("Index");
            else if (proje.DanismanId != Danismanid)//kendi projesinden başka projelere erişirse
                return RedirectToAction("Logout", "Login");

            Istek Gonderilenistek = _context.Istek.FirstOrDefault(x => x.ProjeId == id);
            if (Gonderilenistek == null)//istek gönderilmediği halde sayfaya eriştiyse
            {
                return RedirectToAction("Index", "ProjeOnerilerim");
            }





            List<ProjeAl> kaçKezAlınmış = _context.ProjeAl.Where(x => x.ProjeNo == id).ToList();
            if (kaçKezAlınmış.Count == proje.GrupSayisi)
                return RedirectToAction("Index");

            HttpContext.Session.SetInt32("AtamaProjeID", id);

            //bu projeye istek gönderen öğrenciler
            int idAtama = Convert.ToInt32(HttpContext.Session.GetInt32("AtamaProjeID"));

            List<Istek> istekgonderenler = _context.Istek.Where(x => x.ProjeId == idAtama).ToList();
            List<Ogrenci> öğrenciler = new List<Ogrenci>();//tek kişilik projeler için
            List<int> istekIds = new List<int>();
            List<String> grupBilgileri = new List<String>();//2 kişilik projeler için

            foreach (Istek istek in istekgonderenler)
            {
                Ogrenci öğrenci = _context.Ogrenci.FirstOrDefault(x => x.OgrenciNo == istek.OgrNo1);//öğrenci nesnesini göndermek istiyoruz view'a
                öğrenciler.Add(öğrenci);

                if (istek.OgrNo2 != null)//gönderilen istekte eğer öğrNo2 mevcutsa
                {
                    Ogrenci öğrenci2 = _context.Ogrenci.FirstOrDefault(x => x.OgrenciNo == istek.OgrNo2);
                    String s = öğrenci.OgrenciNo + " " + öğrenci.Ad + " " + öğrenci.Soyad + " --- " +
                                öğrenci2.OgrenciNo + " " + öğrenci2.Ad + " " + öğrenci2.Soyad;
                    istekIds.Add(istek.Id);
                    grupBilgileri.Add(s);
                }
            }

            ViewBag.buProjeyeİstekGönderenler = istekgonderenler;
            ViewBag.öğrenciler = öğrenciler;
            ViewBag.istekIds = istekIds;
            ViewBag.gruplar = grupBilgileri;
            return View(proje);
        }
        
        [HttpPost]
        public IActionResult ProjeAta(int istekId)
        {
            int id = Convert.ToInt32(HttpContext.Session.GetInt32("AtamaProjeID"));

            ProjeAl alınan_proje = new ProjeAl();//tabloya insert yapılacak, yeni nesne üret

            alınan_proje.ProjeNo = id;

            Istek atamasıYapılacakIstek = _context.Istek.FirstOrDefault(x => x.Id == istekId);
            //proje ataması yapılan öğrencilerin, diğer istekleri silinmeli(bir öğrenci en fazla bir proje alabilir)
            if (atamasıYapılacakIstek.OgrNo2 == null)//bu tek kişilik bir proje 
            {
                alınan_proje.OgrNo1 = atamasıYapılacakIstek.OgrNo1;

                //proje ataması yapılan öğrencinin, diğer istekleri silinmeli(bir öğrenci en fazla bir proje alabilir)
                List<Istek> digerIstekler = _context.Istek.Where(x => x.OgrNo1 == atamasıYapılacakIstek.OgrNo1 || x.OgrNo2 == atamasıYapılacakIstek.OgrNo1).ToList();
                foreach (Istek istek in digerIstekler)
                {
                    _context.Istek.Remove(istek);
                }
            }
            else
            { //bu iki kişilik bir proje
                alınan_proje.OgrNo1 = atamasıYapılacakIstek.OgrNo1;
                alınan_proje.OgrNo2 = atamasıYapılacakIstek.OgrNo2;

                List<Istek> digerIstekler = _context.Istek.Where(x => x.OgrNo1 == atamasıYapılacakIstek.OgrNo1 || x.OgrNo2 == atamasıYapılacakIstek.OgrNo1).ToList();
                foreach (Istek istek1 in digerIstekler)
                {
                    _context.Istek.Remove(istek1);
                }
                digerIstekler = _context.Istek.Where(x => x.OgrNo1 == atamasıYapılacakIstek.OgrNo2 || x.OgrNo2 == atamasıYapılacakIstek.OgrNo2).ToList();
                foreach (Istek istek1 in digerIstekler)
                {
                    _context.Istek.Remove(istek1);
                }

                _context.Remove(atamasıYapılacakIstek);//bu arkadaşlar artık bir projeye atandılar isteklerini sil
            }


            alınan_proje.ProjeDurumu = "Yeni Proje (Akademisyen Önerisinden)";//bu metodu akademisyen sadece projesini atamak için kullanır
            alınan_proje.Form2 = atamasıYapılacakIstek.Form2;
            _context.ProjeAl.Add(alınan_proje);
            _context.SaveChanges();

            //bir projenin kontenjanı dolduysa, o projeye dair istekleri sil
            int count = _context.ProjeAl.Where(x => x.ProjeNo == id).Count();
            ProjeOnerileri projeOnerisi = _context.ProjeOnerileri.FirstOrDefault(x => x.Id == id);
            if (count == projeOnerisi.GrupSayisi)
            {
                List<Istek> digerIstekler = _context.Istek.Where(x => x.ProjeId == id).ToList();
                foreach (Istek istek1 in digerIstekler)
                {
                    _context.Istek.Remove(istek1);
                }
            }
            _context.SaveChanges();

            return RedirectToAction("Index", "ProjeOnerilerim");
        }

        //***********************************
        //Actionlarda kullanılan fonksiyonlar
        public List<Ogrenci> IstekGonderenOgrenciler()
        {
            int id = Convert.ToInt32(HttpContext.Session.GetInt32("AtamaProjeID"));

            List<Istek> istekgonderenler = _context.Istek.Where(x => x.ProjeId == id).ToList();
            List<Ogrenci> öğrenciler1 = new List<Ogrenci>();
            List<Ogrenci> öğrenciler2 = new List<Ogrenci>();

            foreach (Istek istek in istekgonderenler)
            {
                Ogrenci öğrenci = _context.Ogrenci.FirstOrDefault(x => x.OgrenciNo == istek.OgrNo1);//öğrenci nesnesini göndermek istiyoruz view'a
                öğrenciler1.Add(öğrenci);
                Ogrenci öğrenci2 = _context.Ogrenci.FirstOrDefault(x => x.OgrenciNo == istek.OgrNo2);
                öğrenciler2.Add(öğrenci2);
            }

            return öğrenciler1;
        }
        
        public Boolean IsForm2ExpiredDate()
        {
            Takvim takvim = _context.Takvim.FirstOrDefault(x => x.Id == 1);
            if (DateTime.Today > takvim.Form2)
                return true;
            return false;
        }

        /*public Boolean BuProjeKendisininMi(ProjeOnerileri projem) {
			int Danismanid = Convert.ToInt32(HttpContext.Session.GetInt32("id"));

			ProjeOnerileri proje = projeDb.ProjeOnerileri.FirstOrDefault(x => x.Id == projem.Id);
			if (proje.DanismanId != Danismanid)//kendi projesinden başka projelere erişirse
				return false;
			return true;
		} */
    }
}