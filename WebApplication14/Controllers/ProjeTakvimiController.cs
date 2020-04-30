using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication14.Models;

namespace WebApplication14.Controllers
{
    public class ProjeTakvimiController : Controller
    {
        private readonly AraProjeContext _context;

        public ProjeTakvimiController(AraProjeContext context)
        {
            _context = context;
        }
        
        public IActionResult Index()
        {
            Takvim takvim = _context.Takvim.FirstOrDefault(x => x.Id == 1);
            ViewBag.takvim = takvim;
            return View();
        }
        
        [HttpPost]
        [Authorize(Roles = "Koordinator")]
        public IActionResult Index(Takvim takvimYeni)
        {
            Takvim takvim = _context.Takvim.FirstOrDefault(x => x.Id == 1);
            if (takvimYeni.Arabütünleme != null)
                takvim.Arabütünleme = takvimYeni.Arabütünleme;
            if (takvimYeni.Ararapor1 != null)
                takvim.Ararapor1 = takvimYeni.Ararapor1;
            if (takvimYeni.Ararapor2 != null)
                takvim.Ararapor2 = takvimYeni.Ararapor2;
            if (takvimYeni.Arasinav != null)
                takvim.Arasinav = takvimYeni.Arasinav;
            if (takvimYeni.Bkitap != null)
                takvim.Bkitap = takvimYeni.Bkitap;
            if (takvimYeni.Bütünlemerapor != null)
                takvim.Bütünlemerapor = takvimYeni.Bütünlemerapor;
            if (takvimYeni.Bitirmebutunleme != null)
                takvim.Bitirmebutunleme = takvimYeni.Bitirmebutunleme;
            if (takvimYeni.Bitirmesinav != null)
                takvim.Bitirmesinav = takvimYeni.Bitirmesinav;
            if (takvimYeni.Form2 != null)
                takvim.Form2 = takvimYeni.Form2;
            if (takvimYeni.Finalrapor != null)
                takvim.Finalrapor = takvimYeni.Finalrapor;
            if (takvimYeni.Kitap != null)
                takvim.Kitap = takvimYeni.Kitap;
            if (takvimYeni.Ret != null)
                takvim.Ret = takvimYeni.Ret;
            if (takvimYeni.Toplanti != null)
                takvim.Toplanti = takvimYeni.Toplanti;
            if (takvimYeni.Form1 != null)
                takvim.Form1 = takvimYeni.Form1;
            if (takvimYeni.Form1Toplanti != null)
                takvim.Form1Toplanti = takvimYeni.Form1Toplanti;

            _context.SaveChanges();
            return RedirectToAction("Index", "ProjeTakvimi");
        }
    }
}