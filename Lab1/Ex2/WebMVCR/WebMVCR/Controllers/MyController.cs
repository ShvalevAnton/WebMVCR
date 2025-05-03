using Microsoft.AspNetCore.Mvc;

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
            string Greeting = hour < 12 ? "Доброе утро" : "Добрый день" + hel;
            return Greeting;
        }
    }
}
