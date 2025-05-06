// Создание экземпляра компановщика WebApplicationBuilder, который собирает настройки конфигурирование, журналирование, сервисы, размещение будущего приложения
var builder = WebApplication.CreateBuilder(args);

// Регистрирование необходимых служб и конфигурации с помощью WebApplicationBuilder в OS
builder.Services.AddControllersWithViews();

// Вызов метода Build() в экземпляре компоновщика, чтобы создать экземпляр WebApplication
var app = builder.Build();

// Добавление промежуточного ПО в веб-приложение, чтобы создать конвейер;



// Сопоставление конечных точек приложения;
//app.MapGet("/", () => "Hello World!");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
//MapDefaultControllerRoute(). // Вышеуказанный вариант может заменить универсальный метод

// Вызов метода Run() в веб-приложении, чтобы запустить сервер и начинаем слушать запросы
app.Run();
