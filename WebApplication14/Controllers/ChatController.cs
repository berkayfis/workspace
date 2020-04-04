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
using Microsoft.AspNetCore.Authorization;

namespace WebApplication14.Controllers
{
	[Authorize]
    public class ChatController : Controller
    {
		private readonly AraProjeContext _context;

		public ChatController(AraProjeContext context)
		{
			_context = context;
		}
		public IActionResult Index()
        {
			//if (HttpContext.Session.GetInt32("akademisyen") == null)
			//{
			//	return RedirectToAction("Logout", "Login");
			//}
			SetMessageGuid();
			//ViewBag.DanismaniOldugumProjeler = _context.ProjeAl.Where(x => (x.ProjeNoNavigation.DanismanId == id || x.OgrenciOneriNoNavigation.Danismanid == id) && x.KabulDurumu == "Kabul").ToList();
            return View();
        }

		private void SetMessageGuid()
		{
			var userRole = User.Identity.AuthenticationType;
			if (userRole == "Akademisyen Claims")
			{
				CheckAkademisyenMessageGuid();				
			}
			else if (userRole == "Ogrenci Claims")
			{
				CheckOgrenciMessageGuid();
			}
		}
		private void CheckOgrenciMessageGuid()
		{
			string id = HttpContext.Session.GetString("id");
			Ogrenci ogrenci = _context.Ogrenci.Find(id);
			if (ogrenci != null && ogrenci.MessageId == Guid.Empty)
			{
				ogrenci.MessageId = Guid.NewGuid();
				_context.Ogrenci.Update(ogrenci);
				_context.SaveChanges();
			}
		}
		private void CheckAkademisyenMessageGuid()
		{
			int id = Convert.ToInt32(HttpContext.Session.GetInt32("id"));
			AkademikPersonel akademisyen = _context.AkademikPersonel.Find(id);
			if (akademisyen != null && akademisyen.MessageId == Guid.Empty)
			{
				akademisyen.MessageId = Guid.NewGuid();
				_context.AkademikPersonel.Update(akademisyen);
				_context.SaveChanges();
			}
		}

		[Authorize]
		public IActionResult SendEmail() {
			//if (HttpContext.Session.GetInt32("akademisyen") == null)
			//{
			//	return RedirectToAction("Logout", "Login");
			//}

			//int id = 1;
			//try
			//{
			//	id = Convert.ToInt32(HttpContext.Request.Query["id"].ToString());
			//}
			//catch (NullReferenceException nre)
			//{
			//	return RedirectToAction("Index", "Home");
			//}
			//ProjeAl proje = _context.ProjeAl.FirstOrDefault(x => x.Id == id);
			var akademisyenler = _context.AkademikPersonel.OrderBy(x => x.Ad);
			var ogrenciler = _context.Ogrenci.OrderBy(x => x.OgrenciNo);

			ViewBag.Akademisyenler = akademisyenler;
			ViewBag.Ogrenciler = ogrenciler;
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
			AkademikPersonel a = _context.AkademikPersonel.Find(id);

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