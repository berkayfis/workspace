using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CustomAuthentication.Models.Publications;


namespace CustomAuthentication.Controllers
{
    public class PublicationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SaveArticle(Article article)
        {
            //Insert DB and show the related page 
            return View();
        }
    }
}