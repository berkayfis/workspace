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
        private readonly AraProjeContext _context;

        public KurulKararıController(AraProjeContext context)
        {
            _context = context;
        }
        
        public IActionResult Index()
        {
			var model = _context.ProjeAl.ToList();
            return View(model);
        }
    }
}