using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SynCart
{
    public class CustomerDetails
    {
        private static int s_customerID=1000;
        //private double _walletBalance;
        public string CustomerID { get; }
        public string CustomerName { get; set; }
        public string City { get; set; }
        public long MobileNumber { get; set; }
        public double WalletBalance { get;set; }
        public string EmailID { get; set; }
        
        public CustomerDetails(string customerName,string city,long mobileNumber,double walletBalance,string emailID)
        {
            s_customerID++;
            CustomerID="CID"+s_customerID;
            CustomerName=customerName;
            City=city;
            MobileNumber=mobileNumber;
            WalletBalance=walletBalance;
            EmailID=emailID;
        }

        public void WalletRecharge(double amount)
        {
            WalletBalance+=amount;
        }

        public double DeductBalance(double amount)
        {
            return WalletBalance-=amount;
        }
        
        
        
        
        
        
        
        
        
        
    }
}