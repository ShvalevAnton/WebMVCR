using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcCreditApp.Data;
using MvcCreditApp.Models;

namespace MvcCreditApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly CreditContext db;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, CreditContext context)
        {
            _logger = logger;
            db = context;
        }

        public IActionResult Index()
        {
            GiveCredits();
            return View();
        }

        private void GiveCredits()
        {
            var allCredits = db.Credits.ToList<Credit>();
            ViewBag.Credits = allCredits;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult CreateBid()
        {
            GiveCredits();
            var allBids = db.Bids.ToList<Bid>(); 
            ViewBag.Bids = allBids; 
            return View();
        }

        [HttpPost]
        public string CreateBid(Bid newBid)
        {
            newBid.bidDate = DateTime.Now;
            // Добавляем новую заявку в БД 
            db.Bids.Add(newBid);
            // Сохраняем в БД все изменения 
            db.SaveChanges();
            return "Спасибо, " + newBid.Name + ", за выбор нашего банка. Ваша заявка будет рассмотрена в течении 10 дней.";
        }      


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // Метод извлечения из БД нужной информации в частичное представление
        public ActionResult BidSearch(string name)
        {
            // Поиск в таблице Bids записей, где CreditHead содержит строку name
            var allBids = db.Bids.Where(a => a.CreditHead.Contains(name)).ToList();
            // Проверка, найдены ли результаты
            if (allBids.Count == 0)
            {
                //Если нет результатов - возвращаем текстовое сообщение
                return Content("Указанный кредит " + name + " не найден");
                //return HttpNotFound();
            }
            // Если результаты есть - возвращаем частичное представление с данными
            return PartialView(allBids);
        }

    }
}
