using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MvcCreditApp.Models;

namespace MvcCreditApp.Data
{
    // Класс контекста данных (он нужен для облегчения доступа к БД на основе модели)
    // Этот класс контекста представляет полный слой данных, который можно использовать в приложениях.
    // Благодаря DbContext, можно запросить, изменить, удалить или вставить значения в базу данных.
    public class CreditContext : DbContext
    {
        // Конструктор класса
        // Конструктор принимает DbContextOptions<CreditContext> — это настройки контекста (например, строка подключения к БД).
        // base(options) передаёт эти настройки в родительский класс(DbContext)
        public CreditContext(DbContextOptions<CreditContext> options): base(options){ }
        public DbSet<Credit> Credits { get; set; }
        public DbSet<Bid> Bids { get; set; }
    }
}
