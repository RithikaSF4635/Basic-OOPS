using System;
using System.Collections.Generic;
namespace EmployeePlayRollManagement;
class Program
{
    public static void Main(string[] args)
    {
        List<EmployeeDetails> Employees=new List<EmployeeDetails>();

        string option;
        do
        {
            Console.WriteLine("Main Menu : ");
            Console.WriteLine("1. Registration");
            Console.WriteLine("2. Login");
            Console.WriteLine("3. Exit");
            int input = int.Parse(Console.ReadLine());
            EmployeeDetails employee = new EmployeeDetails();

            switch (input)
            {
                case 1:
                    {
                        
                        Console.WriteLine("Enter Employee ID : " + employee.EmployeeID);
                        Console.Write("Enter Employee Name : ");
                        employee.EmployeeName = Console.ReadLine();
                        Console.Write("Enter Role : ");
                        employee.Role=Console.ReadLine();
                        Console.Write("Enter WorkLocation : ");
                        employee.WorkLocation =Enum.Parse<Location>(Console.ReadLine());
                        Console.Write("Enter Team Name : ");
                        employee.TeamName =Console.ReadLine();
                        Console.Write("Enter DateOfJoining : ");
                        employee.DateOfJoining= DateTime.ParseExact(Console.ReadLine(),"dd/MM/yyyy",null);
                        Console.Write("Number of Working Days in Month : ");
                        employee.WorkingDays=int.Parse(Console.ReadLine());
                        Console.Write("Number of Days Leave Taken : ");
                        employee.LeaveTaken=int.Parse(Console.ReadLine());
                        Console.Write("Enter Gender : ");
                        employee.Gender = Enum.Parse<Gender>(Console.ReadLine());

                        Employees.Add(employee);
                        break;
                    }
                case 2:
                    {
                        Console.Write("Enter Employee ID : ");
                        string loginID=Console.ReadLine();
                        bool flag=true;
                        foreach(EmployeeDetails emp in Employees)
                        {
                            if (emp.EmployeeID==loginID)
                            {
                                flag=false;
                                string option1;
                                do{
                                    Console.WriteLine("Sub Menu : ");
                                    Console.WriteLine("1. Calculate Salary");
                                    Console.WriteLine("2. Display Details");
                                    Console.WriteLine("3. Exit");
                                    int press=int.Parse(Console.ReadLine());

                                    switch(press)
                                    {
                                        case 1:
                                        {
                                            Console.WriteLine($"Salary : {employee.SalaryCalculation(emp.WorkingDays,emp.LeaveTaken)}");
                                            break;
                                        }
                                        case 2:
                                        {
                                            Console.WriteLine($"Employee ID : {emp.EmployeeID}");
                                            Console.WriteLine($"Employee Name : {emp.EmployeeName}");
                                            Console.WriteLine($"Role : {emp.Role}");
                                            Console.WriteLine($"Work Location : {emp.WorkLocation}");
                                            Console.WriteLine($"Team Name : {emp.TeamName}");
                                            Console.WriteLine($"Date of Joining : {emp.DateOfJoining}");
                                            Console.WriteLine($"Number of Working Days in Month : {emp.WorkingDays}");
                                            Console.WriteLine($"Number of Leave Taken : {emp.LeaveTaken}");
                                            Console.WriteLine($"Gender : {emp.Gender}");
                                            break;
                                        }
                                       
                                        case 3:
                                        {
                                            break;
                                        }

                                        
                                    }
                                    Console.Write("Do you want to return to sub menu ? ");
                                    option1=Console.ReadLine();

                                }while(option1=="yes");
                            }
                           
                        }
                        if(flag)
                        {
                            Console.WriteLine("Invalid User ID");
                        }
                        

                        break;
                    }
                case 3:
                    {
                        break;
                    }

            }
            Console.WriteLine("Do you want to return to main menu ?");
            option = Console.ReadLine();
        } while (option == "yes");
    }
}