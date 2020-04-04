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
			List<OgrenciProjeOnerisi> model = _context.OgrenciProjeOnerisi.Where(x => x.Danismanid == id).ToList();
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
			ProjeAl alınanProje = new ProjeAl();

			/*ProjeAl projeyiAlmisMi = p.ProjeAl.FirstOrDefault(x => x.OgrNo1 == proje.Ogrno1 || proje.Ogrno2 == proje.Ogrno1);
			if (projeyiAlmisMi != null) {
				return RedirectToAction("Logout", "Login");
			}*/

			alınanProje.OgrenciOneriNo = proje.Id;
			alınanProje.Form2 = proje.Form2;
			alınanProje.OgrNo1 = proje.Ogrenci1No;
			if (proje.Ogrenci2No != null)
				alınanProje.OgrNo2 = proje.Ogrenci2No;
			alınanProje.ProjeDurumu = proje.Statu;

			_context.ProjeAl.Add(alınanProje);
			_context.SaveChanges();

			return RedirectToAction("OgrenciOnerisi");
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