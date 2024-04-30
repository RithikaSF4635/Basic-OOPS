using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAccountOpening
{
    public enum Gender
    {
        Male,
        Female,
        Transgender
    }
    /// <summary>
    /// This class is for customer details
    /// </summary>
    public class CustomerDetails
    {
       private static int _customerID=1000;
       public string CustomerID { get; set;}
       public string CustomerName { get; set; }
       public decimal Balance { get; set; }
       public Gender Gender { get; set; }
       public long Phone { get; set; }
       public string MailID { get; set; }
       public DateTime DateOFBirth  { get; set; }

       public CustomerDetails()
       {
            _customerID++;
            CustomerID="SF"+_customerID;
       }

       public decimal Deposit(decimal amount,decimal Balance)
       {
            Balance+=amount;
            return Balance;
       }

       public decimal Withdraw(decimal withdraw_amount,decimal Balance)
       {
            Balance=Balance-withdraw_amount;
            return Balance;
       }
    
    }

    
}