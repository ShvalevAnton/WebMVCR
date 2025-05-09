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
            // ��������� ����� ������ � �� 
            db.Bids.Add(newBid);
            // ��������� � �� ��� ��������� 
            db.SaveChanges();
            return "�������, " + newBid.Name + ", �� ����� ������ �����. ���� ������ ����� ����������� � ������� 10 ����.";
        }      


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // ����� ���������� �� �� ������ ���������� � ��������� �������������
        public ActionResult BidSearch(string name)
        {
            // ����� � ������� Bids �������, ��� CreditHead �������� ������ name
            var allBids = db.Bids.Where(a => a.CreditHead.Contains(name)).ToList();
            // ��������, ������� �� ����������
            if (allBids.Count == 0)
            {
                //���� ��� ����������� - ���������� ��������� ���������
                return Content("��������� ������ " + name + " �� ������");
                //return HttpNotFound();
            }
            // ���� ���������� ���� - ���������� ��������� ������������� � �������
            return PartialView(allBids);
        }

    }
}
