using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
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
        public IActionResult Index(String username, String password)
        {
            //string root = "C:\\Users\\FSA\\source\\repos\\demo\\WebApplication14\\wwwroot\\disc\\";

            var takvim = _context.Takvim.FirstOrDefault(x => x.Id == 1);
            if (DateTime.Today > takvim.Form2)
                HttpContext.Session.SetInt32("Form2SureDoldu", 1);
            if (DateTime.Today < takvim.Arasinav && DateTime.Today > takvim.Finalrapor)
                HttpContext.Session.SetInt32("AraSınavveBitirmeSınav", 1);


            if (IsAkademikPersonel(username, password))
            {
                var akademikPersonel = _context.AkademikPersonel.FirstOrDefault(x => x.KullaniciAdi == username && x.Sifre == password);
                HttpContext.Session.SetInt32("id", akademikPersonel.Id);
                HttpContext.Session.SetString("ad", akademikPersonel.Unvan + " " + akademikPersonel.Ad + " " + akademikPersonel.Soyad);
                if (akademikPersonel.Unvan == "Arş. Grv.")
                {
                    HttpContext.Session.SetInt32("asistan", 1);
                }
                HttpContext.Session.SetString("em", akademikPersonel.Kisaltma);
                HttpContext.Session.SetInt32("akademisyen", akademikPersonel.Id);

                string subdir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AkademikPersonel", akademikPersonel.Kisaltma);
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
            else if (IsOgrenci(username, password))
            {
                Ogrenci ogrenci = _context.Ogrenci.FirstOrDefault(x => x.OgrenciNo == username && x.OgrenciNo == password);
                HttpContext.Session.SetString("id", ogrenci.OgrenciNo);
                HttpContext.Session.SetString("ad", ogrenci.Ad + " " + ogrenci.Soyad);
                HttpContext.Session.SetString("öğrenci", ogrenci.OgrenciNo);

                ProjeAl proje = _context.ProjeAl.FirstOrDefault(x => x.OgrNo1 == ogrenci.OgrenciNo || x.OgrNo2 == ogrenci.OgrenciNo);
                if (proje == null)
                {
                    proje = _context.ProjeAl.FirstOrDefault(x => x.OgrenciOneriNoNavigation.Ogrenci1No == ogrenci.OgrenciNo || x.OgrenciOneriNoNavigation.Ogrenci2No == ogrenci.OgrenciNo);
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

        private bool IsOgrenci(string username, string password)
        {
            Ogrenci ogrenci = _context.Ogrenci.FirstOrDefault(x => x.OgrenciNo == username && x.OgrenciNo == password);
            if (ogrenci != null)
            {
                var identityClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, ogrenci.Ad),
                    new Claim(ClaimTypes.Role, "Ogrenci")
                };
                var identity = new ClaimsIdentity(identityClaims, "Ogrenci Claims");
                var principal = new ClaimsPrincipal(identity);

                HttpContext.SignInAsync(principal);
                return true;
            }
            else
            {
                return false;
            }

        }

        private bool IsAkademikPersonel(string username, string password)
        {
            var akademikPersonel = _context.AkademikPersonel.FirstOrDefault(x => x.KullaniciAdi == username && x.Sifre == password);
            if (akademikPersonel != null)
            {
                var identityClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, akademikPersonel.Ad),
                    new Claim(ClaimTypes.Role, "Akademisyen")
                };
                if (IsKoordinator(akademikPersonel))
                {
                    identityClaims.Add(new Claim(ClaimTypes.Role, "Koordinator"));
                }

                var identity = new ClaimsIdentity(identityClaims, "Akademisyen Claims");
                var principal = new ClaimsPrincipal(identity);

                HttpContext.SignInAsync(principal);
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsKoordinator(AkademikPersonel akademikPersonel)
        {
            ProjeKoordinatoru koordinator = _context.ProjeKoordinatoru.FirstOrDefault(x => x.AkademisyenId == akademikPersonel.Id);
            if (koordinator == null)
            {
                return false;
            }
            return true;
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }
    }
}