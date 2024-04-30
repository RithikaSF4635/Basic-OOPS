using System;
namespace SynCart;
public class Program
{
    public static void Main(string[] args)
    {
        Operations.AddDefaultData();

        Operations.MainMenu();
    }
}