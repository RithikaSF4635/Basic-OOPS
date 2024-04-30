using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAdmission
{
    public static class FileHandling
    {
        //Creating folder and files method
        public static void Create()
        {
            if (!Directory.Exists("StudentAdmission"))
            {
                Console.WriteLine("Creating Folder");
                Directory.CreateDirectory("StudentAdmission");
            }

            //File for student info
            if(!File.Exists("StudentAdmission/StudentInfo.csv"))
            {
                Console.WriteLine("Creating file...");
                File.Create("StudentAdmission/StudentInfo.csv").Close();
            }

            //File for department

            if(!File.Exists("StudentAdmission/DepartmentInfo.csv"))
            {
                Console.WriteLine("Creating file...");
                File.Create("StudentAdmission/DepartmentInfo.csv").Close();
            }

            //File for Admission

            if (!File.Exists("StudentAdmission/AdmissionInfo.csv"))
            {
                Console.WriteLine("Creating file....");
                File.Create("StudentAdmission/AdmissionInfo.csv").Close();
            }

        }

        
        //WriteToCSV method
        public static void WriteToCSV()
        {
            //Students Info
            string[] students=new string[Operations.studentList.Count];
            for(int i=0;i<Operations.studentList.Count;i++)
            {
                students[i]=Operations.studentList[i].StudentID+","+Operations.studentList[i].StudentName+","+Operations.studentList[i].FatherName+","+Operations.studentList[i].DOB.ToString("dd/MM/yyyy")+","+Operations.studentList[i].Gender+","+Operations.studentList[i].PhysicsMark+","+Operations.studentList[i].ChemistryMark+","+Operations.studentList[i].MathsMark;

            }
            File.WriteAllLines("StudentAdmission/StudentInfo.csv",students);

            //Department Info

            string[] departments=new string[Operations.departmentList.Count];
            for (int i=0;i<Operations.departmentList.Count;i++)
            {
                departments[i]=Operations.departmentList[i].DepartmentID+","+Operations.departmentList[i].DepartmentName+","+Operations.departmentList[i].NumberOfSeats;

            }
            File.WriteAllLines("StudentAdmission/DepartmentInfo.csv",departments);

            //Admission Info

            string[] admissions=new string[Operations.admissionList.Count];
            for (int i=0;i<Operations.admissionList.Count;i++)
            {
                admissions[i]=Operations.admissionList[i].AdmissionID+","+Operations.admissionList[i].StudentID+","+Operations.admissionList[i].DepartmentID+","+Operations.admissionList[i].AdmissionDate+","+Operations.admissionList[i].AdmissionStatus;

            }
            File.WriteAllLines("StudentAdmission/AdmissionInfo.csv",admissions);
        }


        //ReadFromCSV method
        public static void ReadFromCSV()
        {
            string[] students=File.ReadAllLines("StudentAdmission/StudentInfo.csv");
            foreach (string student in students)
            {
                StudentDetails student1=new StudentDetails(student);
                Operations.studentList.Add(student1);
            }

            //Department file reading
            string[] departments=File.ReadAllLines("StudentAdmission/DepartmentInfo.csv");
            foreach (string department in departments)
            {
                DepartmentDetails department1=new DepartmentDetails(department);
                Operations.departmentList.Add(department1);
            }

            //Admission file reading
            string[] admissions=File.ReadAllLines("StudentAdmission/AdmissionInfo.csv");
            foreach(string admission in admissions)
            {
                AdmissionDetails admission1=new AdmissionDetails(admission);
                Operations.admissionList.Add(admission1);
            }
        }
    }
}