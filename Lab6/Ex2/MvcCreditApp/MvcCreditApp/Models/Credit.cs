using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MvcCreditApp.Models
{
    // Summary:
    //     Класс реализует модель данных о кредите.
    public class Credit
    {
        // ID кредита
        public virtual int CreditId { get; set; }
        // Название
        [DisplayName("Название")]
        [Required(ErrorMessage = "Название обязательно")]
        [StringLength(100, ErrorMessage = "Слишком длинное название")]
        public virtual string Head { get; set; }
        // Период, на который выдается кредит
        [DisplayName("Период")]
        [Range(1, 60, ErrorMessage = "Период должен быть 1-60 месяцев")]
        public virtual int Period { get; set; }
        // Максимальная сумма кредита
        [DisplayName("Максимальная сумма кредита")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public virtual int Sum { get; set; }
        // Процентная ставка
        [Range(0, 100, ErrorMessage = "Процент должен быть 0-100")]
        [DisplayName("Процентная ставка (%)")]
        public virtual int Procent { get; set; }
    }
}
