using Microsoft.AspNetCore.Mvc;
using WebMVCR.Models;

namespace WebMVCR.Controllers
{
    public class MyController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}
        public string Index(string hel)
        {
            int hour = DateTime.Now.Hour;
            string Greeting = ModelClass.ModelHello() + ", " + hel;
            return Greeting;
        }
    }
}
