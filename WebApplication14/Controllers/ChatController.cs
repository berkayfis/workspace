using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System.Net;
using WebApplication14.Models;
using System.Net.Mail;

namespace WebApplication14.Controllers
{
    public class ChatController : Controller
    {
		AraProjeContext p = new AraProjeContext();
        public IActionResult Index()
        {
			if (HttpContext.Session.GetInt32("akademisyen") == null)
			{
				return RedirectToAction("Logout", "Login");
			}

			int id = Convert.ToInt32(HttpContext.Session.GetInt32("id"));
			ViewBag.DanismaniOldugumProjeler = p.ProjeAl.Where(x => (x.ProjeNoNavigation.DanismanId == id || x.OgrenciOneriNoNavigation.Danismanid == id) && x.KabulDurumu == "Kabul").ToList();
            return View();
        }
		public IActionResult SendEmail() {
			if (HttpContext.Session.GetInt32("akademisyen") == null)
			{
				return RedirectToAction("Logout", "Login");
			}

			int id = 1;
			try
			{
				id = Convert.ToInt32(HttpContext.Request.Query["id"].ToString());
			}
			catch (NullReferenceException nre)
			{
				return RedirectToAction("Index", "Home");
			}
			ProjeAl proje = p.ProjeAl.FirstOrDefault(x => x.Id == id);


			return View();
		}

		[HttpPost]
		public async Task<IActionResult> SendEmailAsync(String Baslik, String İcerik, IFormFile Eklenti) {
			String filePath = "";
			BodyBuilder bodyBuilder = new BodyBuilder();
			if (Eklenti != null) { 
				if (Eklenti.Length > 0)
				{
					// full path to file in temp location
					filePath = "C:\\Users\\FSA\\source\\repos\\demo\\WebApplication14\\wwwroot\\disc\\" + Baslik + ".pdf";

					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						await Eklenti.CopyToAsync(stream);
					}
					bodyBuilder.Attachments.Add(filePath);
				}
			}
			

			MimeMessage m = new MimeMessage();
			MailboxAddress from = new MailboxAddress("YTÜ CE Proje Koordinatörlüğü", "ytuceprojectcoordinatorship@gmail.com");
			m.From.Add(from);
			MailboxAddress to = new MailboxAddress("KarsiTaraf","fsakyildiz@gmail.com");//öğrencilerin e mail adresi
			m.To.Add(to);
			int id = Convert.ToInt32(HttpContext.Session.GetInt32("id"));
			AkademikPersonel a = p.AkademikPersonel.Find(id);

			m.Subject = a.Unvan + " " + a.Ad + " "  + a.Soyad + " - " + Baslik;
			bodyBuilder.TextBody = İcerik;

			m.Body = bodyBuilder.ToMessageBody();

			MailKit.Net.Smtp.SmtpClient client = new MailKit.Net.Smtp.SmtpClient();
			try
			{
				client.Connect("smtp.gmail.com", 587, false);
			}
			catch (Exception e) {
				HttpContext.Session.SetInt32("mail", 0);
				return RedirectToAction("Index", "Chat");
			}
			client.Authenticate("ytuceprojectcoordinatorship@gmail.com", "sami5777");
			client.Send(m);
			client.Disconnect(true);
			client.Dispose();

			HttpContext.Session.SetInt32("mail", 1);
			return RedirectToAction("Index");
		}
    }
}