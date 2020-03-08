using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication14.Models;

namespace WebApplication14.Controllers
{
    public class KurulKararıController : Controller
    {
		AraProjeContext p = new AraProjeContext();
        public IActionResult Index()
        {
			var model = p.ProjeAl.ToList();
            return View(model);
        }
    }
}