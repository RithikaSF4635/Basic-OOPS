using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EBbillCalculation
{
    public class UserDetails
    {
       private static int _meterID=1000;
       public string MeterID { get; set;}
       public string UserName { get; set; }
       public long Phone { get; set; }
       public string MailID { get; set; }
       public decimal UnitsUsed  { get; set; }

       public UserDetails()
       {
            _meterID++;
            MeterID="SF"+_meterID;
            _BillID++;
            BillID="SF"+_BillID;
            UnitsUsed=0;
       }

       public int CalculateAmount(int Units)
       {
        int TotalAmount=Units*5;
        return TotalAmount;
       }

       private static int _BillID=100;

       public string BillID { get; set; }

       
       
       

    }
}