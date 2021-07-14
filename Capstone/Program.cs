using System;
using System.Collections.Generic;
using System.IO;

namespace Capstone
{
    class Program
    {
        
        static void Main(string[] args)
        {
            string dir = Environment.CurrentDirectory;
            string filePath = Path.Combine(dir, "vending.txt");
            Vending vendingMachine = new Vending(filePath);

            int exitVariable = 0;
            while (exitVariable != 3)
            {
                Console.Clear();
                exitVariable = vendingMachine.StartMenu();
            } 
        }
    }
}
