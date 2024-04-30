using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAdmission
{
    //Static Class
    public static class Operations
    {
        //Local/Global Object Creation
        static StudentDetails currentLoggedInStudent;
        
        //Static List Creation
        public static List<StudentDetails> studentList=new List<StudentDetails>();
        public static List<DepartmentDetails> departmentList=new List<DepartmentDetails>();
        public static List<AdmissionDetails> admissionList=new List<AdmissionDetails>();

        public static void MainMenu()
        {
            Console.WriteLine("**********************Welcome to Syncfusion College of Engineering and Technology*********************");
            //Need to show the main menu option
           

            string mainChoice="yes";
            do
            {
                Console.WriteLine("MainMenu\n1. Registration\n2. Login\n3. Departmentwise Seat Availability\n4. Exit");
                //Need to get an input from user and validate.
                Console.Write("SelectionTypes an option: ");
                int mainOption = int.Parse(Console.ReadLine());
                //Need to create mainmenu structure 
                switch (mainOption)
                {
                    case 1:
                        {
                            Console.WriteLine("***************Student Registration*******************");
                            StudentRgistration();
                            break;
                        }
                    case 2:
                        {
                            Console.WriteLine("***************Student Login**********************");
                            StudentLogin();
                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine("*****************Departmentwise Seat Availability*****************");
                            DepartmentwiseSeatAvailability();
                            break;
                        }
                    case 4:
                        {
                            
                            Console.WriteLine("Application Exited Successfully");
                            mainChoice="no";
                            break;
                        }

                }
                //Need to iterate until the option is exit.
            } while (mainChoice=="yes");

        }//Main menu ends

        //Student Registration
        public static void StudentRgistration()
        {
            //Need to get required details
            Console.Write("Enter your name : ");
            string name=Console.ReadLine();
            Console.Write("Enter your father's name : ");
            string fatherName=Console.ReadLine();
            Console.Write("Enter your Date of Birth : ");
            DateTime dob=DateTime.ParseExact(Console.ReadLine(),"dd/MM/yyyy",null);
            Console.Write("Enter your Gender : ");
            Gender gender=Enum.Parse<Gender>(Console.ReadLine(),true);
            Console.Write("Enter your physics mark : ");
            int physics=int.Parse(Console.ReadLine());
            Console.Write("Enter your chemistry mark : ");
            int chemistry=int.Parse(Console.ReadLine());
            Console.Write("Enter your maths mark : ");
            int maths=int.Parse(Console.ReadLine());
            //Need to create an object
            StudentDetails student=new StudentDetails(name,fatherName,dob,gender,physics,chemistry,maths);
            //Need to add in the list
            studentList.Add(student);
            //Need to display confirmation message and ID.
            Console.WriteLine($"Registration Successfull and Student ID is {student.StudentID}");

        }//Student Registration Ends

        //Student Login
        public static void StudentLogin()
        {
            //Need to get ID input
            Console.WriteLine("Enter your student ID : ");
            string loginID =Console.ReadLine().ToUpper();
            //Validate by its presence in the list
            bool flag=true;
            foreach(StudentDetails student in studentList)
            {
                if(loginID.Equals(student.StudentID))
                {
                    flag=false;
                    //assigning current user to gobal variable
                    currentLoggedInStudent=student;
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
        }//Student Login Ends

        //Sub Menu
        public static void SubMenu()
        {
            string subChoice="yes";
            do
            {
                Console.WriteLine("************SubMenu************");
                //Need to show sub menu option
                Console.WriteLine("Select an Option\n1. Check Eligibility\n2. Show Details\n3. Take Admission\n4. Cancel Admission\n5. Show Admission Details\n6. Exit");
                //Getting user option
                Console.Write("Enter your Option : ");
                int subOption=int.Parse(Console.ReadLine());
                //Need to create sub menu structure
                switch(subOption)
                {
                    case 1:
                    {
                        Console.WriteLine("************Check Eligibility************");
                        CheckEligibility();
                        break;
                    }
                    case 2:
                    {
                        Console.WriteLine("**************Show Details*************");
                        ShowDetails();
                        break;
                    }
                    case 3:
                    {
                        Console.WriteLine("*****************Take Admission**************");
                        TakeAdmission();
                        break;
                    }
                    case 4:
                    {
                        Console.WriteLine("**************Cancel Admission************");
                        CancelAdmission();
                        break;
                    }
                    case 5:
                    {
                        Console.WriteLine("**********************Show Admission Details****************");
                        ShowAdmissionDetails();
                        break;
                    }
                    case 6:
                    {
                        Console.WriteLine("**************Exit****************");
                        subChoice="no";
                        break;
                    }
                }
                //Iterate till the option is exit.
            }while(subChoice=="yes");
        }//Sub menu ends.
        
        //CheckEligibility
        public static void CheckEligibility()
        {
            //Get the cut off value as input
            Console.Write("Enter the cutoff value : ");
            double cutOff=double.Parse(Console.ReadLine());
            //Check eligibility or not
            if(currentLoggedInStudent.CheckEligibility(cutOff))
            {
                Console.WriteLine("Student is eligible");
            }
            else
            {
                Console.WriteLine("Student is not eligible");
            }
        }//CheckEligibility Ends.

        //Show Details
        public static void ShowDetails()
        {
            //Need to show current student's details.
            Console.WriteLine("|Student ID|Student Name|Father Name|DOB|Gender|Physics Marks|Chemistry Marks|Maths Marks|");
            Console.WriteLine($"|{currentLoggedInStudent.StudentID}|{currentLoggedInStudent.StudentName}|{currentLoggedInStudent.FatherName}|{currentLoggedInStudent.DOB}|{currentLoggedInStudent.Gender}|{currentLoggedInStudent.PhysicsMark}|{currentLoggedInStudent.ChemistryMark}|{currentLoggedInStudent.MathsMark}");
        }//ShowDetails Ends.

        //Take Admission
        public static void TakeAdmission()
        {
            //Need to show available department details
            DepartmentwiseSeatAvailability();
            //Ask department ID from user
            Console.Write("Select a department ID : ");
            string departmentID=Console.ReadLine().ToUpper();
            //check the ID is present or not
            bool flag=true;
            foreach(DepartmentDetails department in departmentList)
            {
                if(departmentID.Equals(department.DepartmentID))
                {
                    flag=false;
                    //check the student is eligible or not
                    if(currentLoggedInStudent.CheckEligibility(75.0))
                    {
                        //check the seat availability
                        if(department.NumberOfSeats>0)
                        {
                            //check student already taken any admission
                            int count=0;
                            foreach(AdmissionDetails admission in admissionList)
                            {
                                if ((currentLoggedInStudent.StudentID.Equals(admission.StudentID)) && (admission.AdmissionStatus.Equals(AdmissionStatus.Admitted)))
                                {
                                    count++;

                                }
                            }
                            if (count==0)
                            {
                                //create aadmission object
                                AdmissionDetails admissionTaken=new AdmissionDetails(currentLoggedInStudent.StudentID,department.DepartmentID,DateTime.Now,AdmissionStatus.Admitted);

                                //Reduce seat count
                                department.NumberOfSeats--;
                                //Add to the admission list
                                admissionList.Add(admissionTaken);
                                //Display admission successful message.
                                Console.WriteLine($"You have taken admission successfully.Admission ID : {admissionTaken.AdmissionID}");
                            }
                            else
                            {
                                Console.WriteLine("You have already taken an admission");
                            }
                            
                        }
                        else
                        {
                            Console.WriteLine("Seats are not available");
                        }
                        
                    }
                    else
                    {
                        Console.WriteLine("you are not eligible due to low cutoff");
                    }
                    
                    break;

                }
            }
            if(flag)
            {
                Console.WriteLine("Invalid ID or ID not present");
            }
            
        }//TakeAdmission Ends.

        //Cancel Admission
        public static void CancelAdmission()
        {
            //Check the student is taken any admission and display it
            bool flag=true;
            foreach(AdmissionDetails admission in admissionList)
            {
                if ((currentLoggedInStudent.StudentID.Equals(admission.StudentID)) && (admission.AdmissionStatus.Equals(AdmissionStatus.Admitted)))
                {
                    //cancel the found admission
                    admission.AdmissionStatus=AdmissionStatus.Cancelled;
                    //return the seat to department
                    foreach(DepartmentDetails department in departmentList)
                    {
                        if (admission.DepartmentID.Equals(department.DepartmentID))
                        {
                            department.NumberOfSeats++;
                            break;
                        }
                    }
                    break;
                }
            }
            if (flag)
            {
                Console.WriteLine("You have not admitted to cancel admission");
            }
            
        }//CancelAdmission Ends

        //ShowAdmissionDetails
        public static void ShowAdmissionDetails()
        {
            //Need to show current logged in student's admission details
            Console.WriteLine($"|Admission ID|Student ID|Department|Admission Date|");
            foreach(AdmissionDetails admission in admissionList)
            {
                if(currentLoggedInStudent.StudentID.Equals(admission.StudentID))
                {
                    Console.WriteLine($"|{admission.AdmissionID}|{admission.StudentID}|{admission.DepartmentID}|{admission.AdmissionDate}|{admission.AdmissionStatus}");
                }
                
            }
        }//ShowAdmissionDetails

        //Departmentwise Seat Availability
        public static void DepartmentwiseSeatAvailability()
        {
            //Need to show department availability
            string line = "_________________________________________";
            Console.WriteLine("DepartmentID|DepartmentName|NumberOfSeats|");
            foreach(DepartmentDetails department in departmentList)
            {

                Console.WriteLine($"|{department.DepartmentID,-12}|{department.DepartmentName,-14}|{department.NumberOfSeats,-12}|");
                Console.WriteLine(line);
            }
        }//Departmentwise Seat Availability

        //Adding Default Data
        public static void AddDefaultData()
        {
            StudentDetails student1=new StudentDetails("Ravichandran E","Ettapparajan",new DateTime(1999,11,11),Gender.Male,95,95,95);
            StudentDetails student2=new StudentDetails("Baskaran S","Sethurajan",new DateTime(1999,11,11),Gender.Male,95,95,95);
            studentList.AddRange(new List<StudentDetails>(){student1,student2});

            DepartmentDetails department1=new DepartmentDetails("EEE",29);
            DepartmentDetails department2=new DepartmentDetails("CSE",29);
            DepartmentDetails department3=new DepartmentDetails("MECH",30);
            DepartmentDetails department4=new DepartmentDetails("ECE",30);
            departmentList.AddRange(new List<DepartmentDetails>(){department1,department2,department3,department4});

            AdmissionDetails admission1=new AdmissionDetails(student1.StudentID,department1.DepartmentID,new DateTime(2022,05,11),AdmissionStatus.Admitted);
            AdmissionDetails admission2=new AdmissionDetails("SF3002","DID102",new DateTime(2022,05,12),AdmissionStatus.Admitted);
            admissionList.AddRange(new List<AdmissionDetails>(){admission1,admission2});

           
            
            
            Console.WriteLine();
            

        }//Default Data end  


        
    }
}