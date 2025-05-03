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
            string res = String.Format("Номер счета {0}, баланс {1}, тип {2}", accNo, accBal, accType); 
            return res; 
        }
    }
    public class StudyCsharp_
    {

    }
}
