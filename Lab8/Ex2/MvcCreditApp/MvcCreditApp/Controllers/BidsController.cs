using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;
using MvcCreditApp.Data;
using MvcCreditApp.Models;

namespace MvcCreditApp.Controllers
{
    public class BidsController : Controller
    {
        private readonly CreditContext _context;

        public BidsController(CreditContext context)
        {
            _context = context;
        }

        // GET: Bids
        // Отображает список всех заявок
        [OutputCache(Duration = 60)]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Bids.ToListAsync());
        }

        // GET: Bids/Details/5
        // Показывает детали конкретной заявки
        public async Task<IActionResult> Details(int? id)
        {
            // Проверяет наличие ID
            if (id == null)
            {
                return NotFound();
            }
            // Ищет заявку по ID
            var bid = await _context.Bids.FirstOrDefaultAsync(m => m.BidId == id);

            // Если не находит - возвращает NotFound
            if (bid == null)
            {
                return NotFound();
            }
            // Иначе возвращает представление с найденной заявкой
            return View(bid);
        }

        // GET: Bids/Create
        // Просто возвращает пустое представление для создания новой заявки
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bids/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BidId,Name,CreditHead,bidDate")] Bid bid)
        {
            // Если модель валидна - добавляет в БД и сохраняет изменения
            if (ModelState.IsValid)
            {
                _context.Add(bid);
                await _context.SaveChangesAsync();
                // Перенаправляет на Index
                return RedirectToAction(nameof(Index));
            }
            // Если модель невалидна - возвращает представление с ошибками
            return View(bid);
        }

        // GET: Bids/Edit/5
        // Находит заявку по ID и возвращает представление для редактирования
        public async Task<IActionResult> Edit(int? id)
        {
            // Проверяет, был ли передан ID (не null)
            if (id == null)
            {
                return NotFound();
            }
            // Асинхронно ищет заявку в базе данных по первичному ключу (ID)
            var bid = await _context.Bids.FindAsync(id);

            // Если заявка не найдена, возвращает NotFound (HTTP 404)
            if (bid == null)
            {
                return NotFound();
            }
            // Если заявка найдена, возвращает представление Edit, передавая найденную заявку
            return View(bid);
        }

        // POST: Bids/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BidId,Name,CreditHead,bidDate")] Bid bid)
        {
            //Сравнивает ID из URL с BidId из формы.
            if (id != bid.BidId)
            {
                // Если не совпадают → возвращает 404 Not Found(защита от подмены данных).
                return NotFound();
            }

            // Проверяет, прошла ли модель валидацию (например, обязательные поля заполнены, корректные форматы данных).
            if (ModelState.IsValid)
            {
                try
                {
                    // Обновление данных
                    _context.Update(bid);
                    // Асинхронно отправляет изменения в базу данных
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BidExists(bid.BidId))
                    {
                        // 404 Not Found
                        return NotFound(); // Запись удалили другим пользователем из БД после загрузки формы → возвращает NotFound.
                    }
                    else
                    {
                        throw; // Если ошибка из-за параллельного редактирования
                    }
                }
                // При успешном обновлении — перенаправляет на страницу списка заявок (Index).
                return RedirectToAction(nameof(Index));
            }
            // Возвращает ту же форму с подсвеченными ошибками.
            return View(bid);
        }

        // GET: Bids/Delete/5
        // Находит заявку по ID и возвращает представление с подтверждением удаления
        public async Task<IActionResult> Delete(int? id)
        {
            // Проверка наличия ID
            if (id == null)
            {
                return NotFound();
            }

            // Поиск заявки в базе данных
            var bid = await _context.Bids.FirstOrDefaultAsync(m => m.BidId == id);

            // Проверка существования заявки
            if (bid == null)
            {
                // Отображение 404 Not Found
                return NotFound();
            }
            // Отображение страницы подтверждения
            return View(bid);
        }

        // POST: Bids/Delete/5
        [HttpPost, ActionName("Delete")] //  позволяет использовать URL /Bids/Delete/5 вместо /Bids/DeleteConfirmed/5
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bid = await _context.Bids.FindAsync(id);
            if (bid != null)
            {
                _context.Bids.Remove(bid);
            }
            //  асинхронно применяет изменения в БД
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BidExists(int id)
        {
            return _context.Bids.Any(e => e.BidId == id);
        }
    }
}
