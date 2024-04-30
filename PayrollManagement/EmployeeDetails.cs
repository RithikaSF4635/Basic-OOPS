using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayrollManagement
{
    public enum Gender{Select,Female,Male,Transgender}
    public enum Branch{Eymard,Karuna,Madhura}
    public enum Team{Network,Hardware,Developer,Facility}
    public class EmployeeDetails
    {
        /*
        Properties:
        •	EmployeeID – (Auto Generated- SF3000)
        •	Full Name
        •	DOB
        •	MobileNumber
        •	Gender
        •	Branch – (Enum – Eymard, Karuna, Madhura)
        •	Team – (Network, Hardware, Developer, Facility)
        */

        private static int s_employeeID=3000;
        public string EmployeeID { get;}
        public string FullName { get; set; }
        public DateTime DOB { get; set; }
        public string MobileNumber { get; set; }
        public Gender Gender { get; set; }
        public Branch Branch { get; set; }
        public Team Team { get; set; }
        
        
        public EmployeeDetails(string fullName,DateTime dob,string mobileNumber,Gender gender,Branch branch,Team team)
        {
            s_employeeID++;
            EmployeeID="SF"+s_employeeID;
            FullName=fullName;
            DOB=dob;
            MobileNumber=mobileNumber;
            Gender=gender;
            Branch=branch;
            Team=team;
        }
        
        
        
        
        
        
        
    }
}