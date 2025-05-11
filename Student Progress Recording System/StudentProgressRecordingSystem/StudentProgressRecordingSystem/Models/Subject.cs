using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace StudentProgressRecordingSystem.Models
{
    public class Subject
    {

        public int SubjectId { get; set; }

        [Required]
        [Display(Name = "Название предмета")]
        public string Name { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        // Навигационное свойство для оценок по этому предмету
        public ICollection<Grade> Grades { get; set; }
    }
}
