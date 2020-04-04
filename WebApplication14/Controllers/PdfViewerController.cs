using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication14.Models;

namespace WebApplication14.Controllers
{
    public class PdfViewerController : Controller
    {
		private readonly AraProjeContext _context;

		public PdfViewerController(AraProjeContext context)
		{
			_context = context;
		}
		public IActionResult Index(int id)
        {
			ProjeOnerileri proje = _context.ProjeOnerileri.FirstOrDefault(x => x.Id == id);
			if (proje != null && proje.Form1 != null)
			{ //eğer akademisyen önerisi ise
				String name = proje.Form1;
				name = name.Substring(name.LastIndexOf("\\") + 1);
				ViewBag.dene = name;
				return View();
			}
			return RedirectToAction("Index", "Home");
			//OgrenciProjeOnerisi ogrenciOneri = p.OgrenciProjeOnerisi.FirstOrDefault(x => x.Id == id);//öğrenci önerisi ise
			//String name2 = ogrenciOneri.Form2;
			/*path = path.Substring(path.LastIndexOf("\\") + 1);
			ViewBag.dene = path;
			return View();*/
		}
		public IActionResult PdfGoruntule(String path) {
			if (path == null) { return RedirectToAction("Index", "Home"); }
			path = path.Substring(path.LastIndexOf("\\") + 1);
			ViewBag.dene = path;
			return View();
		}
		public IActionResult PdfGoruntule2(String path,String banch)
		{
			if (path == null || banch == null) { return RedirectToAction("Index", "Home"); }
			path = path.Substring(path.LastIndexOf("\\") + 1);
			ViewBag.dene = path;
			ViewBag.banch = banch;
			return View();
		}
	}
}