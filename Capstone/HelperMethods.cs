using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Capstone
{
    public static class HelperMethods
    {
        //////////////////////////////////////////////////////METHODS/////////////////////////////////////////////////////////////////////////////
        public static string LineInput()
        {
            return Console.ReadLine();
        }

        public static void Output(string output)
        {
            Console.WriteLine(output);
        }

        public static void ClearPage()
        {
            Console.Clear();
        }

        public static void WriteToFile(string filePath, List<string> data)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    foreach (string line in data)
                    {
                        sw.WriteLine(line);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static string GetChange(decimal money)
        {
            int nickels = 0;
            int dimes = 0;
            int quarters = 0;

            while (money >= 0.25M)
            {
                quarters++;
                money -= 0.25M;
            }
            while (money >= 0.10M)
            {
                dimes++;
                money -= 0.10M;
            }
            while (money >= 0.05M)
            {
                nickels++;
                money -= 0.05M;
            }
            return $"Your change is {quarters} quarters, {dimes} dimes, and {nickels} nickels";
        }
    }
}
