using System.ComponentModel.DataAnnotations;

namespace StudentProgressRecordingSystem.Models
{
    public class Grade
    {
        // Уникальный идентификатор оценки (первичный ключ)
        public int GradeId { get; set; }

        [Display(Name = "Оценка")]
        [Range(1, 5)]
        public int Score { get; set; }

        [Display(Name = "Дата оценки")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Display(Name = "Комментарий")]
        public string Comments { get; set; }

        // Внешний ключ и навигационное свойство для студента
        public int StudentId { get; set; }
        public Student Student { get; set; }

        // Внешний ключ и навигационное свойство для предмета
        public int SubjectId { get; set; }
        [Display(Name = "Предмет")]
        public Subject Subject { get; set; }
    }
}
