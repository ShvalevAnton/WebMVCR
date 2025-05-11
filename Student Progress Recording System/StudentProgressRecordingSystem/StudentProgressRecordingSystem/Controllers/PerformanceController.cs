using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentProgressRecordingSystem.Models;
using StudentProgressRecordingSystem.Data;
using System.Security.Cryptography;
using static System.Formats.Asn1.AsnWriter;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace StudentProgressRecordingSystem.Controllers
{
    /// <summary>
    /// Контроллер для отображения успеваемости студентов
    /// </summary>
    public class PerformanceController : Controller
    {
        private readonly AppDbContext _context;

        // Внедрение зависимости контекста БД
        public PerformanceController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Performance
        ///  Успеваемость всех студентов
        public async Task<IActionResult> AllStudents()
        {
            var students = await _context.Students
                .Include(s => s.Grades)
                .Select(s => new
                {
                    s.StudentId,
                    s.FullName,
                    Grades = s.Grades.Select(g => g.Score).ToList(),
                    AverageGrade = s.Grades.Any() ? s.Grades.Average(g => g.Score) : 0
                })
                .OrderByDescending(s => s.AverageGrade)
                .ToListAsync();

            ViewBag.Students = students;
            ViewBag.Title = "Все студенты";
            return View();
        }

        // Топ-5 лучших студентов
        public async Task<IActionResult> TopStudents()
        {
            var topStudents = await _context.Students
                .Include(s => s.Grades)
                .Select(s => new
                {
                    s.StudentId,
                    s.FullName,
                    Grades = s.Grades.Select(g => g.Score).ToList(),
                    AverageGrade = s.Grades.Any() ? s.Grades.Average(g => g.Score) : 0
                })
                .OrderByDescending(s => s.AverageGrade)
                .Take(5)
                .ToListAsync();

            ViewBag.Students = topStudents;
            ViewBag.Title = "Топ-5 лучших студентов";
            return View("TopStudents");
        }

        // Топ-5 худших студентов
        public async Task<IActionResult> BottomStudents()
        {
            var bottomStudents = await _context.Students
                .Include(s => s.Grades)
                .Select(s => new
                {
                    s.StudentId,
                    s.FullName,
                    Grades = s.Grades.Select(g => g.Score).ToList(),
                    AverageGrade = s.Grades.Any() ? s.Grades.Average(g => g.Score) : 0
                })
                .OrderBy(s => s.AverageGrade)
                .Take(5)
                .ToListAsync();

            ViewBag.Students = bottomStudents;
            ViewBag.Title = "Топ-5 худших студентов";
            return View("BottomStudents");
        }

        // Добавим новый метод в PerformanceController
        public IActionResult ExportToText()
        {
            // Получаем данные о студентах и их успеваемости
            var students = _context.Students
                .Include(s => s.Grades)
                    .ThenInclude(g => g.Subject)
                .Select(s => new
                {
                    s.StudentId,
                    s.FullName,
                    AverageGrade = s.Grades.Any() ? s.Grades.Average(g => g.Score) : 0,
                    SubjectsCount = s.Grades.Select(g => g.SubjectId).Distinct().Count()
                })
                .OrderByDescending(x => x.AverageGrade)
                .ToList();

            // Создаем содержимое файла
            var sb = new StringBuilder();
            sb.AppendLine("Список студентов и их успеваемость");
            sb.AppendLine("==================================");
            sb.AppendLine();
            sb.AppendLine($"Дата формирования: {DateTime.Now:dd.MM.yyyy HH:mm}");
            sb.AppendLine();
            sb.AppendLine("№ | ФИО студента | Количество предметов | Средний балл");
            sb.AppendLine("--|--------------|----------------------|-------------");

            int counter = 1;
            foreach (var student in students)
            {
                sb.AppendLine($"{counter++} | {student.FullName} | {student.SubjectsCount} | {student.AverageGrade:F2}");
            }

            sb.AppendLine();
            sb.AppendLine($"Всего студентов: {counter}");
            sb.AppendLine($"Средний балл по всем: {students.Average(s => s.AverageGrade):F2}");

            // Преобразуем в массив байтов
            var byteArray = Encoding.UTF8.GetBytes(sb.ToString());

            // Возвращаем файл для скачивания
            return File(byteArray, "text/plain", $"students_report_{DateTime.Now:yyyyMMdd_HHmm}.txt");
        }

        public IActionResult ExportToJson()
        {
            // Получаем данные о студентах и их успеваемости
            var students = _context.Students
                .Include(s => s.Grades)
                    .ThenInclude(g => g.Subject)
                .Select(s => new
                {
                    s.StudentId,
                    s.FullName,
                    AverageGrade = s.Grades.Any() ? s.Grades.Average(g => g.Score) : 0,
                    Subjects = s.Grades
                        .GroupBy(g => g.Subject)
                        .Select(g => new
                        {
                            SubjectName = g.Key.Name,
                            Grades = g.Select(gr => gr.Score).ToArray(),
                            SubjectAverage = g.Average(gr => gr.Score)
                        })
                        .OrderByDescending(s => s.SubjectAverage)
                        .ToList()
                })
                .OrderByDescending(x => x.AverageGrade)
                .ToList();

            // Настройки сериализации
            var options = new JsonSerializerOptions
            {
                WriteIndented = true, // Красивое форматирование
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // camelCase для свойств
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull, // Игнорировать null
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping // Безопасные символы
            };

            // Сериализуем в JSON
            var json = JsonSerializer.Serialize(students, options);

            // Возвращаем файл для скачивания
            return File(
                Encoding.UTF8.GetBytes(json),
                "application/json",
                $"students_report_{DateTime.Now:yyyyMMdd_HHmm}.json");
        }
    }
}
