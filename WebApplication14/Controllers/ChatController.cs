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
			GetMessages();
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
		public async Task<IActionResult> SendEmailAsync(string Icerik, Guid ReceiverId, IFormFile Eklenti) {
			if (Eklenti != null) { 
				if (Eklenti.Length > 0)
				{
					// full path to file in temp location
					string filePath = "C:\\Users\\FSA\\source\\repos\\demo\\WebApplication14\\wwwroot\\disc\\" + Eklenti.FileName;

					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						await Eklenti.CopyToAsync(stream);
					}
				}
			}
			Guid senderGuid = GetCurrentUserMessageId();

			_context.Messages.Add(new Message
			{
				Content = Icerik,
				CreatedDate = DateTime.Now,
				ReceiverId = ReceiverId,
				SenderId =  senderGuid
			});
			_context.SaveChanges();			

			return RedirectToAction("Index");
		}

		private Guid GetCurrentUserMessageId()
		{
			var userRole = User.Identity.AuthenticationType;
			if (userRole == "Akademisyen Claims")
			{
				int id = Convert.ToInt32(HttpContext.Session.GetInt32("id"));
				AkademikPersonel akademisyen = _context.AkademikPersonel.Find(id);
				return akademisyen.MessageId;
			}
			else if (userRole == "Ogrenci Claims")
			{
				string id = HttpContext.Session.GetString("id");
				Ogrenci ogrenci = _context.Ogrenci.Find(id);
				return ogrenci.MessageId;
			}
			else
			{
				return Guid.Empty;
			}
		}

		private List<Guid> GetMessages()
		{
			try
			{
				Guid currentUserMessageId = GetCurrentUserMessageId();
				var receivedMessages = _context.Messages.Where(x => x.ReceiverId == currentUserMessageId).GroupBy(x=> x.SenderId).Select(x=>x.Key).ToList();
				var sendedMessages = _context.Messages.Where(x => x.SenderId == currentUserMessageId).GroupBy(x => x.ReceiverId).Select(x => x.Key).ToList();

				return null;
				
			}
			catch (Exception)
			{

				throw;
			}
		}
	}
}