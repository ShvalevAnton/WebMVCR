using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MvcCreditApp.Models
{
    // Класс реализует модель данных о заявке на кредит.
    public class Bid
    {
        // ID заявки 
        public virtual int BidId { get; set; }
        // Имя заявителя
        [Required(ErrorMessage = "Обязательное поле")]
        [DisplayName("Имя заявителя")]
        [StringLength(100, ErrorMessage = "Не более 100 символов")]
        public virtual string Name { get; set; }
        // Название кредита 
        [DisplayName("Название кредита")]
        public virtual string CreditHead { get; set; }
        // Дата подачи заявки 
        [Required]
        [DisplayName("Дата подачи")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm}")]
        public virtual DateTime bidDate { get; set; }
    }
}
