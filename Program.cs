using System;

namespace LabTask10b
{
    public class PriceExceedsBalanceException : Exception
    {
        public PriceExceedsBalanceException(string message) : base(message)
        {
        }
    }
    public class NegativeAmountException : ArgumentOutOfRangeException
    {
        public NegativeAmountException(string paramName) : base(paramName, "Amount cannot be negative.")
        {
        }
    }
    abstract class Coffeeshop
    {
        private string p_name;
        private double m_balance;
        private bool isFrozen = false;

        public void setName(string name)
        {
            this.p_name = name;
        }

        public string getName()
        {
            return p_name;
        }

        public void setBalance(double balance)
        {
            this.m_balance = balance;
        }

        public double getBalance()
        {
            return m_balance;
        }

        public bool IsFrozen
        {
            get { return isFrozen; }
            set { isFrozen = value; }
        }

        public abstract void Purchase(double amount);

        public abstract void Amountavailable(double amount);

        protected void CheckAccountStatus(double amount)
        {
            if (IsFrozen)
            {
                throw new Exception("Account is frozen");
            }

            if (amount < 0)
            {
                throw new NegativeAmountException("Amount");
            }
        }
    }

    class Frozen : Coffeeshop
    {
        public override void Amountavailable(double amount)
        {
            CheckAccountStatus(amount);
            setBalance(getBalance() - amount);
        }

        public override void Purchase(double amount)
        {
            throw new NotImplementedException();
        }
    }

    class RegularCoffeeShop : Coffeeshop
    {
        public override void Amountavailable(double amount)
        {
            throw new NotImplementedException();
        }

        public override void Purchase(double amount)
        {
            CheckAccountStatus(amount);

            if (getBalance() < amount)
            {
                throw new PriceExceedsBalanceException("Price exceeds balance");
            }

            setBalance(getBalance() - amount);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // Example usage
            RegularCoffeeShop regularShop = new RegularCoffeeShop();
            regularShop.setName("Regular Coffee Shop");
            regularShop.setBalance(-100.0);

            Frozen frozenShop = new Frozen();
            frozenShop.setName("Frozen Coffee Shop");
            frozenShop.setBalance(200.0);

            try
            {
                regularShop.Purchase(20.0);
                Console.WriteLine($"{regularShop.getName()} Purchase Successful. Remaining Balance: {regularShop.getBalance()}");

                frozenShop.Amountavailable(25.0);
                Console.WriteLine($"{frozenShop.getName()} Purchase Successful. Remaining Balance: {frozenShop.getBalance()}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            Console.ReadLine();
        }
        
    }
    
}
