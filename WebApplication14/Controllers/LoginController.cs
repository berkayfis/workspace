using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication14.Models;

namespace WebApplication14.Controllers
{
    public class LoginController : Controller
    {
        private readonly AraProjeContext _context;

        public LoginController(AraProjeContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(String username, String pass)
        {
            string root = "C:\\Users\\FSA\\source\\repos\\demo\\WebApplication14\\wwwroot\\disc\\";

            var takvim = _context.Takvim.FirstOrDefault(x => x.Id == 1);

            var akademikPersonel = _context.AkademikPersonel.FirstOrDefault(x => x.KullaniciAdi == username && x.Sifre == pass);

            if (DateTime.Today > takvim.Form2)
                HttpContext.Session.SetInt32("Form2SureDoldu", 1);
            if (DateTime.Today < takvim.Arasinav && DateTime.Today > takvim.Finalrapor)
                HttpContext.Session.SetInt32("AraSınavveBitirmeSınav", 1);

            if (akademikPersonel != null)
            {
                HttpContext.Session.SetInt32("id", akademikPersonel.Id);
                HttpContext.Session.SetString("ad", akademikPersonel.Unvan + " " + akademikPersonel.Ad + " " + akademikPersonel.Soyad);
                if (akademikPersonel.Unvan == "Arş. Grv.")
                {
                    HttpContext.Session.SetInt32("asistan", 1);
                }
                HttpContext.Session.SetString("em", akademikPersonel.Kisaltma);
                HttpContext.Session.SetInt32("akademisyen", akademikPersonel.Id);

                string subdir = root + akademikPersonel.Kisaltma;
                // If directory does not exist, create it. 
                if (!Directory.Exists(subdir))
                {
                    Directory.CreateDirectory(subdir);
                }

                HttpContext.Session.SetString("dir", akademikPersonel.Kisaltma + "/Profil.PNG");

                ProjeKoordinatoru koor = _context.ProjeKoordinatoru.FirstOrDefault(x => x.AkademisyenId == akademikPersonel.Id);
                if (koor != null)
                {
                    HttpContext.Session.SetInt32("koordinatör", koor.Id);
                }
                return RedirectToAction("Index", "ProjeOnerilerim");
            }
            else
            {
                Ogrenci ogrenci = _context.Ogrenci.FirstOrDefault(x => x.OgrenciNo == username && x.OgrenciNo == pass);
                if (ogrenci != null)
                {
                    HttpContext.Session.SetString("id", ogrenci.OgrenciNo);
                    HttpContext.Session.SetString("ad", ogrenci.Ad + " " + ogrenci.Soyad);
                    HttpContext.Session.SetString("öğrenci", ogrenci.OgrenciNo);

                    ProjeAl proje = _context.ProjeAl.FirstOrDefault(x => x.OgrNo1 == ogrenci.OgrenciNo || x.OgrNo2 == ogrenci.OgrenciNo);
                    if (proje == null)
                    {
                        proje = _context.ProjeAl.FirstOrDefault(x => x.OgrenciOneriNoNavigation.Ogrno1 == ogrenci.OgrenciNo || x.OgrenciOneriNoNavigation.Ogrno2 == ogrenci.OgrenciNo);
                        if (proje != null)
                        {
                            HttpContext.Session.SetInt32("projeAlmisMi", 1);
                            if (proje.KabulDurumu == "Kabul")
                                HttpContext.Session.SetInt32("KabulOlduMu", 1);
                        }
                    }
                    else
                    {
                        HttpContext.Session.SetInt32("projeAlmisMi", 1);
                        if (proje.KabulDurumu == "Kabul")
                            HttpContext.Session.SetInt32("KabulOlduMu", 1);
                    }


                    string subdir = root + ogrenci.OgrenciNo;
                    // If directory does not exist, create it. 
                    if (!Directory.Exists(subdir))
                    {
                        Directory.CreateDirectory(subdir);
                    }

                    HttpContext.Session.SetString("dir", ogrenci.OgrenciNo + "/Profil.PNG");

                    if (DateTime.Today > takvim.Toplanti)
                    {
                        return RedirectToAction("Index", "AtamasıYapılanProjeler");
                    }

                    return RedirectToAction("Index", "Ogrenci");
                }
                ViewBag.Mesaj = "Kullanıcı adı veya şifre hatalı!";
                return View();
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}