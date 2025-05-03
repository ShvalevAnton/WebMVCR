namespace WebMVCR.Models
{
    public enum AccountType { 
        Checking,   // Текуий расчетный счёт
        Deposit     // Вклад
    }
    public struct BankAccount { 
        public long accNo; 
        public decimal accBal; 
        public AccountType accType;

        public override string ToString() 
        {
            string res = String.Format($"Номер счета {accNo}, баланс {accBal}, тип {accType}"); 
            return res; 
        }
    }
    public class StudyCsharp_
    {

    }
}
