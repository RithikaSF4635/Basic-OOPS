using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePlayRollManagement
{
    public enum Gender
    {
        Male,
        Female,
        Transgender
    }

    public enum Location
    {
        Chennai,
        USA,
        Bangalore
    }
    public class EmployeeDetails
    {
       private static int _employeeID=1000;
       public string EmployeeID { get; set;}
       public string EmployeeName { get; set; }
       public string Role { get; set; }
       
       public Location WorkLocation { get; set; }
       
       public string TeamName { get; set; }
       public DateTime DateOfJoining  { get; set; }
       public int WorkingDays { get; set; }
       
       public int LeaveTaken { get; set; }
       
       
       public Gender Gender { get; set; }

       public EmployeeDetails()
       {
            _employeeID++;
            EmployeeID="SF"+_employeeID;
       }

       public decimal SalaryCalculation(int WorkingDaysInMonth,int LeaveTaken)
       {
            int TotalDays=WorkingDaysInMonth-LeaveTaken;
            decimal salary=TotalDays*500;
            return salary;
       }
    }
}