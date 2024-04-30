using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLibraryManagement
{
    public enum Gender{Select, Female, Male,Tansgender}
    public enum Department{ECE,EEE,CSC}
    public class UserDetails
    {
        /*
        a.	UserID (Auto Increment – SF3000)
        b.	UserName
        c.	Gender
        d.	Department – (Enum – ECE, EEE, CSE)
        e.	MobileNumber
        f.	MailID
        */
        private double _walletBalance;
        private static int s_userID=3000;
        public string UserID { get; }
        public string UserName{ get; set; }
        public Gender Gender { get; set; }
        public Department Department { get; set; }
        public string MobileNumber { get; set; }
        public string MailID { get; set; }
        public double WalletBalance { get{return _walletBalance ;}  }
        
        //method Wallet Recharge
        public  void WalletRecharge(double amount)
        {
            _walletBalance+=amount;
        }
        //method deduct Balance
        public  void DeductBalance(double amount)
        {
            _walletBalance-=amount;
        }

        public UserDetails(string userName,Gender gender,Department department,string mobileNumber,string mailID)
        {
            s_userID++;
            UserID="SF"+s_userID;
            UserName=userName;
            Gender=gender;
            Department=department;
            MobileNumber=mobileNumber;
            MailID=mailID;
        }
        
        
        
        
        
        
        
        
        
        

    }
    
}