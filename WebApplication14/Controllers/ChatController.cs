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
			var messageUsers = GetMessages();
			ViewBag.MessageUsers = GetMessageUsers(messageUsers);
            return View();
        }

		[Authorize]
		public IActionResult SendEmail(Guid receiverGuid) {
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
			var ogrenciler = _context.Ogrenci.OrderBy(x => x.Ad);

			if (receiverGuid != Guid.Empty)
			{
				ViewBag.ReceiverGuid = receiverGuid;
			}

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
					string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Eklenti.FileName);

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

		public IActionResult ChatHistory(Guid guid)
		{
			var currentUserGuid = GetCurrentUserMessageId();
			List<Message> chatHistory = _context.Messages.Where(x => (x.SenderId == guid && x.ReceiverId == currentUserGuid) || (x.ReceiverId == guid && x.SenderId == currentUserGuid)).OrderBy(x => x.CreatedDate).ToList();
			ViewBag.ChatHistory = chatHistory;
			ViewBag.CurrentUser = currentUserGuid;
			ViewBag.Partner = GetMessagePartner(guid);
			return View();
		}

		private dynamic GetMessagePartner(Guid guid)
		{
			var userRole = User.Identity.AuthenticationType;
			if (userRole == "Akademisyen Claims")
			{
				Ogrenci ogrenci = _context.Ogrenci.Where(x=> x.MessageId==guid).FirstOrDefault();
				return ogrenci;
			}
			else if (userRole == "Ogrenci Claims")
			{
				AkademikPersonel akademisyen = _context.AkademikPersonel.Where(x => x.MessageId == guid).FirstOrDefault();
				return akademisyen;
			}
			else
			{
				return Guid.Empty;
			}
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
				var sentMessages = _context.Messages.Where(x => x.SenderId == currentUserMessageId && !receivedMessages.Contains(x.ReceiverId)).GroupBy(x => x.ReceiverId).Select(x => x.Key).ToList();

				receivedMessages.AddRange(sentMessages);

				return receivedMessages;
				
			}
			catch (Exception)
			{

				throw;
			}
		}
		private dynamic GetMessageUsers(List<Guid> messageUsers)
		{
			var userRole = User.Identity.AuthenticationType;
			if (userRole == "Akademisyen Claims")
			{
				return _context.Ogrenci.Where(x => messageUsers.Contains(x.MessageId));
			}
			else if (userRole == "Ogrenci Claims")
			{
				return _context.AkademikPersonel.Where(x => messageUsers.Contains(x.MessageId));
			}
			return null;
		}
	}
}