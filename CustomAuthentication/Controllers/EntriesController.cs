using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BibTeXLibrary;
using CustomAuthentication.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace CustomAuthentication.Controllers
{
    public class EntriesController : Controller
    {
        private readonly PublicationService _publicationService;

        public EntriesController(PublicationService publicationService)
        {
            _publicationService = publicationService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult All(string path)
        {
            List<BibEntry> entries = _publicationService.GetAllEntriesFromFile(path);
            return View(entries);
        }

        [HttpPost]
        public IActionResult Index(IFormFile file)
        {
            var filepath = Path.GetTempFileName();

            using (var stream = new FileStream(filepath,FileMode.Create))
            {
                file.CopyTo(stream);
            }
            
            return RedirectToAction("All","Entries", new { path = filepath });
        }

        public IActionResult Detail(string tags)
        {
            JObject json = JObject.Parse(tags);

            return View(json);
        }
    }
}