using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAdmission
{
    //Enum Declaration
    public enum Gender{
        Select,
        Female,
        Male
    }
    public class StudentDetails
    {
        /*
        a.	StudentID – (AutoGeneration ID – SF3000)
        b.	StudentName
        c.	FatherName
        d.	DOB
        e.	Gender – Enum (Male, Female, Transgender)
        f.	Physics
        g.	Chemistry
        h.	Maths

        */
        //Field
        //Static field
        private static int s_studentID=3000;
        //Properties
        public string StudentID { get;  } //Read Only Property
        public string StudentName { get; set; }
        public string FatherName { get; set; }
        public DateTime DOB { get; set; }
        public Gender Gender { get; set; }
        public int PhysicsMark { get; set; }
        public int ChemistryMark { get; set; }
        public int MathsMark { get; set; }

        //Constructor
        public StudentDetails(string studentName,string fatherName,DateTime dob,Gender gender,int physics,int chemistry,int maths)
        {
            //Auto Incrementation
            s_studentID++;
            StudentID="SF"+s_studentID;
            StudentName=studentName;
            FatherName=fatherName;
            DOB=dob;
            Gender=gender;
            PhysicsMark=physics;
            ChemistryMark=chemistry;
            MathsMark=maths;
        }

        public StudentDetails(string student)
        {
            string[] values=student.Split(",");
            StudentID=values[0];
            s_studentID=int.Parse(values[0].Remove(0,2));
            StudentName=values[1];
            FatherName=values[2];
            DOB=DateTime.ParseExact(values[3],"dd/MM/yyyy",null);
            Gender=Enum.Parse<Gender>(values[4]);
            PhysicsMark=int.Parse(values[5]);
            ChemistryMark=int.Parse(values[6]);
            MathsMark=int.Parse(values[7]);
        }

        //Methods
        public double Average()
        {
            int total=PhysicsMark+ChemistryMark+MathsMark;
            double Average=(double)total/3;
            return Average;
        }

        public bool CheckEligibility(double cutOff)
        {
            if(Average() >= cutOff)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        
        
        
        
        
        
        
        
        
        
        
        
        
    }
}