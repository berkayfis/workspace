using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication14.Controllers
{
    public class PdfViewerController : Controller
    {
        public IActionResult Index(string path)
        {
            return new PhysicalFileResult(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path), "application/pdf");
        }
    }
}