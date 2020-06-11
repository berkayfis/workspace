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

			var araProjeSinavTarihi = t.Arasinav;
			var bitirmeProjeSinavTarihi = t.Bitirmesinav;

			var finalExams = _context.Finals.Where(x => x.SinavTarihi == araProjeSinavTarihi || x.SinavTarihi == bitirmeProjeSinavTarihi).ToList();
			if (finalExams.Count != 0)
			{
				var araProjects = finalExams.Where(x => x.projeTipi == "Bilgisayar(Ara) Proje").ToList();
				var bitirmeProjects = finalExams.Where(x => x.projeTipi == "Bitirme Projesi").ToList();

				ViewBag.araProjeler = araProjects;
				ViewBag.bitirmeProjeler = bitirmeProjects;
			}
			else
			{
				var isKoordinator = User.IsInRole("Koordinator");
				var isAkademisyen = User.IsInRole("Akademisyen");
				var isOgrenci = User.IsInRole("Ogrenci");
				if (isKoordinator)
				{


					//ara proje raporlarını yüklemeyen && final raporunu yüklemeyen, final sınavına giremez
					List<ProjeAl> projects = _context.ProjeAl.Where(x => x.KabulDurumu == "Kabul" && (x.Ararapor1 != null || x.Ararapor2 != null) && x.Finalrapor != null).ToList();

					//jüri öğretim üyeleri arasından seçilir(arş. grv. yer almaz)
					List<AkademikPersonel> bolumdekiHocalar = _context.AkademikPersonel.Where(x => x.Unvan == "Dr. Öğr. Üyesi" ||
																							x.Unvan == "Doç. Dr." ||
																							x.Unvan == "Prof. Dr.").ToList();


					List<String> seanslar = new List<String>() { "9.00-9.30", "9.30-10.00", "10.00-10.30",
														 "10.30-11.00", "11.00-11.30", "11.30-12.00"};

					List<String> derslikler = new List<String>() { "D106", "D107", "D108", "D110", "D111", "D112" };

					List<FinalSinav> finalProgram = new List<FinalSinav>();
					var i = 0;
					foreach (var project in projects)
					{
						var danisman = project.OgrenciOneriNoNavigation != null ? project.OgrenciOneriNoNavigation.Danisman : project.ProjeNoNavigation.Danisman;
						bolumdekiHocalar.Remove(danisman);

						var finalExam = new FinalSinav
						{
							projeAdı = project.OgrenciOneriNoNavigation != null ? project.OgrenciOneriNoNavigation.Isim : project.ProjeNoNavigation.Isim,
							projeTipi = project.OgrenciOneriNoNavigation != null ? project.OgrenciOneriNoNavigation.Turu : project.ProjeNoNavigation.Kategori,
							akademisyenID1 = project.OgrenciOneriNoNavigation != null ? Convert.ToInt32(project.OgrenciOneriNoNavigation.Danismanid) : Convert.ToInt32(project.ProjeNoNavigation.DanismanId),
							akademisyenKisaltma1 = project.OgrenciOneriNoNavigation != null ? project.OgrenciOneriNoNavigation.Danisman.Kisaltma : project.ProjeNoNavigation.Danisman.Kisaltma,
							ogrNo1 = project.OgrNo1,
							ogrNo2 = project.OgrNo2,
						};

						if (bolumdekiHocalar.Count == 0)
						{
							bolumdekiHocalar = _context.AkademikPersonel.Where(x => x.Unvan == "Dr. Öğr. Üyesi" ||
																							x.Unvan == "Doç. Dr." ||
																							x.Unvan == "Prof. Dr.").ToList();
							bolumdekiHocalar.Remove(danisman);
						}

						var rand = new Random();
						var randomAkademisyenId = rand.Next(bolumdekiHocalar.Count);

						finalExam.akademisyenID2 = bolumdekiHocalar[randomAkademisyenId].Id;
						finalExam.akademisyenKisaltma2 = bolumdekiHocalar[randomAkademisyenId].Kisaltma;
						bolumdekiHocalar.RemoveAt(randomAkademisyenId);

						if (bolumdekiHocalar.Count == 0)
						{
							bolumdekiHocalar = _context.AkademikPersonel.Where(x => x.Unvan == "Dr. Öğr. Üyesi" ||
																							x.Unvan == "Doç. Dr." ||
																							x.Unvan == "Prof. Dr.").ToList();
							bolumdekiHocalar.Remove(danisman);
							bolumdekiHocalar.Remove(bolumdekiHocalar[randomAkademisyenId]);
						}
						randomAkademisyenId = rand.Next(bolumdekiHocalar.Count);
						finalExam.akademisyenID3 = bolumdekiHocalar[randomAkademisyenId].Id;
						finalExam.akademisyenKisaltma3 = bolumdekiHocalar[randomAkademisyenId].Kisaltma;
						bolumdekiHocalar.RemoveAt(randomAkademisyenId);

						bolumdekiHocalar.Add(danisman);
						finalProgram.Add(finalExam);
					}

					var araProjects = finalProgram.Where(x => x.projeTipi == "Bilgisayar(Ara) Proje").ToList();
					araProjects = araProjects.GroupBy(x => x.akademisyenID1).OrderByDescending(g => g.Count())
						.SelectMany(x => x).ToList();
					var bitirmeProjects = finalProgram.Where(x => x.projeTipi == "Bitirme Projesi").ToList();
					bitirmeProjects = bitirmeProjects.GroupBy(x => x.akademisyenID1).OrderByDescending(g => g.Count())
						.SelectMany(x => x).ToList();

					i = 0; var j = 0;
					foreach (var finalExam in bitirmeProjects)
					{
						finalExam.Seans = seanslar[j];
						finalExam.Sinif = derslikler[i];
						finalExam.SinavTarihi = bitirmeProjeSinavTarihi;

						j++;

						i = j == 6 ? i + 1 : i;
						j = j % 6;
						i = i % 6;
					}

					i = 0; j = 0;
					foreach (var araProjeExam in araProjects)
					{
						araProjeExam.Seans = seanslar[j];
						araProjeExam.Sinif = derslikler[i];
						araProjeExam.SinavTarihi = araProjeSinavTarihi;

						j++;

						i = j == 6 ? i + 1 : i;
						j = j % 6;
						i = i % 6;
					}
					_context.Finals.AddRange(araProjects);
					_context.Finals.AddRange(bitirmeProjects);
					_context.SaveChanges();

					ViewBag.araProjeler = araProjects;
					ViewBag.bitirmeProjeler = bitirmeProjects;
				}
				else if(isOgrenci ||isAkademisyen)
				{
					ViewBag.ErrorMessage = "Koordinatör tarafından final programı oluşturulmadı.";
				}
			}
            return View();
        }		
	}	
}