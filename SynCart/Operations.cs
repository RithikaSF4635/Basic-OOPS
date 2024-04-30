using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SynCart
{
    public static class Operations
    {
        static List<CustomerDetails> customerList=new List<CustomerDetails>();
        static List<ProductDetails> productList=new List<ProductDetails>();
        static List<OrderDetails> orderList=new List<OrderDetails>();

        static CustomerDetails currentLoggedInCustomer;

        //Main Menu
        public static void MainMenu()
        {
            string mainChoice="yes";
            do
            {
                Console.WriteLine("************Welcome to SynCart************");
                Console.WriteLine("Main Menu\n1. Customer Registration\n2. Login\n3. Exit");
                int option=int.Parse(Console.ReadLine());
                switch(option)
                {
                    case 1:
                    {
                        Console.WriteLine("************Customer Registration************");
                        CustomerRegistration();
                        break;
                    }
                    case 2:
                    {
                        Console.WriteLine("*****************Customer Login Page**************");
                        Login();
                        break;
                    }
                    case 3:
                    {
                        Console.WriteLine("Exiting application");
                        mainChoice="no";
                        break;
                    }
                }

            }while(mainChoice=="yes");
        }

        public static void CustomerRegistration()
        {
            Console.Write("Enter your name : ");
            string name=Console.ReadLine();
            Console.Write("Enter your City : ");
            string city=Console.ReadLine();
            Console.Write("Enter your Phone Number : ");
            long phone=long.Parse(Console.ReadLine());
            Console.Write("Enter your Mail ID : ");
            string emailID=Console.ReadLine();
            
            double walletBalance=0.0;
            Console.Write("WalletBalance : "+walletBalance);
            CustomerDetails customer=new CustomerDetails(name,city,phone,walletBalance,emailID);
            customerList.Add(customer);
            Console.WriteLine($"Registration Successfully Completed and Customer ID is {customer.CustomerID}");
        }

        public static void Login()
        {
            Console.Write("Enter your customer ID : ");
            string loginID=Console.ReadLine().ToUpper();
            bool flag=true;
            foreach(CustomerDetails customer in customerList)
            {
                if (loginID.Equals(customer.CustomerID))
                {
                    flag=false;
                    Console.WriteLine("Your logged in Successfully");
                    currentLoggedInCustomer=customer;
                    SubMenu();
                    break;
                }
            }
            if(flag)
            {
                Console.WriteLine("Invalid Customer ID.");
            }
        }

        //Sub Menu
        public static void SubMenu()
        {
            
            string subChoise="yes";
            do
            {
                Console.WriteLine("**********Sub Menu***********");
                Console.WriteLine("Sub Menu\n1. Purchase\n2. Order History\n3. Cancel Order\n4. Wallet Balance\n5. Wallet Recharge\n6. Exit");
                Console.WriteLine("Enter your option : ");
                int option=int.Parse(Console.ReadLine());
                switch(option)
                {
                    case 1:
                    {
                        Purchase();
                        break;
                    }
                    case 2:
                    {
                        OrderHistory();
                        break;
                    }
                    case 3:
                    {
                        CancelOrder();
                        break;
                    }
                    case 4:
                    {
                        WalletBalance();
                        break;
                    }
                    case 5:
                    {
                        
                        Recharge();
                        break;
                    }
                    case 6:
                    {
                        subChoise="no";
                        break;
                    }
                }


            }while(subChoise=="yes");
        }

        //method Purchase
        public static void Purchase()
        {
            //show the Product list
            foreach(ProductDetails product in productList)
            {
                Console.WriteLine($"|{product.ProductID}|{product.ProductName}|{product.Stock}|{product.Price}|{product.ShippingDuration}|");
            }
            //ask the customer to select product ID
            Console.Write("Enter the Product ID : ");
            string productId=Console.ReadLine().ToUpper();
            //check the product ID valid/not
            bool flag=true;
            foreach(ProductDetails product in productList)
            {
                if(productId.Equals(product.ProductID))
                {
                    flag=false;
                    //if valid ask count to purchase
                    Console.Write("Enter number of products needed : ");
                    int countProductNeeded=int.Parse(Console.ReadLine());
                    if (countProductNeeded<(product.Stock))
                    {
                        //count available calculate amount using given formula
                        int deliveryCharge=50;
                        double totalAmount=(double)(countProductNeeded*product.Price) + deliveryCharge;
                        //check customer balance ensure he is having enough money to pay amount
                        if (currentLoggedInCustomer.WalletBalance > totalAmount)
                        {
                            //sufficient 1)deduct the amount from wallet 2)deduct the count from the stock
                            currentLoggedInCustomer.DeductBalance(totalAmount);
                            product.Stock-=countProductNeeded;
                            //create order id and add it to order list
                            OrderDetails order=new OrderDetails(currentLoggedInCustomer.CustomerID,product.ProductID,totalAmount,DateTime.Now,countProductNeeded,OrderStatus.Ordered);
                            orderList.Add(order);
                            Console.WriteLine($"Order Placed Successfully.Order ID: "+ order.OrderID);

                            //show the delivery date of order
                            DateTime deliveryDate=order.PurchaseDate.AddDays(product.ShippingDuration);
                            Console.WriteLine($"Order Placed and your order delivery date is on : {deliveryDate.ToString("dd/MM/yyyy")}");
                        }
                        else
                        {
                            //insufficient tell to recharge
                            Console.WriteLine("Insufficient Wallet Balance. Please recharge your wallet and do purchase again");
                        }
                        
                        
                    }
                    else{
                        Console.WriteLine($"Required count not available.Current availability is {product.Stock}");
                    }
                    //if count is not available(required  count not available) and display currently available stock
                    break;

                }
                
            }
            if(flag)
            {
                Console.WriteLine("Invalid Product ID");
            }
            

        }//Purchase Ends.

        //method Order History
        public static void OrderHistory()
        {
            bool flag=true;
            //Display orders that current Logged customer made
            foreach(OrderDetails order in orderList)
            {
                if(currentLoggedInCustomer.CustomerID.Equals(order.CustomerID) && order.OrderStatus.Equals(OrderStatus.Ordered))
                {
                    flag=false;
                    Console.WriteLine($"|{order.OrderID}|{order.CustomerID}|{order.ProductID}|{order.TotalPrice}|{order.PurchaseDate}|{order.Quantity}|{order.OrderStatus}|");
                }
                
            }
            if(flag)
            {
                Console.WriteLine("You have not made any order.");
            }

        }//Order Ends.
        
        //method Cancel Order
        public static void CancelOrder()
        {
            bool flag=true;
            foreach(OrderDetails order in orderList)
            {
                if (currentLoggedInCustomer.CustomerID.Equals(order.CustomerID) && order.OrderStatus.Equals(OrderStatus.Ordered))
                {
                    flag=false;
                    Console.WriteLine($"|{order.OrderID}|{order.CustomerID}|{order.ProductID}|{order.TotalPrice}|{order.PurchaseDate}|{order.Quantity}|{order.OrderStatus}|");
                   
                    
                }

                
            }
            
            if (flag)
            {
                Console.WriteLine("Invalid Order ID");
            }
            if (!flag)
            {
                Console.Write("Enter the orderId to cancel : ");
                string orderId = Console.ReadLine().ToUpper();
                foreach (OrderDetails order in orderList)
                {
                    if (currentLoggedInCustomer.CustomerID.Equals(order.CustomerID) && order.OrderStatus.Equals(OrderStatus.Ordered))
                    {
                        flag = false;

                        foreach (ProductDetails product in productList)
                        {
                            if (order.ProductID.Equals(product.ProductID))
                            {
                                product.Stock += order.Quantity;
                                break;
                            }
                        }
                        currentLoggedInCustomer.WalletRecharge(order.TotalPrice);
                        order.OrderStatus = OrderStatus.Cancelled;
                        Console.WriteLine($"Your order has been cancelled successfully.");

                    }


                }
                if (flag)
                {
                    Console.WriteLine("Invalid OrderID");
                }
            }
        }//Cancel Order Ends.

        //method Wallet Balance
        public static void WalletBalance()
        {
            Console.WriteLine($"Wallet Balance : {currentLoggedInCustomer.WalletBalance}");
        }//wallet Balance Ends.

        //method Wallet Recharge
        public static void Recharge()
        {

            Console.Write("Enter amount : ");
            double amount=double.Parse(Console.ReadLine());
            currentLoggedInCustomer.WalletRecharge(amount);
            Console.WriteLine("Recharged successfully.Current Balance : "+currentLoggedInCustomer.WalletBalance);

        }//wallet Recharge Ends.
        public static void AddDefaultData()
        {
            CustomerDetails customer1=new CustomerDetails("Ravi","Chennai",9885858588,50000,"ravi@mail.com");
            CustomerDetails customer2=new CustomerDetails("Baskaran","Chennai",9888475757,60000,"baskaran@mail.com");
            customerList.AddRange(new List<CustomerDetails>(){customer1,customer2});

            ProductDetails product1=new ProductDetails("Mobile (Samsung)",10,10000,3);
            ProductDetails product2=new ProductDetails("Tablet (Lenovo)",5,15000,2);
            ProductDetails product3=new ProductDetails("Camara (Sony)",3,20000,4);
            ProductDetails product4=new ProductDetails("iPhone",5,50000,6);
            ProductDetails product5=new ProductDetails("Laptop (Lenovo I3)",3,40000,3);
            ProductDetails product6=new ProductDetails("HeadPhone (Boat)",5,1000,2);
            ProductDetails product7=new ProductDetails("Speakers (Boat)",4,500,2);
            productList.AddRange(new List<ProductDetails>(){product1,product2,product3,product4,product5,product6,product7});

            OrderDetails order1=new OrderDetails("CID1001","PID101",20000,DateTime.Now,2,OrderStatus.Ordered);
            OrderDetails order2=new OrderDetails("CID1002","PID103",40000,DateTime.Now,2,OrderStatus.Ordered);
            orderList.AddRange(new List<OrderDetails>(){order1,order2});

            //To Display CustomerList
            foreach(CustomerDetails customer in customerList)
            {
                Console.WriteLine($"|{customer.CustomerID}|{customer.CustomerName}|{customer.City}|{customer.MobileNumber}|{customer.WalletBalance}|{customer.EmailID}|");
            }

            //Display Product List
            

            //Display Order List
            








        }
    }
}