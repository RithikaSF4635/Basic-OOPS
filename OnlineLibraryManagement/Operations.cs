using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace OnlineLibraryManagement
{
    public static class Operations
    {
        static List<UserDetails> userList=new List<UserDetails>();
        static List<BookDetails> bookList=new List<BookDetails>();
        static List<BorrowDetails> borrowList=new List<BorrowDetails>();

        static UserDetails currentLoggedInUser;

        public static void MainMenu()
        {
            string mainChoice="yes";
            do
            {
                Console.WriteLine("Online Library management and Book Tracking");
                Console.WriteLine("Main Menu : \n1. UserRegistration\n2. User Login\n3. Exit");
                Console.Write("Enter a option : ");
                int option=int.Parse(Console.ReadLine());
                switch(option)
                {
                    case 1:
                    {
                        Console.WriteLine("************User Registration***********");
                        UserRegistration();
                        break;
                    }
                    case 2:
                    {
                        Console.WriteLine("**************User Login**************");
                        UserLogin();
                        break;
                    }
                    case 3:
                    {
                        Console.WriteLine("Exiting Application");
                        mainChoice="no";
                        break;
                    }
                }
            }while(mainChoice=="yes");
        }

        public static void UserRegistration()
        {
            Console.Write("Enter your name : ");
            string name=Console.ReadLine();
            Console.Write("Enter your gender : ");
            Gender gender=Enum.Parse<Gender>(Console.ReadLine());
            Console.Write("Enter your department : ");
            Department department=Enum.Parse<Department>(Console.ReadLine());
            Console.Write("Enter your Mobile Number : ");
            string mobileNumber=Console.ReadLine();
            Console.Write("Enter your mail ID : ");
            string mailID=Console.ReadLine();

            UserDetails user=new UserDetails(name,gender,department,mobileNumber,mailID);
            userList.Add(user);
            Console.WriteLine("Registration Successfully completed.Your user ID is "+user.UserID);
        }

        public static void UserLogin()
        {
            
            
                Console.Write("Enter user login ID : ");
                string loginID=Console.ReadLine().ToUpper();
                bool flag=true;
                foreach(UserDetails user in userList)
                {
                    if (loginID.Equals(user.UserID))
                    {
                        flag=false;
                        Console.WriteLine("You are LoggedIn.");
                        currentLoggedInUser=user;
                        subMenu();
                        break;
                    }
                }
                if(flag)
                {
                    Console.WriteLine("Invalid User ID. Please enter a valid one");
                }

                
           
        }

        public static void subMenu()
        {
            string subChoice = "yes";
            do
            {
                Console.WriteLine("Sub Menu\n1. Borrowbook\n2. Show Borrow History\n3. Return Books\n4. WalletRecharge\n5. Exit");
                Console.Write("Enter your option : ");
                int option = int.Parse(Console.ReadLine());
                switch (option)
                {
                    case 1:
                        {
                            Console.WriteLine("Borrow Book");
                            BorrowBook();
                            break;
                        }
                    case 2:
                        {
                            Console.WriteLine("Show Borrowed History");
                            ShowBorrowedHistory();
                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine("Return Books");
                            ReturnBooks();
                            break;
                        }
                    case 4:
                        {
                            Console.WriteLine("WalletRecharge");
                            WalletRecharge();
                            break;
                        }
                    case 5:
                        {
                            Console.WriteLine("Exiting Sub Menu");
                            subChoice = "no";
                            break;
                        }
                }

            } while (subChoice == "yes");
        }

        public static void BorrowBook()
        {
            //show list of books
            foreach(BookDetails book in bookList)
            {
                Console.WriteLine($"|{book.BookID}|{book.BookName}|{book.AuthorName}|{book.BookCount}|");

            }

            //ask user to enter book ID
            Console.Write("Enter book ID to borrow : ");
            string bookID=Console.ReadLine().ToUpper();
            bool flag=true;
            foreach(BookDetails book in bookList)
            {
                if (bookID.Equals(book.BookID))
                {
                    flag=false;
                    Console.Write("Enter the count of the book : ");
                    int bookCountNeeded=int.Parse(Console.ReadLine());
                    if (bookCountNeeded <= book.BookCount)
                    {
                        int count=0;
                        foreach (BorrowDetails borrow in borrowList)
                        {
                            if (currentLoggedInUser.UserID.Equals(borrow.UserID) && borrow.Status.Equals(Status.Borrowed))
                            {
                                count++;
                            }

                        }
                        if (count==3)
                        {
                            Console.WriteLine("You have borrowed 3 books already");
                        }
                        else if (count > 3)
                        {
                            Console.WriteLine($"You can have maximum of 3 borrowed books. Your already borrowed books count is {count} and requested count is {bookCountNeeded}, which exceeds 3");
                        }
                        else
                        {
                            int paidFineAmount=0;
                            BorrowDetails borrow =new BorrowDetails(bookID,currentLoggedInUser.UserID,DateTime.Today,bookCountNeeded,Status.Borrowed,paidFineAmount);
                            Console.WriteLine("Book Borrowed successfully");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Books are not available for the selected count");
                        foreach (BorrowDetails borrow in borrowList)
                        {
                            if (bookID.Equals(borrow.BookID) && borrow.Status.Equals(Status.Borrowed))
                            {
                                
                                Console.WriteLine($"The book will be available on {borrow.BorrowedDate.AddDays(15)}");
                            }

                        }

                    }

                }
            }
            if(flag)
            {
                Console.WriteLine("Invalid Book ID, Please enter valid ID");
            }
            //            
        }

        public static void ShowBorrowedHistory()
        {
            foreach(BorrowDetails borrow in borrowList)
            {
                if (currentLoggedInUser.UserID.Equals(borrow.UserID) && borrow.Status.Equals(Status.Borrowed))
                {
                    Console.WriteLine($"|{borrow.BorrowID}|{borrow.BookID}|{borrow.UserID}|{borrow.BorrowedDate}|{borrow.BorrowBookCount}|{borrow.Status}|{borrow.PaidFineAmount}|");
                }
                
            }
        }
        public static void ReturnBooks()
        {
            foreach(BorrowDetails borrow in borrowList)
            {
                if (currentLoggedInUser.UserID.Equals(borrow.UserID) && borrow.Status.Equals(Status.Borrowed))
                {
                    Console.WriteLine($"|{borrow.BorrowID}|{borrow.BookID}|{borrow.UserID}|{borrow.BorrowedDate}|{borrow.BorrowBookCount}|{borrow.Status}|{borrow.PaidFineAmount}|");
                }
                
            } 
            Console.Write("Enter the bookID to return : ");
            string bookID=Console.ReadLine().ToUpper();
            foreach(BorrowDetails borrow in borrowList)
            {
                if (bookID.Equals(borrow.BookID) && borrow.Status.Equals(Status.Borrowed))
                {
                   DateTime date=borrow.BorrowedDate.AddDays(15);
                   DateTime returnDate=DateTime.Now;
                   int days=returnDate.CompareTo(date);
                   if (returnDate>date)
                   {
                    int fine=days*1;
                    if (currentLoggedInUser.WalletBalance>fine)
                    {
                        currentLoggedInUser.DeductBalance(fine);
                        borrow.Status=Status.Returned;
                        Console.WriteLine("Book returned successfully");
                    }
                    else
                    {
                        Console.WriteLine("Insufficient balance. Please rechange and proceed");

                    }
                     
                     
                   }
                   else
                    {
                        borrow.Status=Status.Returned;

                        Console.WriteLine("Book returned successfully");
                    }
                }
                
            }           
        }
        public static void WalletRecharge()
        {
            Console.Write("Enter amount : ");
            double amount=double.Parse(Console.ReadLine());
            currentLoggedInUser.WalletRecharge(amount);
        }

      

        public static void AddDefaultData()
        {
            UserDetails user1=new UserDetails("Ravichandran",Gender.Male,Department.EEE,"9938388333","ravi@gmail.com");
            userList.Add(user1);
            UserDetails user2=new UserDetails("Priyadharshini",Gender.Female,Department.CSC,"9944444455","priya@gmail.com");
            userList.Add(user2);

            BookDetails book1=new BookDetails("C#","Author1",3);
            bookList.Add(book1);
            BookDetails book2=new BookDetails("HTML","Author2",5);
            bookList.Add(book2);
            BookDetails book3=new BookDetails("CSS","Author1",3);
            bookList.Add(book3);
            BookDetails book4=new BookDetails("JS","Author1",3);
            bookList.Add(book4);
            BookDetails book5=new BookDetails("TS","Author2",2);
            bookList.Add(book5);

            BorrowDetails borrow1=new BorrowDetails("BID1001","SF3001",new DateTime(2023,09,10),2,Status.Borrowed,0);
            borrowList.Add(borrow1);
            BorrowDetails borrow2=new BorrowDetails("BID1003","SF3001",new DateTime(2023,09,12),1,Status.Borrowed,0);
            borrowList.Add(borrow2);
            BorrowDetails borrow3=new BorrowDetails("BID1004","SF3001",new DateTime(2023,09,14),1,Status.Returned,16);
            borrowList.Add(borrow3);
            BorrowDetails borrow4=new BorrowDetails("BID1002","SF3002",new DateTime(2023,09,11),2,Status.Borrowed,0);
            borrowList.Add(borrow4);
            BorrowDetails borrow5=new BorrowDetails("BID1005","SF3002",new DateTime(2023,09,09),1,Status.Returned,20);
            borrowList.Add(borrow5);

            //display user list
            foreach(UserDetails user in userList)
            {
                
                Console.WriteLine($"|{user.UserID ,12}|{user.UserName,15}|{user.Gender,12}|{user.Department,12}|{user.MobileNumber,12}|{user.MailID,12}|");

            }
            //display Book list
            foreach(BookDetails book in bookList)
            {
                Console.WriteLine($"|{book.BookID}|{book.BookName}|{book.AuthorName}|{book.BookCount}|");

            }

            //Display Borrow list
            foreach(BorrowDetails borrow in borrowList)
            {
                
                
                Console.WriteLine($"|{borrow.BorrowID}|{borrow.BookID}|{borrow.UserID}|{borrow.BorrowedDate.ToString("dd/MM/yyyy")}|{borrow.BorrowBookCount}|{borrow.Status}|{borrow.PaidFineAmount}|");
                
                
            }
            
        }

    }
}