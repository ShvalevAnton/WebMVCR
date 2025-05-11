namespace StudentProgressRecordingSystem.Models
{
    /// <summary>
    /// ViewModel для отображения успеваемости
    /// </summary>
    public class Performance
    {
        public int StudentId { get; set; } // ID студента
        public string FullName { get; set; } // Полное имя
        public List<int> Grades { get; set; } // Список оценок
        public double AverageGrade { get; set; } // Средний балл
    }
}
