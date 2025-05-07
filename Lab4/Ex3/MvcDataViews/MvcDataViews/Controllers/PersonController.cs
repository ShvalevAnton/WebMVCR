using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcDataViews.Models;

namespace MvcDataViews.Controllers
{
    public class PersonController : Controller
    {
        static List<Person> people = new List<Person>();    // Список объектов Person
        // GET: PersonController
        public ActionResult Index()
        {
            return View(people);
        }

        // GET: PersonController/Details/5
        public ActionResult Details(Person p)
        {
            return View(p);
        }

        // GET: PersonController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PersonController/Create
        [HttpPost]  //указывает, что этот метод срабатывает только на HTTP POST запрос (отправка формы).
        [ValidateAntiForgeryToken]  // защита от CSRF-атак (проверяет, что форма была отправлена с вашего сайта, а не поддельного).
        public ActionResult Create(Person p)
        {
            try
            {
                // Проверка валидности модели
                if (!ModelState.IsValid) 
                {
                    // Если есть ошибки – возвращается та же форма (Create) с переданными данными (p), чтобы пользователь мог исправить ошибки.
                    return View("Create", p);
                }
                people.Add(p);
                // После успешного добавления – перенаправляет на метод Index (список всех людей).
                return RedirectToAction("Index");
            }
            catch
            {
                // Если произошла ошибка (например, исключение при добавлении) – возвращается пустая форма Create.
                return View();
            }
        }

        // GET: PersonController/Edit/5
        public ActionResult Edit(int id)
        {
            Person p = new Person();
            foreach (Person pn in people)
            {
                if (pn.Id == id)
                {
                    p.Name = pn.Name;
                    p.Age = pn.Age;
                    p.Id = pn.Id;
                    p.Phone = pn.Phone;
                    p.Email = pn.Email;
                }
            }
            return View(p);
        }

        // POST: PersonController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Person p)
        {            
            if (!ModelState.IsValid)
            {
                return View("Edit", p);
            }
            foreach (Person pn in people)
            {
                if (pn.Id == p.Id)
                {
                    pn.Name = p.Name;
                    pn.Age = p.Age;
                    pn.Id = p.Id;
                    pn.Phone = p.Phone;
                    pn.Email = p.Email;
                }                    
            }
            return RedirectToAction("Index");            
        }

        // GET: PersonController/Delete/5
        public ActionResult Delete(int id)
        {
            Person p = new Person();
            foreach (Person pn in people)
            {
                if (pn.Id == id)
                {
                    p.Name = pn.Name;
                    p.Age = pn.Age;
                    p.Id = pn.Id;
                    p.Phone = pn.Phone;
                    p.Email = pn.Email;
                    return View(p);
                }
            }
            return RedirectToAction("Index");
        }

        // POST: PersonController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Person p)
        {
            try
            {
                // Поиск и удаление человека из коллекции
                foreach (Person pn in people)
                {
                    if (pn.Id == p.Id) 
                    {
                        people.Remove(pn);
                    }
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
