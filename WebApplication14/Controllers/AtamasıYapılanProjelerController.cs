using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication14.Models;

namespace WebApplication14.Controllers
{
    public class AtamasıYapılanProjelerController : Controller
    {
		AraProjeContext p = new AraProjeContext();
        public IActionResult Index()
        {
			int flag = 0;
			if (HttpContext.Session.GetString("öğrenci") != null) { 
				String ogrNo = HttpContext.Session.GetString("id");//giriş yapan öğrenci

				ProjeAl alınanProje = ProjeAlmisMi(ogrNo);
				if (alınanProje != null)
				{
					ViewBag.aldığıProje = alınanProje;
					flag = 1;
				}
				ViewBag.flag = flag;
			}
			

			ViewBag.alınanProjeler = p.ProjeAl.ToList();
            return View();
        }

		public ProjeAl ProjeAlmisMi(String ogrNo)
		{
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
	}
}