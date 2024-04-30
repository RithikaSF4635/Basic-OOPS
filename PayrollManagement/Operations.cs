using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PayrollManagement
{
    public static class Operations
    {
        static List<EmployeeDetails> employeeList=new List<EmployeeDetails>();
        static List<AttendanceDetails> attendanceList=new List<AttendanceDetails>();
        static EmployeeDetails currentLoggedInEmployee;


        //Main Menu
        public static void MainMenu()
        {
            Console.WriteLine("**********************Welcome to Syncfusion Software Private Limited*********************");
            //Need to show the main menu option
           

            string mainChoice="yes";
            do
            {
                Console.WriteLine("MainMenu\n1. Employee Registration\n2. Employee Login\n3. Exit");
                //Need to get an input from user and validate.
                Console.Write("SelectionTypes an option: ");
                int mainOption = int.Parse(Console.ReadLine());
                //Need to create mainmenu structure 
                switch (mainOption)
                {
                    case 1:
                        {
                            Console.WriteLine("***************Registration*******************");
                            Registration();
                            break;
                        }
                    case 2:
                        {
                            Console.WriteLine("***************Login**********************");
                            Login();
                            break;
                        }
                    
                    case 3:
                        {
                            
                            Console.WriteLine("Application Exited Successfully");
                            mainChoice="no";
                            break;
                        }

                }
                //Need to iterate until the option is exit.
            } while (mainChoice=="yes");
        }


        //Registration Method
        public static void Registration()
        {
            //Need to get required details
            Console.Write("Enter your Full Name : ");
            string name=Console.ReadLine();
            Console.Write("Enter your Date of Birth : ");
            DateTime dob=DateTime.ParseExact(Console.ReadLine(),"dd/MM/yyyy",null);
            Console.Write("Enter your Mobile Number : ");
            string mobileNumber=Console.ReadLine();
            Console.Write("Enter your Gender : ");
            Gender gender=Enum.Parse<Gender>(Console.ReadLine(),true);
            Console.Write("Enter your Branch : ");
            Branch branch=Enum.Parse<Branch>(Console.ReadLine(),true);
            Console.Write("Enter your Team : ");
            Team team=Enum.Parse<Team>(Console.ReadLine(),true);
            
            
            //Need to create an object
            EmployeeDetails employee=new EmployeeDetails(name,dob,mobileNumber,gender,branch,team);
            employeeList.Add(employee);
            Console.WriteLine($"Employee added successfully your id is: {employee.EmployeeID}");
        }


        //Login Method
        public static void Login()
        {
            //Need to get ID input
            Console.WriteLine("Enter your Employee ID : ");
            string loginID =Console.ReadLine().ToUpper();
            //Validate by its presence in the list
            bool flag=true;
            foreach(EmployeeDetails employee in employeeList)
            {
                if(loginID.Equals(employee.EmployeeID))
                {
                    flag=false;
                    //assigning current user to gobal variable
                    currentLoggedInEmployee=employee;
                    Console.WriteLine("Logged In Successfully");
                    //Need to call sub menu
                    SubMenu();
                    break;
                }
            }
            if(flag)
            {
                Console.WriteLine("Invalid ID or ID is not present");
            }
            //If not - Invalid ID
        }


        //SubMenu Method
        public static void SubMenu()
        {
            string subChoice="yes";
            do
            {
                Console.WriteLine("************SubMenu************");
                //Need to show sub menu option
                Console.WriteLine("Select an Option\n1. Add Attendance\n2. Display Details\n3. Calculate Salary\n4. Exit");
                //Getting user option
                Console.Write("Enter your Option : ");
                int subOption=int.Parse(Console.ReadLine());
                //Need to create sub menu structure
                switch(subOption)
                {
                    case 1:
                    {
                        Console.WriteLine("************Add Attendance************");
                        AddAttendance();
                        break;
                    }
                    case 2:
                    {
                        Console.WriteLine("**************Display Details*************");
                        DisplayDetails();
                        break;
                    }
                    case 3:
                    {
                        Console.WriteLine("*****************Calculate Salary**************");
                        CalculateSalary();
                        break;
                    }
                    
                    case 4:
                    {
                        Console.WriteLine("**************Exit****************");
                        subChoice="no";
                        break;
                    }
                }
                //Iterate till the option is exit.
            }while(subChoice=="yes");
        }


        //Add Attendance method
        public static void AddAttendance()
        {
            Console.WriteLine("Do you want to Check-in, say yes or no : ");
            string checkInOption=Console.ReadLine().ToLower();
            if (checkInOption=="yes")
            {
                Console.WriteLine("Enter Date and Time of Check-in (dd/MM/yyyy HH:mm) : ");
                DateTime checkInTime=DateTime.ParseExact(Console.ReadLine(),"dd/MM/yyyy HH:mm",null);
                
                Console.WriteLine("Do you want to Check-Out, say yes or no : ");
                string checkOutOption=Console.ReadLine().ToLower();
                DateTime checkOutTime=DateTime.MinValue;
                if (checkOutOption=="yes")
                {
                    Console.WriteLine("Enter Date and Time of Check-Out (dd/MM/yyyy HH:mm) : ");
                    checkOutTime=DateTime.ParseExact(Console.ReadLine(),"dd/MM/yyyy HH:mm",null);
                }

                double hoursWorked=(checkInTime - checkOutTime).TotalHours;
                if (hoursWorked>8)
                {
                    hoursWorked=8;
                    AttendanceDetails attendance=new AttendanceDetails(currentLoggedInEmployee.EmployeeID,DateTime.Now,checkInTime,checkOutTime,hoursWorked);
                    attendanceList.Add(attendance);
                    Console.WriteLine("Check-in and Checkout Successful and today you have worked 8 Hours");
                }
                else
                {
                    Console.WriteLine("You haven't worked for 8 hours");
                }
            }

        }

        //Display Details
        public static void DisplayDetails()
        {
            foreach (AttendanceDetails attendance in attendanceList)
            {
                if (attendance.EmployeeID==currentLoggedInEmployee.EmployeeID)
                {
                    Console.WriteLine($"|{attendance.AttendanceID}|{attendance.EmployeeID}|{attendance.Date}|{attendance.CheckInTime}|{attendance.CheckOutTime}|{attendance.HoursWorked}|");
                }
            }
        }


        //Salary Calculation
        public static void CalculateSalary()
        {
            double perDaySalary=500;
            double totalhoursWorked=0;

            int currentMonth=DateTime.Now.Month;
            foreach (AttendanceDetails attendance in attendanceList)
            {
                if (attendance.EmployeeID==currentLoggedInEmployee.EmployeeID && attendance.CheckInTime.Month==currentMonth)
                {
                    totalhoursWorked+=attendance.HoursWorked;
                }
            }

            double totalSalary=(totalhoursWorked/8)*perDaySalary;
            Console.WriteLine($"Total Salary for the current month : Rs. {totalSalary}");
        }



        //Adding Default values
        public static void AddingDefault()
        {
            EmployeeDetails employee1=new EmployeeDetails("Ravi",new DateTime(1999,11,11),"9958858888",Gender.Male,Branch.Eymard,Team.Developer);
            employeeList.Add(employee1);
            foreach (EmployeeDetails employee in employeeList)
            {
                Console.WriteLine($"|{employee.EmployeeID}|{employee.FullName}|{employee.DOB}|{employee.MobileNumber}|{employee.Gender}|{employee.Branch}|{employee.Team}|");
            }

            AttendanceDetails attendance1=new AttendanceDetails("SF3001",new DateTime(2022,05,15),new DateTime(2022,05,15,9,0,0),new DateTime(2022,05,15,18,0,0),8);
            AttendanceDetails attendance2=new AttendanceDetails("SF3002",new DateTime(2022,05,16),new DateTime(2022,05,16,9,10,0),new DateTime(2022,05,16,18,50,0),8);
            attendanceList.Add(attendance1);
            attendanceList.Add(attendance2);
            foreach (AttendanceDetails attendance in attendanceList)
            {
                Console.WriteLine($"|{attendance.AttendanceID}|{attendance.EmployeeID}|{attendance.Date}|{attendance.CheckInTime}|{attendance.CheckOutTime}|{attendance.HoursWorked}|");
            }


        }

    }
}