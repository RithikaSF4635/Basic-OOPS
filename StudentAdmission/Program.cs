using System;
namespace StudentAdmission; //File Scoped Namespace
public class Program
{
    public static void Main(string[] args)
    {
        //Creating CSV files 
        FileHandling.Create();
        //Default Data Calling
        FileHandling.ReadFromCSV();
        Operations.MainMenu();

        FileHandling.WriteToCSV();
    }
}
