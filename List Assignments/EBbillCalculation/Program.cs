using System;
namespace EBbillCalculation;
class Program
{
    public static void Main(string[] args)
    {
        List<UserDetails> Users = new List<UserDetails>();
        string option;
        do
        {
            Console.WriteLine("Main Menu : ");
            Console.WriteLine("1. Registration");
            Console.WriteLine("2. Login");
            Console.WriteLine("3. Exit");
            int input = int.Parse(Console.ReadLine());
            UserDetails user = new UserDetails();

            switch (input)
            {
                case 1:
                    {
                        
                        Console.WriteLine("Enter Meter ID : " + user.MeterID);
                        Console.Write("Enter User Name : ");
                        user.UserName = Console.ReadLine();
                        Console.Write("Enter Phone Number : ");
                        user.Phone =long.Parse(Console.ReadLine());
                        Console.Write("Enter Mail ID : ");
                        user.MailID = Console.ReadLine();

                        Users.Add(user);
                        break;
                    }
                case 2:
                    {
                        Console.Write("Enter Meter ID : ");
                        string loginID=Console.ReadLine();
                        bool flag=true;
                        foreach(UserDetails user1 in Users)
                        {
                            if (user1.MeterID==loginID)
                            {
                                flag=false;
                                string option1;
                                do{
                                    Console.WriteLine("Sub Menu : ");
                                    Console.WriteLine("1. Calculate Amount");
                                    Console.WriteLine("2. Display User Details");
                                    Console.WriteLine("3. Exit");
                                    int press=int.Parse(Console.ReadLine());

                                    switch(press)
                                    {
                                        case 1:
                                        {
                                            Console.Write("Enter Units Used : ");
                                            int units=int.Parse(Console.ReadLine());
                                            Console.WriteLine("Bill ID : "+ user1.BillID);
                                            Console.WriteLine("User Name : "+ user1.UserName);
                                            Console.WriteLine($"EB Bill Amount : {user.CalculateAmount(units)}");
                                            break;
                                        }
                                        case 2:
                                        {
                                            Console.WriteLine($"Meter ID : {user1.MeterID}");
                                            Console.WriteLine($"User Name : {user1.UserName}");
                                            Console.WriteLine($"Phone Number : {user1.Phone}");
                                            Console.WriteLine($"Mail ID : {user1.MailID}");
                                            break;
                                        }
                                        
                                        case 3:
                                        {
                                            break;
                                        }

                                        
                                    }
                                    Console.Write("Do you want to return to sub menu ? ");
                                    option1= Console.ReadLine();

                                }while(option1=="yes");
                            }
                            
                        }
                        if(flag)
                        {
                            Console.WriteLine("Invalid Meter ID");
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