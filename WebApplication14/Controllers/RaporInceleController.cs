using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication14.Models;

namespace WebApplication14.Controllers
{
    public class RaporInceleController : Controller
    {
		private readonly AraProjeContext _context;

		public RaporInceleController(AraProjeContext context)
		{
			_context = context;
		}
		
		public IActionResult Index()
        {
			if (HttpContext.Session.GetInt32("asistan") == null) {
				return RedirectToAction("Logout", "Login");
			}
			int id = Convert.ToInt32(HttpContext.Session.GetInt32("id"));
			List<ProjeAl> incelenecekRaporlar = _context.ProjeAl.Where(x => x.AsistanId == id).ToList();

			Takvim t = _context.Takvim.FirstOrDefault(x => x.Id == 1);
			if (DateTime.Today > t.Ararapor1 && DateTime.Today < t.Ararapor2)
			{ //Ara Rapor 1'in incelenme süresi
				ViewBag.AraRapor1 = incelenecekRaporlar;
			}
			else if (DateTime.Today > t.Ararapor2 && DateTime.Today < t.Finalrapor)
			{//Ara Rapor 2'nin incelenme süresi
				ViewBag.AraRapor2 = incelenecekRaporlar;
			}

			return View();
        }
    }
}