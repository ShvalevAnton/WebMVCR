using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace StudentProgressRecordingSystem.Models
{
    public class Student
    {

        public int StudentId { get; set; }

        [Required]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Display(Name = "Отчество")]
        public string MiddleName { get; set; }

        [Display(Name = "Группа")]
        public string Group { get; set; }

        [Display(Name = "Дата зачисления")]
        [DataType(DataType.Date)]
        public DateTime EnrollmentDate { get; set; }
                
        // Полное имя студента (вычисляемое свойство)
        [Display(Name = "ФИО")]
        public string FullName => $"{LastName} {FirstName} {MiddleName}".Trim();

        public ICollection<Grade> Grades { get; set; }

    }
}
