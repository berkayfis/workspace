using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication14.Models;

namespace WebApplication14.Controllers
{
	public class OgrenciController : Controller
	{
		AraProjeContext p = new AraProjeContext();
		public IActionResult Index()
		{
			if (HttpContext.Session.GetInt32("Form2SureDoldu") == 1) {//Form2 tesliminden sonra istek gönderme kapanır
				return RedirectToAction("Index", "AtamasıYapılanProjeler");
			}

			int flag = 0;
			String ogrNo = HttpContext.Session.GetString("id");//giriş yapan öğrenci

			ProjeAl alınanProje = ProjeAlmisMi(ogrNo);
			if (alınanProje != null)
			{
				ViewBag.aldığıProje = alınanProje;
				flag = 1;
			}
			ViewBag.flag = flag;

			List<ProjeOnerileri> akademisyenOnerileri = p.ProjeOnerileri.OrderBy(x => x.Danisman.Kisaltma).ToList();
			ViewBag.AkademisyenOnerileri = akademisyenOnerileri;

			List<int> kaçKezAtandı = new List<int>();
			foreach (ProjeOnerileri proje in akademisyenOnerileri)
			{
				kaçKezAtandı.Add(p.ProjeAl.Where(x => x.ProjeNo == proje.Id).Count());
			}
			ViewBag.kaçKezAtandı = kaçKezAtandı;

			return View();
		}

		public IActionResult ProjeOner() {
			if (HttpContext.Session.GetInt32("Form2SureDoldu") == 1)//Form2 tesliminden sonra proje öneremezsin
			{
				return RedirectToAction("Logout", "Login");
			}

			String ogrNo = HttpContext.Session.GetString("öğrenci");

			ProjeAl alınanProje = ProjeAlmisMi(ogrNo);
			if (alınanProje != null) {// bu arkadaşa proje atanmış, proje önermesin
				return RedirectToAction("Index", "Ogrenci");
			}

			ViewBag.oturumlar = p.AlanOturum.ToList();
			ViewBag.akademisyenler = p.AkademikPersonel.ToList().OrderBy(x => x.Kisaltma);
			ViewBag.öğrenciler = atamasıYapılmayanOgrenciler();
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> ProjeOnerAsync(OgrenciProjeOnerisi proje, IFormFile Form2)
		{
			String ogrNo = HttpContext.Session.GetString("id");
			Ogrenci a = p.Ogrenci.Find(ogrNo);
			if (a != null) {
				proje.Ogrno1 = ogrNo;
			}

			if (Form2.Length > 0)
			{
				// full path to file in temp location
				var filePath = "C:\\Users\\FSA\\source\\repos\\demo\\WebApplication14\\wwwroot\\disc\\" + proje.Ogrno1 + " - " + proje.Isim + ".pdf";

				using (var stream = new FileStream(filePath, FileMode.Create))
				{
					await Form2.CopyToAsync(stream);
				}
				String s = filePath;
				proje.Form2 = s;
			}

			//burada aynı zamanda PROJE_AL'a da ekleyeceğiz
			ProjeAl alınan_proje = new ProjeAl();
			/*alınan_proje.OgrNo1 = ogrNo;
			if (proje.Ogrno2 != null)
				alınan_proje.OgrNo2 = proje.Ogrno2;
			alınan_proje.= proje.Danismanid;*/
			alınan_proje.ProjeDurumu = "Yeni Proje (Öğrenci Önerisinden)";

			if (proje.Ogrno2 == "0")
				proje.Ogrno2 = null;
			p.OgrenciProjeOnerisi.Add(proje);
			p.SaveChanges();
			//p.ProjeAl.Add(alınan_proje);
			//p.SaveChanges();
			return RedirectToAction("Index", "Ogrenci");
		}


		public IActionResult IstekGonder(int id) {
			if (HttpContext.Session.GetInt32("Form2SureDoldu") == 1)//Form2 tesliminden sonra istek gönderemezsin
			{
				return RedirectToAction("Logout", "Login");
			}
			String ogrNo = HttpContext.Session.GetString("id");

			//Kontenjan dolduysa istek gönderemezsin
			int count = p.ProjeAl.Where(x => x.ProjeNo == id).Count();
			ProjeOnerileri projeÖnerisi = p.ProjeOnerileri.FirstOrDefault(x => x.Id == id);
			if (count == projeÖnerisi.GrupSayisi)
				return RedirectToAction("Logout", "Login");

			//Bir akademisyen, seni bir projeye atadıysa istek gönderemezsin 
			ProjeAl projeal = ProjeAlmisMi(ogrNo);
			if (projeal != null)
			{
				return RedirectToAction("Logout", "Login");
			}

			//Form2 teslim tarihinden sonra istek gönderemezsin
			if (HttpContext.Session.GetInt32("Form2SureDoldu") == 1)
				return RedirectToAction("Logout", "Login");

			//bu projeye daha önce istek göndermiş mi
			int flag = 0;
			Istek istek = p.Istek.FirstOrDefault(x => x.OgrNo1 == ogrNo && x.ProjeId == id);
			if (istek != null)
			{
				flag = 1;
			}
			else
			{
				istek = p.Istek.FirstOrDefault(x => x.OgrNo2 == ogrNo && x.ProjeId == id);
				if (istek != null)
				{
					flag = 1;
				}
			}
			ViewBag.flag = flag;

			ProjeOnerileri proje = p.ProjeOnerileri.FirstOrDefault(x => x.Id == id);
			//giriş yapan kullanıcıyı view'a gönderME
			List<Ogrenci> öğrenciler = p.Ogrenci.ToList();
			Ogrenci ogrenci = öğrenciler.FirstOrDefault(x => x.OgrenciNo == ogrNo);
			öğrenciler.Remove(ogrenci);

			//ataması yapılan öğrencileri view'a gönderME
			ViewBag.öğrenciler = atamasıYapılmayanOgrenciler();

			HttpContext.Session.SetInt32("İstekProjeID", proje.Id);
			return View(proje);
		}
		[HttpPost]
		public async Task<IActionResult> IstekGonderAsync(String OgrNo2, IFormFile Form2)
		{
			int id = Convert.ToInt32(HttpContext.Session.GetInt32("İstekProjeID"));
			Istek i = new Istek();
			i.ProjeId = id;
			String ogrNo = HttpContext.Session.GetString("id");
			i.OgrNo1 = ogrNo;
			i.OgrNo2 = OgrNo2;

			ProjeOnerileri proje = p.ProjeOnerileri.FirstOrDefault(x => x.Id == id);
			if (Form2 != null) {
				if (Form2.Length > 0)
				{
					// full path to file in temp location
					String filePath = "";
					//akademisyen daha önce giriş yapmadıysa DirectoryNotFoundException
					if (OgrNo2 != null)
						filePath = "C:\\Users\\FSA\\source\\repos\\demo\\WebApplication14\\wwwroot\\disc\\" + proje.Danisman.Kisaltma + "\\" + ogrNo + "-" + OgrNo2 + ".pdf";
					else
						filePath = "C:\\Users\\FSA\\source\\repos\\demo\\WebApplication14\\wwwroot\\disc\\" + proje.Danisman.Kisaltma + "\\" + ogrNo + ".pdf";
					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						await Form2.CopyToAsync(stream);
					}
					i.Form2 = filePath;
				}
			}


			p.Istek.Add(i);
			p.SaveChanges();
			return RedirectToAction("Index", "Ogrenci");
		}

		public IActionResult IstekGonderdigimProjeler() {
			String ogrNo = HttpContext.Session.GetString("id");

			var takvim = p.Takvim.FirstOrDefault(x => x.Id == 1);//bunu FirstOrDefault(x => x.Donem == "2019-Güz") yap
			DateTime bugün = DateTime.Today;
			DateTime Form2SonGün = Convert.ToDateTime(takvim.Form2);
			if (Form2SonGün < bugün)
				ViewBag.takvim = 1;//form2 son gün geçti

			ViewBag.istekGonderdigimProjeler = p.Istek.Where(x => x.OgrNo1 == ogrNo || x.OgrNo2 == ogrNo).ToList();
			return View();
		}
		public IActionResult BuDonemAldığımProje() {
			String ogrNo = HttpContext.Session.GetString("id");

			ProjeAl proje = ProjeAlmisMi(ogrNo);
			if (proje != null && proje.KabulDurumu == "Kabul")
			{
				ViewBag.BuDonemAldigimProje = proje;

				var takvim = p.Takvim.FirstOrDefault(x => x.Id == 1);
				if (DateTime.Today == takvim.Ararapor1)
				{
					ViewBag.AraRapor1 = 1;
				}
				else if (DateTime.Today == takvim.Ararapor2)
					ViewBag.AraRapor2 = 1;
				else if (DateTime.Today == takvim.Finalrapor)
					ViewBag.FinalRapor = 1;
			}
			else {//bu dönem proje almıyorsa
				return RedirectToAction("Logout", "Login");
			}

			return View();
		}
		[HttpPost]
		public async Task<IActionResult> BuDonemAldığımProjeAsync(IFormFile AraRapor1, IFormFile AraRapor2, IFormFile FinalRaporu)
		{
			String ogrNo = HttpContext.Session.GetString("id");
			ProjeAl proje = ProjeAlmisMi(ogrNo);
			String filePath = "C:\\Users\\FSA\\source\\repos\\demo\\WebApplication14\\wwwroot\\disc\\" + ogrNo + "\\";

			if (AraRapor1 != null)
			{
				if (AraRapor1.Length > 0)
				{
					filePath += "AraRapor1.pdf";

					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						await AraRapor1.CopyToAsync(stream);
					}
					if (proje != null)//zaten var proje:)
					{
						proje.Ararapor1 = filePath;
						p.SaveChanges();
					}

				}
			}
			else if (AraRapor2 != null)
			{
				if (AraRapor2.Length > 0)
				{
					// full path to file in temp location
					filePath += "AraRapor2.pdf";


					await WriteToDiskAsync(AraRapor2, filePath);
					if (proje != null)//zaten var proje:)
					{
						proje.Ararapor2 = filePath;
						p.SaveChanges();
					}

				}
			}
			else if (FinalRaporu != null)
			{
				if (FinalRaporu.Length > 0)
				{
					filePath += "\\FinalRapor.pdf";

					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						await FinalRaporu.CopyToAsync(stream);
					}
					if (proje != null)//zaten var proje:)
					{
						proje.Finalrapor = filePath;
						p.SaveChanges();
					}

				}
			}

			return RedirectToAction("BuDonemAldığımProje", "Ogrenci");
		}
		public IActionResult DevamProjesi() {
			String no = HttpContext.Session.GetString("id");
			ViewBag.projem = p.EskiBasarisizAlinanProje.Where(x => x.Ogrno1 == no).ToList();
			return View();
		}
		[HttpPost]
		public IActionResult DevamProjesi(int id)
		{
			EskiBasarisizAlinanProje kalınanProje = p.EskiBasarisizAlinanProje.FirstOrDefault(x => x.Id == id);
			OgrenciProjeOnerisi proje = new OgrenciProjeOnerisi();

			proje.Isim = kalınanProje.Isim;
			proje.Ogrno1 = kalınanProje.Ogrno1;
			proje.OturumNo = kalınanProje.OturumNo;
			proje.Statu = kalınanProje.Statu;
			proje.Turu = kalınanProje.Turu;
			proje.Danismanid = kalınanProje.Danismanid;
			proje.Form2 = kalınanProje.Form2;

			p.OgrenciProjeOnerisi.Add(proje);
			p.SaveChanges();

			return RedirectToAction("Index");
		}

		public List<Ogrenci> atamasıYapılmayanOgrenciler() {
			List<Ogrenci> öğrenciler = p.Ogrenci.ToList();
			List<ProjeAl> alınanProjeler = p.ProjeAl.ToList();
			List<String> projeAlanNumaralar = new List<String>();

			foreach (ProjeAl proje1 in alınanProjeler)//proje alan öğrencileri bul
			{
				if (proje1.ProjeNo != null)
				{
					projeAlanNumaralar.Add(proje1.OgrNo1);
					if (proje1.OgrNo2 != null)
					{
						projeAlanNumaralar.Add(proje1.OgrNo2);
					}
				}
				else if (proje1.OgrenciOneriNo != null)
				{
					projeAlanNumaralar.Add(proje1.OgrenciOneriNoNavigation.Ogrno1);
					if (proje1.OgrenciOneriNoNavigation.Ogrno2 != null)
					{
						projeAlanNumaralar.Add(proje1.OgrenciOneriNoNavigation.Ogrno2);
					}
				}
			}
			foreach (String Numara in projeAlanNumaralar)//Proje alan numaraları öğrencilerden çıkar
			{
				Ogrenci öğrenci = öğrenciler.FirstOrDefault(x => x.OgrenciNo == Numara);
				if (öğrenci != null)
				{
					öğrenciler.Remove(öğrenci);
				}
			}
			//kendisini de seçemez
			String ogrNo = HttpContext.Session.GetString("id");
			Ogrenci ogrenci = p.Ogrenci.FirstOrDefault(x => x.OgrenciNo == ogrNo);
			öğrenciler.Remove(ogrenci);

			return öğrenciler;
		}

		public ProjeAl ProjeAlmisMi(String ogrNo) {
			ProjeAl proje = p.ProjeAl.FirstOrDefault(x => x.OgrNo1 == ogrNo || x.OgrNo2 == ogrNo);//Akademisyen önerilerinden bir proje almış mı?
			if (proje == null)
			{
				proje = p.ProjeAl.FirstOrDefault(x => x.OgrenciOneriNoNavigation.Ogrno1 == ogrNo || x.OgrenciOneriNoNavigation.Ogrno2 == ogrNo);//öğrencinin kendi önerisini danışman kabul etmiş mi
				if (proje != null)
				{
					return proje;
				}
				return null;
			}
			else
				return proje;
		}
		public async Task WriteToDiskAsync(IFormFile file,String filePath) {
			if (file != null)
			{
				if (file.Length > 0)
				{
					try
					{
						/*if (System.IO.File.Exists(Path.Combine(root, file)))
						{
							// If file found, delete it    
							System.IO.File.Delete(Path.Combine(root, file));
							Console.WriteLine("File deleted.");
						}*/
					}
					catch (Exception e)
					{

					}
					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						await file.CopyToAsync(stream);
					}
				}
			}
		}
	}
}