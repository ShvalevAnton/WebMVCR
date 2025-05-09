namespace MvcCreditApp.Models
{
    // Класс реализует модель данных о заявке на кредит.
    public class Bid
    {
        // ID заявки 
        public virtual int BidId { get; set; } 
        // Имя заявителя
        public virtual string Name { get; set; } 
        // Название кредита 
        public virtual string CreditHead { get; set; } 
        // Дата подачи заявки 
        public virtual DateTime bidDate { get; set; }
    }
}
