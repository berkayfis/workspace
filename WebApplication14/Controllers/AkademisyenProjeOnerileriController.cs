using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication14.Models;

namespace WebApplication14.Controllers
{
    public class AkademisyenProjeOnerileriController : Controller
    {
		private readonly AraProjeContext _context;

		public AkademisyenProjeOnerileriController(AraProjeContext context)
		{
			_context = context;
		}
		
		public IActionResult Index()
        {
			ViewBag.projeler = _context.ProjeOnerileri.OrderBy(x => x.OturumNo).ToList();
            return View();
        }
		
		public IActionResult OgrenciOnerisi() {
			if (HttpContext.Session.GetInt32("akademisyen") == null)
			{
				return RedirectToAction("Logout", "Login");
			}
			else if (IsForm2ExpiredDate())
			{
				return RedirectToAction("Logout", "Login");
			}

			int id = Convert.ToInt32(HttpContext.Session.GetInt32("id"));
			List<OgrenciProjeOnerisi> model = _context.OgrenciProjeOnerisi.Where(x => x.Danismanid == id && x.OgrenciOnayi == 1).ToList();
			List<ProjeAl> kabulEdilenler = new List<ProjeAl>();
			List<int> onaylandıMı = new List<int>();
			int flag = 0;

			foreach (OgrenciProjeOnerisi proje in model)
			{
				ProjeAl projeAlındıMı = _context.ProjeAl.FirstOrDefault(x => x.OgrenciOneriNo == proje.Id);
				if (projeAlındıMı != null)
				{
					//kabulEdilenler.Add(projeAlındıMı);
					onaylandıMı.Add(1);
				}
				else {
					//kabulEdilenler.Add(new ProjeAl());	
					onaylandıMı.Add(0);
					flag = 1;
				}
			}

			ViewBag.flag = flag;
			ViewBag.OnayladığımProjeler = onaylandıMı;
			return View(model);
		}

		public IActionResult OgrenciOnerisiOnayla(int id) {
			if (HttpContext.Session.GetInt32("akademisyen") == null)
			{
				return RedirectToAction("Logout", "Login");
			}
			else if (IsForm2ExpiredDate())
			{
				return RedirectToAction("Logout", "Login");
			}

			OgrenciProjeOnerisi proje = _context.OgrenciProjeOnerisi.FirstOrDefault(x => x.Id == id);
			if (ProjeAlmisMi(proje))
			{

			}
			else
			{
				ProjeAl alınanProje = new ProjeAl();
				alınanProje.OgrenciOneriNo = proje.Id;
				alınanProje.Form2 = proje.Form2;
				alınanProje.OgrNo1 = proje.Ogrenci1No;
				if (proje.Ogrenci2No != null)
					alınanProje.OgrNo2 = proje.Ogrenci2No;
				alınanProje.ProjeDurumu = proje.Statu;

				_context.ProjeAl.Add(alınanProje);

				var ogrenci1Oneriler = _context.OgrenciProjeOnerisi.Where(x => (x.Ogrenci1No == proje.Ogrenci1No || x.Ogrenci2No == proje.Ogrenci1No) && x.Id != id).ToList();
				var ogrenci1Istekler = _context.Istek.Where(x => x.OgrNo1 == proje.Ogrenci1No || x.OgrNo2 == proje.Ogrenci1No).ToList();

				_context.OgrenciProjeOnerisi.RemoveRange(ogrenci1Oneriler);
				_context.Istek.RemoveRange(ogrenci1Istekler);

				if (proje.Ogrenci2No != null)
				{
					var ogrenci2Oneriler = _context.OgrenciProjeOnerisi.Where(x => (x.Ogrenci1No == proje.Ogrenci2No || x.Ogrenci2No == proje.Ogrenci2No)&& x.Id!=id).ToList();
					var ogrenci2Istekler = _context.Istek.Where(x => x.OgrNo1 == proje.Ogrenci2No || x.OgrNo2 == proje.Ogrenci2No).ToList();
					
					_context.OgrenciProjeOnerisi.RemoveRange(ogrenci2Oneriler);
					_context.Istek.RemoveRange(ogrenci2Istekler);
				}
			}

			/*ProjeAl projeyiAlmisMi = p.ProjeAl.FirstOrDefault(x => x.OgrNo1 == proje.Ogrno1 || proje.Ogrno2 == proje.Ogrno1);
			if (projeyiAlmisMi != null) {
				return RedirectToAction("Logout", "Login");
			}*/

			
			_context.SaveChanges();

			return RedirectToAction("OgrenciOnerisi");
		}

		private bool ProjeAlmisMi(OgrenciProjeOnerisi oneri)
		{
			ProjeAl proje = new ProjeAl();
			if (oneri.Ogrenci2No != null)
			{
				proje = _context.ProjeAl.FirstOrDefault(x => x.OgrNo1 == oneri.Ogrenci1No || x.OgrNo1 == oneri.Ogrenci2No || x.OgrNo2 == oneri.Ogrenci1No || x.OgrNo2 == oneri.Ogrenci2No);
			}
			else
			{
				proje = _context.ProjeAl.FirstOrDefault(x => x.OgrNo1 == oneri.Ogrenci1No || x.OgrNo2 == oneri.Ogrenci1No);
			}
			if (proje == null)
			{
				return false;
			}
			return true;
		}

		public IActionResult Discard(int id) {
			if (HttpContext.Session.GetInt32("akademisyen") == null)
			{
				return RedirectToAction("Logout", "Login");
			}
			else if (IsForm2ExpiredDate())
			{
				return RedirectToAction("Logout", "Login");
			}

			ProjeAl proje = _context.ProjeAl.FirstOrDefault(x => x.OgrenciOneriNo == id);

			_context.ProjeAl.Remove(proje);
			_context.SaveChanges();

			return RedirectToAction("OgrenciOnerisi");
		}

		public Boolean IsForm2ExpiredDate()
		{
			Takvim takvim = _context.Takvim.FirstOrDefault(x => x.Id == 1);
			if (DateTime.Today > takvim.Form2)
				return true;
			return false;
		}
	}
}