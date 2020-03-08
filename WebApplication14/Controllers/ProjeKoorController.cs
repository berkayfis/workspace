using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication14.Models;

namespace WebApplication14.Controllers
{
    public class ProjeKoorController : Controller
    {
        AraProjeContext p = new AraProjeContext();

        public IActionResult Index()
        {
            ViewBag.öğrenciOnerileri = p.OgrenciProjeOnerisi.ToList();
            return View();

        }
        public IActionResult Onayla(int id)
        {
            ProjeAl proje = new ProjeAl();
            proje.OgrenciOneriNo = id;
            proje.ProjeDurumu = "Yeni Proje (Öğrenci Önerisinden)";

            p.ProjeAl.Add(proje);
            p.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult DuyuruYayınla()
        {
            if (HttpContext.Session.GetInt32("koordinatör") == null)
                return RedirectToAction("Logout", "Login");

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> DuyuruYayınlaAsync(Duyuru duyuru, IFormFile Eklenti)
        {
            String filePath = "";
            if (Eklenti != null)
            {
                if (Eklenti.Length > 0)
                {
                    // full path to file in temp location
                    filePath = "C:\\Users\\FSA\\source\\repos\\demo\\WebApplication14\\wwwroot\\disc\\" + duyuru.Baslik + ".pdf";
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await Eklenti.CopyToAsync(stream);
                    }
                }
                duyuru.Eklenti = filePath;
            }

            duyuru.Zaman = DateTime.Now;
            int id = Convert.ToInt32(HttpContext.Session.GetInt32("id"));
            ProjeKoordinatoru koor = p.ProjeKoordinatoru.FirstOrDefault(x => x.AkademisyenId == id);
            duyuru.KoordinatorNo = koor.Id;
            p.Duyuru.Add(duyuru);
            p.SaveChanges();
            return RedirectToAction("Duyurular");
        }
        public IActionResult DuyuruDüzenle(int id)
        {
            if (HttpContext.Session.GetInt32("koordinatör") == null)
                return RedirectToAction("Logout", "Login");

            Duyuru duyuru = p.Duyuru.FirstOrDefault(x => x.Id == id);
            if (duyuru != null)
            {
                if (duyuru.KoordinatorNo != HttpContext.Session.GetInt32("koordinatör"))
                    return RedirectToAction("Logout", "Login");

                HttpContext.Session.SetInt32("DuyuruID", id);
                return View(duyuru);
            }
            return RedirectToAction("YayınladığımDuyurular", "ProjeKoor");
        }
        [HttpPost]
        public async Task<IActionResult> DuyuruDüzenleAsync(Duyuru duyuru, IFormFile Eklenti)
        {
            int id = Convert.ToInt32(HttpContext.Session.GetInt32("DuyuruID"));
            Duyuru duyuruDb = p.Duyuru.FirstOrDefault(x => x.Id == id);

            if (duyuru.Baslik != null && !duyuruDb.Baslik.Contains("[Düzenlendi]"))
            {
                duyuruDb.Baslik = duyuru.Baslik + " [Düzenlendi]";
            }
            if (duyuru.Icerik != null)
                duyuruDb.Icerik = duyuru.Icerik;
            if (duyuru.Eklenti != null)
                duyuruDb.Eklenti = duyuru.Eklenti;
            duyuruDb.Zaman = DateTime.Now;
            if (Eklenti != null)
            {
                String filePath = "";
                if (Eklenti.Length > 0)
                {
                    // full path to file in temp location
                    filePath = "C:\\Users\\FSA\\source\\repos\\demo\\WebApplication14\\wwwroot\\disc\\" + duyuru.Baslik + ".pdf";

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await Eklenti.CopyToAsync(stream);
                    }
                    duyuruDb.Eklenti = filePath;
                }
            }
            p.SaveChanges();

            return RedirectToAction("Duyurular", "ProjeKoor");
        }
        public IActionResult Duyurular()
        {
            ViewBag.Duyurular = p.Duyuru.ToList();
            return View();
        }
        public IActionResult YayınladığımDuyurular()
        {
            if (HttpContext.Session.GetInt32("koordinatör") == null)
                return RedirectToAction("Logout", "Login");

            int id = Convert.ToInt32(HttpContext.Session.GetInt32("koordinatör"));
            ViewBag.YayınladığımDuyurular = p.Duyuru.Where(x => x.KoordinatorNo == id).ToList();
            return View();
        }

        public IActionResult DuyuruGörüntüle(int id)
        {
            Duyuru duyuru = p.Duyuru.FirstOrDefault(x => x.Id == id);
            return View(duyuru);
        }

        public IActionResult DersiAlanOgrenciler()
        {
            String filePath = "C:\\Users\\FSA\\source\\repos\\demo\\WebApplication14\\wwwroot\\disc\\" + HttpContext.Session.GetString("BulunduğumuzDönem");

            FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            IExcelDataReader excelReader;
            List<string> liste = new List<string>();

            if (Path.GetExtension(filePath).ToUpper() == ".XLS")
            {
                //Reading from a binary Excel file ('97-2003 format; *.xls)
                excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
            }
            else
            {
                //Reading from a OpenXml Excel file (2007 format; *.xlsx)
                excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            }

            DataSet result = excelReader.AsDataSet();

            while (excelReader.Read())
            {
                liste.Add(excelReader.GetValue(0).ToString() + " " + excelReader.GetString(1) + " " + excelReader.GetString(2));
                Ogrenci ogrenci = new Ogrenci();
                ogrenci.OgrenciNo = excelReader.GetValue(0).ToString();
                Ogrenci ogrenci1 = p.Ogrenci.FirstOrDefault(x => x.OgrenciNo == ogrenci.OgrenciNo);
                if (ogrenci1 == null)
                {
                    ogrenci.Ad = excelReader.GetString(1);
                    ogrenci.Soyad = excelReader.GetString(2);
                    p.Ogrenci.Add(ogrenci);
                }
            }
            p.SaveChanges();

            ViewBag.alanlar = liste;

            //Okuma bitiriliyor.
            excelReader.Close();
            return View();
        }
        public IActionResult Kabul(int id)
        {
            if (HttpContext.Session.GetInt32("koordinatör") == null)//Proje kurulunun kararını sadece koordinatör atayabilir
                return RedirectToAction("Logout", "Login");

            Takvim takvim = p.Takvim.FirstOrDefault(x => x.Id == 1);
            if (takvim.Toplanti == DateTime.Today)
            {
                ProjeAl alınanProje = p.ProjeAl.FirstOrDefault(x => x.Id == id);
                if (alınanProje != null)
                {
                    alınanProje.KabulDurumu = "Kabul";
                }
                p.SaveChanges();
            }
            else
            { //Proje kurulunun kararı toplantı gününün haricinde atanmaz
                return RedirectToAction("Logout", "Login");
            }


            return RedirectToAction("Index", "AtamasıYapılanProjeler");
        }
        public IActionResult Ret(int id)
        {
            if (HttpContext.Session.GetInt32("koordinatör") == null)//Proje kurulunun kararını sadece koordinatör atayabilir
                return RedirectToAction("Logout", "Login");

            Takvim takvim = p.Takvim.FirstOrDefault(x => x.Id == 1);
            if (takvim.Toplanti == DateTime.Today)
            {
                ProjeAl alınanProje = p.ProjeAl.FirstOrDefault(x => x.Id == id);
                if (alınanProje != null)
                {
                    alınanProje.KabulDurumu = "Ret";
                }
                p.SaveChanges();
            }
            else
            { //Proje kurulunun kararı toplantı gününün haricinde atanmaz
                return RedirectToAction("Logout", "Login");
            }

            return RedirectToAction("Index", "AtamasıYapılanProjeler");
        }
        public IActionResult Undo(int id)
        {
            if (HttpContext.Session.GetInt32("koordinatör") == null)//Proje kurulunun kararını sadece koordinatör atayabilir
                return RedirectToAction("Logout", "Login");

            Takvim takvim = p.Takvim.FirstOrDefault(x => x.Id == 1);
            if (takvim.Toplanti == DateTime.Today)
            {
                ProjeAl alınanProje = p.ProjeAl.FirstOrDefault(x => x.Id == id);
                if (alınanProje != null)
                {
                    alınanProje.KabulDurumu = null;
                }
                p.SaveChanges();
            }
            else
            { //Proje kurulunun kararı toplantı gününün haricinde atanmaz
                return RedirectToAction("Logout", "Login");
            }

            return RedirectToAction("Index", "AtamasıYapılanProjeler");
        }

        public IActionResult TakvimEkle()
        {
            if (HttpContext.Session.GetInt32("koordinatör") == null)
                return RedirectToAction("Logout", "Login");

            return View();
        }
        [HttpPost]
        public IActionResult TakvimEkle(Takvim takvim)
        {
            p.Takvim.Add(takvim);
            p.SaveChanges();

            return RedirectToAction("Index", "ProjeKoor");
        }
        public IActionResult DonemBaslat()
        {
            //proje koordinatörünün işini kolaylaştırmak istersek

            /*int yıl = Convert.ToInt32(DateTime.Today.Year.ToString());
			int month = Convert.ToInt32(DateTime.Now.Month.ToString());
			if (month < 3)
			{
				int yıl2 = yıl + 1;
				ViewBag.donem = yıl + "" + yıl2 + " Bahar";
				return View();
			}
			if (month < 11) { 
				ViewBag.s = month;
			}*/
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> DonemBaslatAsync(String Dönem, IFormFile öğrenciler)
        {
            String fileName = öğrenciler.FileName.ToString();
            String fileExtension = fileName.Substring(fileName.LastIndexOf("."));
            if (öğrenciler.Length > 0)
            {
                // full path to file in temp location
                var filePath = "C:\\Users\\FSA\\source\\repos\\demo\\WebApplication14\\wwwroot\\disc\\" + Dönem + fileExtension;

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await öğrenciler.CopyToAsync(stream);
                }
            }

            HttpContext.Session.SetString("BulunduğumuzDönem", Dönem + fileExtension);
            return RedirectToAction("DersiAlanOgrenciler");
        }
        public IActionResult RaporlarıDagit()
        {
            if (HttpContext.Session.GetInt32("koordinatör") == null)
                return RedirectToAction("Logout", "Login");

            Takvim t = p.Takvim.FirstOrDefault(x => x.Id == 1);
            List<ProjeAl> alınanProjeler = p.ProjeAl.ToList();
            if (DateTime.Today > t.Ararapor1 && DateTime.Today < t.Ararapor2)
            { //Ara Rapor 1'in incelenme süresi
                ViewBag.AraRapor1 = alınanProjeler;
            }
            else if (DateTime.Today > t.Ararapor2 && DateTime.Today < t.Finalrapor)
            {//Ara Rapor 2'nin incelenme süresi
                ViewBag.AraRapor2 = alınanProjeler;
            }

            return View();
        }
        public IActionResult RaporlarRandomDagit()
        {
            List<ProjeAl> projeler = p.ProjeAl.ToList();
            foreach (ProjeAl proje in projeler)
            {
                proje.AsistanId = null;
            }


            Takvim t = p.Takvim.FirstOrDefault(x => x.Id == 1);
            if (DateTime.Today > t.Ararapor1 && DateTime.Today < t.Ararapor2)
            { //Ara Rapor 1'in incelenme süresi
                RaporlariRandomDagit(1);
            }
            else if (DateTime.Today > t.Ararapor2 && DateTime.Today < t.Finalrapor)
            {//Ara Rapor 2'nin incelenme süresi
                RaporlariRandomDagit(2);
            }

            return RedirectToAction("RaporlarıDagit", "ProjeKoor");
        }
        public IActionResult Modal()
        {
            return View();
        }

        public void RaporlariRandomDagit(int n)
        {
            List<ProjeAl> projeler = new List<ProjeAl>();
            if (n == 1)
            {
                projeler = p.ProjeAl.Where(x => x.Ararapor1 != null).ToList();
            }
            else if (n == 2)
            {
                projeler = p.ProjeAl.Where(x => x.Ararapor2 != null).ToList();
            }
            List<AkademikPersonel> resAssist = p.AkademikPersonel.Where(x => x.Unvan == "Arş. Grv.").ToList();
            int[,] isYuku = new int[2, resAssist.Count];
            int i = 0;
            foreach (AkademikPersonel akademisyen in resAssist)
            {
                isYuku[i, 0] = akademisyen.Id;
                isYuku[i, 1] = 0;
            }

            var rand = new Random();
            foreach (ProjeAl proje in projeler)
            {
                int x = rand.Next(resAssist.Count);
                int randomAkademisyenId = resAssist[x].Id;
                proje.AsistanId = randomAkademisyenId;
                i = 0;
                int flag = 0;
                /*while (isYuku[i, 0] != randomAkademisyenId && flag == 0)
				{
					if()
					i++;
				}*/
                isYuku[i, 1]++;
            }
            p.SaveChanges();
        }
    }
}