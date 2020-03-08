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
		AraProjeContext p = new AraProjeContext();
        public IActionResult Index()
        {
			ViewBag.projeler = p.ProjeOnerileri.OrderBy(x => x.OturumNo).ToList();
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
			List<OgrenciProjeOnerisi> model = p.OgrenciProjeOnerisi.Where(x => x.Danismanid == id).ToList();
			List<ProjeAl> kabulEdilenler = new List<ProjeAl>();
			List<int> onaylandıMı = new List<int>();
			int flag = 0;

			foreach (OgrenciProjeOnerisi proje in model)
			{
				ProjeAl projeAlındıMı = p.ProjeAl.FirstOrDefault(x => x.OgrenciOneriNo == proje.Id);
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

			OgrenciProjeOnerisi proje = p.OgrenciProjeOnerisi.FirstOrDefault(x => x.Id == id);
			ProjeAl alınanProje = new ProjeAl();

			/*ProjeAl projeyiAlmisMi = p.ProjeAl.FirstOrDefault(x => x.OgrNo1 == proje.Ogrno1 || proje.Ogrno2 == proje.Ogrno1);
			if (projeyiAlmisMi != null) {
				return RedirectToAction("Logout", "Login");
			}*/

			alınanProje.OgrenciOneriNo = proje.Id;
			alınanProje.Form2 = proje.Form2;
			alınanProje.OgrNo1 = proje.Ogrno1;
			if (proje.Ogrno2 != null)
				alınanProje.OgrNo2 = proje.Ogrno2;
			alınanProje.ProjeDurumu = proje.Statu;

			p.ProjeAl.Add(alınanProje);
			p.SaveChanges();

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

			ProjeAl proje = p.ProjeAl.FirstOrDefault(x => x.OgrenciOneriNo == id);

			p.ProjeAl.Remove(proje);
			p.SaveChanges();

			return RedirectToAction("OgrenciOnerisi");
		}

		public Boolean IsForm2ExpiredDate()
		{
			Takvim takvim = p.Takvim.FirstOrDefault(x => x.Id == 1);
			if (DateTime.Today > takvim.Form2)
				return true;
			return false;
		}
	}
}