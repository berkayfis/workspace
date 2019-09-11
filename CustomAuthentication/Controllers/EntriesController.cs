using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BibTeXLibrary;
using CustomAuthentication.Services;
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

        public IActionResult All()
        {
            List<BibEntry> entries = _publicationService.GetAllEntriesFromFile("");
            return View(entries);
        }

        public IActionResult Detail(string tags)
        {
            JObject json = JObject.Parse(tags);
            foreach (var x in json)
            {
                string name = x.Key;
                JToken value = x.Value;
            }


            return View(json);
        }
    }
}