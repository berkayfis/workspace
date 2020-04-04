using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication14.Models;

namespace WebApplication14.Controllers
{
    public class AtamasıYapılanProjelerController : Controller
    {
		private readonly AraProjeContext _context;

		public AtamasıYapılanProjelerController(AraProjeContext context)
		{
			_context = context;
		}

		[Authorize(Roles="Ogrenci,Koordinator,Akademisyen")]
		public IActionResult Index()
        {
			int flag = 0;
			if (HttpContext.Session.GetString("öğrenci") != null) { 
				string ogrenciNo = HttpContext.Session.GetString("id");//giriş yapan öğrenci

				ProjeAl alinanProje = ProjeAlmisMi(ogrenciNo);
				if (alinanProje != null)
				{
					ViewBag.aldigiProje = alinanProje;
					flag = 1;
				}
				ViewBag.flag = flag;
			}
			ViewBag.alinanProjeler = _context.ProjeAl.ToList();
            return View();
        }

		public ProjeAl ProjeAlmisMi(string ogrenciNo)
		{
			ProjeAl proje = _context.ProjeAl.FirstOrDefault(x => x.OgrNo1 == ogrenciNo || x.OgrNo2 == ogrenciNo);//Akademisyen önerilerinden bir proje almış mı?
			if (proje == null)
			{
				proje = _context.ProjeAl.FirstOrDefault(x => x.OgrenciOneriNoNavigation.Ogrenci1No == ogrenciNo || x.OgrenciOneriNoNavigation.Ogrenci2No == ogrenciNo);//öğrencinin kendi önerisini danışman kabul etmiş mi
				if (proje != null)
				{
					return proje;
				}
				return null;
			}
			else
				return proje;
		}
	}
}