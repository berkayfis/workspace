using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication14.Models;

namespace WebApplication14.Controllers
{
    public class SinifAtamaController : Controller
    {
		private readonly AraProjeContext _context;

		public SinifAtamaController(AraProjeContext context)
		{
			_context = context;
		}
		
		public IActionResult Index()
        {
			Takvim t = _context.Takvim.FirstOrDefault(x => x.Id == 1);
			if (DateTime.Today < t.Finalrapor)//daha final raporu teslim edilmemiş sınıf atamaya kalkıyor
				return RedirectToAction("Logout", "Login");
			//ara proje raporlarını yüklemeyen && final raporunu yüklemeyen, final sınavına giremez
			List<ProjeAl> buDonemProjeYapanlarAra = _context.ProjeAl.Where(x => x.KabulDurumu == "Kabul" && 
																	 (x.ProjeNoNavigation.Kategori == "Bilgisayar(Ara) Proje" || 
																	  x.OgrenciOneriNoNavigation.Turu == "Bilgisayar(Ara) Proje")&&
																     (x.Ararapor1 != null || x.Ararapor2 != null) && 
																	  x.Finalrapor != null).OrderBy(x => x.ProjeNoNavigation.DanismanId).
																	  ToList();

			List<ProjeAl> buDonemProjeYapanlarBitirme = _context.ProjeAl.Where(x => x.KabulDurumu == "Kabul" &&
																	  x.ProjeNoNavigation.Kategori == "Bitirme Projesi" &&
																	 (x.Ararapor1 != null || x.Ararapor2 != null) &&
																	  x.Finalrapor != null).ToList();

			/*FinalSinav seans = new FinalSinav();
			seans.projeAdı = "gffdsg";
			seans.ogrNo1 = "34234";*/

			List<List<ProjeAl>> danismanlikYaptigiProjeler = new List<List<ProjeAl>>();//her bir akademisyenin danışmanlık yaptığı projeleri
			List<List<ProjeAl>> danismanlikYaptigiProjeler2 = new List<List<ProjeAl>>();
			List<AkademikPersonel> akademisyenler = _context.AkademikPersonel.ToList();
			List<int> toplamProjeSayisi = new List<int>();
			foreach (AkademikPersonel akademisyen in akademisyenler)
			{
				List<ProjeAl> akademisyeninDanismanlikYaptigiAraProjeler = _context.ProjeAl.Where(x => (x.ProjeNoNavigation.DanismanId == akademisyen.Id && x.ProjeNoNavigation.Kategori == "Bilgisayar(Ara) Proje"||
																							  x.OgrenciOneriNoNavigation.Danismanid == akademisyen.Id && x.OgrenciOneriNoNavigation.Turu == "Bilgisayar(Ara) Proje") && 
																						      x.KabulDurumu == "Kabul" && (x.Ararapor1 != null || x.Ararapor2 != null) && x.Finalrapor != null).
																	                          ToList();
				List<ProjeAl> akademisyeninDanismanlikYaptigiBitirmeProjeleri = _context.ProjeAl.Where(x => (x.ProjeNoNavigation.DanismanId == akademisyen.Id && x.ProjeNoNavigation.Kategori == "Bitirme Projesi" ||
																							  x.OgrenciOneriNoNavigation.Danismanid == akademisyen.Id && x.OgrenciOneriNoNavigation.Turu == "Bitirme Projesi") &&
																							  x.KabulDurumu == "Kabul" && (x.Ararapor1 != null || x.Ararapor2 != null) && x.Finalrapor != null).
																							  ToList();
				toplamProjeSayisi.Add(akademisyeninDanismanlikYaptigiAraProjeler.Count);
				if(akademisyeninDanismanlikYaptigiAraProjeler.Count > 0)
					danismanlikYaptigiProjeler.Add(akademisyeninDanismanlikYaptigiAraProjeler);
				if (akademisyeninDanismanlikYaptigiBitirmeProjeleri.Count > 0)
					danismanlikYaptigiProjeler2.Add(akademisyeninDanismanlikYaptigiBitirmeProjeleri);
			}

			/*int i = 0;
			while (i < danismanlikYaptigiProjeler.Count) {
				foreach (ProjeAl proje in danismanlikYaptigiProjeler[i])
				{
					D106.Add(proje);
				}
				//D106 = danismanlikYaptigiProjeler[i];
				i++;
				foreach (ProjeAl proje in danismanlikYaptigiProjeler[i])
				{
					D107.Add(proje);
				}
				//D107 = danismanlikYaptigiProjeler[i];
				i++;
				foreach (ProjeAl proje in danismanlikYaptigiProjeler[i])
				{
					D108.Add(proje);
				}
				//D108 = danismanlikYaptigiProjeler[i];
				i++;
				foreach (ProjeAl proje in danismanlikYaptigiProjeler[i])
				{
					D110.Add(proje);
				}
				//D110 = danismanlikYaptigiProjeler[i];
				i++;
				foreach (ProjeAl proje in danismanlikYaptigiProjeler[i])
				{
					D010.Add(proje);
				}
				//D010 = danismanlikYaptigiProjeler[i];
				i++;
			}*/

			/*int flag1 = 0, flag2 = 0, flag3 = 0, flag4 = 0, flag5 = 0;
			foreach (List<ProjeAl> proje in danismanlikYaptigiProjeler)
			{
				if (flag1 == 0) { 
					foreach (ProjeAl proje2 in proje)
					{
						D106.Add(proje2);
					}
					flag1 = 1; 
				}
				else if (flag2 == 0)
				{
					foreach (ProjeAl proje2 in proje)
					{
						D107.Add(proje2);
					}
					flag2 = 1;
				}
				else if (flag3 == 0)
				{
					foreach (ProjeAl proje2 in proje)
					{
						D108.Add(proje2);
					}
					flag3 = 1;
				}
				else if (flag4 == 0)
				{
					foreach (ProjeAl proje2 in proje)
					{
						D110.Add(proje2);
					}
					flag4 = 1;
				}
				else if (flag5 == 0)
				{
					foreach (ProjeAl proje2 in proje)
					{
						D010.Add(proje2);
					}
					flag5 = 1;
				}
			}*/

			//jüri öğretim üyeleri arasından seçilir(arş. grv. yer almaz)
			List<AkademikPersonel> bolumdekiHocalar = _context.AkademikPersonel.Where(x => x.Unvan == "Dr. Öğr. Üyesi" || 
																					x.Unvan == "Doç. Dr." || 
																					x.Unvan == "Prof. Dr.").ToList();

			List<AkademikPersonel> jokers = new List<AkademikPersonel>();

			//proje önerisi vermeyen akademisyenler, joker jüri olarak kullan
			foreach (AkademikPersonel akademisyen in bolumdekiHocalar)
			{
				ProjeOnerileri projeOnerisi = _context.ProjeOnerileri.FirstOrDefault(x => x.DanismanId == akademisyen.Id);
				if (projeOnerisi == null) { //bu akademisyen joker
					jokers.Add(akademisyen);
				}
			}

			/*foreach (ProjeAl proje in buDonemProjeYapanlarAra)
			{
				FinalSinav session = new FinalSinav();
				session.akademistenID1 = Convert.ToInt32(proje.ProjeNoNavigation.DanismanId);
				//session.akademisyenID2 = ;
				//session.akademisyenID3 = ;
				session.ogrNo1 = proje.OgrNo1;
				if (proje.OgrNo2 != null)
					session.ogrNo2 = proje.OgrNo2;
				session.projeAdı = proje.ProjeNoNavigation.Isim;
		
				D106.Add(session);
			}*/
			
			List<String> seanslar = new List<String>() { "9.00-9.30", "9.30-10.00", "10.00-10.30", 
														 "10.30-11.00", "11.00-11.30", "11.30-12.00"};

			List<String> derslikler = new List<String>() { "D106", "D107", "D108", "D110", "D111" };

			ViewBag.seanslar = seanslar;
			ViewBag.danismanlik = danismanlikYaptigiProjeler;
			ViewBag.danismanlik2 = danismanlikYaptigiProjeler2;
			ViewBag.toplam = toplamProjeSayisi;
			ViewBag.derslikler = derslikler;
			ViewBag.akademisyeler = akademisyenler;
			ViewBag.buDonemProjeYapanlarBitirme = buDonemProjeYapanlarBitirme;
			ViewBag.buDonemProjeYapanlarAra = buDonemProjeYapanlarAra;
			ViewBag.bolumdekiHocalar = bolumdekiHocalar;
			ViewBag.jokers = jokers;
            return View();
        }
    }
}