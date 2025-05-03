using Microsoft.AspNetCore.Mvc;
using WebMVCR.Models;

namespace WebMVCR.Controllers
{
    public class HomeController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}
        public string Index()
        {           
            string Greeting = ModelClass.ModelHello();
            return Greeting;
        }
    }
}
