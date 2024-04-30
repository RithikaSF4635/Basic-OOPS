using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
namespace BankAccountOpening;
class Program
{
    public static void Main(string[] args)
    {
        List<CustomerDetails> Customers = new List<CustomerDetails>();
        string option;
        do
        {
            Console.WriteLine("Main Menu : ");
            Console.WriteLine("1. Registration");
            Console.WriteLine("2. Login");
            Console.WriteLine("3. Exit");
            int input = int.Parse(Console.ReadLine());
            CustomerDetails customer = new CustomerDetails();

            switch (input)
            {
                case 1:
                    {
                        
                        Console.WriteLine("Enter Customer ID : " + customer.CustomerID);
                        Console.Write("Enter Customer Name : ");
                        customer.CustomerName = Console.ReadLine();
                        Console.Write("Enter Bank Balance : ");
                        customer.Balance = int.Parse(Console.ReadLine());
                        Console.Write("Enter Gender : ");
                        customer.Gender =Enum.Parse<Gender>(Console.ReadLine());
                        Console.Write("Enter Phone Number : ");
                        customer.Phone =long.Parse(Console.ReadLine());
                        Console.Write("Enter Mail ID : ");
                        customer.MailID = Console.ReadLine();
                        Console.Write("Enter Date of Birth : ");
                        customer.DateOFBirth = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);

                        Customers.Add(customer);
                        break;
                    }
                case 2:
                    {
                        Console.Write("Enter Customer ID : ");
                        string loginID=Console.ReadLine();
                        bool flag=true;
                        foreach(CustomerDetails cust in Customers)
                        {
                            if (cust.CustomerID==loginID)
                            {
                                flag=false;
                                string option1;
                                do{
                                    Console.WriteLine("Sub Menu : ");
                                    Console.WriteLine("1. Deposit");
                                    Console.WriteLine("2. Withdraw");
                                    Console.WriteLine("3. Balance check");
                                    Console.WriteLine("4. Exit");
                                    int press=int.Parse(Console.ReadLine());

                                    switch(press)
                                    {
                                        case 1:
                                        {
                                            Console.Write("Enter amount : ");
                                            decimal amount=decimal.Parse(Console.ReadLine());
                                            cust.Balance=customer.Deposit(amount,cust.Balance);
                                            Console.WriteLine($"Current Balance : {cust.Balance}");
                                            break;
                                        }
                                        case 2:
                                        {
                                            Console.Write("Enter withDrawal Amount : ");
                                            decimal withdraw_amount=decimal.Parse(Console.ReadLine());
                                            cust.Balance=customer.Withdraw(withdraw_amount,cust.Balance);
                                            Console.WriteLine($"Current Balance : {cust.Balance}");
                                            break;
                                        }
                                        case 3:
                                        {
                                            Console.WriteLine($"Current Balance : {cust.Balance}");
                                            break;
                                        }
                                        case 4:
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