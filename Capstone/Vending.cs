using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Capstone
{
    public class Vending
    {
        //////////////////////////////////////////////////////CTOR/////////////////////////////////////////////////////////////////////////////
        public Vending(string filePath)
        {
            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        string[] itemData = line.Split("|");
                        ReStock(itemData);
                    }
                }
            }
            catch (Exception e)
            {

                HelperMethods.Output(e.Message);
            }
        }
        //////////////////////////////////////////////////////PROPERTIES/////////////////////////////////////////////////////////////////////////////
        public List<Item> Stock { get; private set; } = new List<Item>();

        public List<string> LogList { get; private set; } = new List<string>();

        public List<string> SalesReportList 
        { 
            get
            {
                List<string> SalesReportList = new List<string>();
                foreach (Item item in Stock)
                {
                    SalesReportList.Add($"{item.Name} | {item.NumberSold}");
                }
                return SalesReportList;
            } 
        } 

        public decimal CurrentMoneyIn { get; private set; } = 0;

        //////////////////////////////////////////////////////METHODS/////////////////////////////////////////////////////////////////////////////
        public void ReStock(string[] itemData)
        {
            if (itemData[itemData.Length - 1] == "Chip")
            {
                Chips chip = new Chips(itemData[1], decimal.Parse(itemData[2]), itemData[0]);
                Stock.Add(chip);
            }
            else if (itemData[itemData.Length - 1] == "Candy")
            {
                Candy candy = new Candy(itemData[1], decimal.Parse(itemData[2]), itemData[0]);
                Stock.Add(candy);
            }
            else if (itemData[itemData.Length - 1] == "Drink")
            {
                Drink drink = new Drink(itemData[1], decimal.Parse(itemData[2]), itemData[0]);
                Stock.Add(drink);
            }
            else if (itemData[itemData.Length - 1] == "Gum")
            {
                Gum gum = new Gum(itemData[1], decimal.Parse(itemData[2]), itemData[0]);
                Stock.Add(gum);
            }
            else
            {
                Item item = new Item(itemData[1], decimal.Parse(itemData[2]), itemData[0]);
                Stock.Add(item);
            }
        }

        public void PrintMenu(string menuName)
        {
            if (menuName == "Start Menu")
            {
                HelperMethods.Output("1.) Display Vending Machine Items \n2.) Purchase \n3.) Exit");
            }
            else if (menuName == "Purchase Menu")
            {
                HelperMethods.Output("1.) Feed Money \n2.) Select Product \n3.) Finish Transaction");
            }
        }

        public int StartMenu()
        {
            PrintMenu("Start Menu");
            string userInput = HelperMethods.LineInput();
            if (userInput == "1")
            {
                Display();
                return 1;
            }
            else if (userInput == "2")
            {
                int exitPurchase = 0;
                while (exitPurchase != 3)
                {
                    exitPurchase = Purchase();
                }
                return 2;
            }
            else if (userInput == "3")
            {
                HelperMethods.Output("Thank You For Shopping At Umbrella Corp.");
                return 3;
            }
            else if (userInput == "4")
            {
                SalesReport();
                return 1;
            }
            else
            {
                HelperMethods.Output("Not a Valid Entry. Press Enter to Return to Purchase Menu:");
                HelperMethods.LineInput();
                return 1;
            }
        }
        public void Display()
        {
            //Loops through our list of class Item Stock in vending.cs and calls the Display property for that Item
            HelperMethods.ClearPage();
            foreach (Item item in Stock)
            {
                HelperMethods.Output(item.Display);
            }
            HelperMethods.Output("Press Enter to Return to the Start Menu:");
            HelperMethods.LineInput();
        }
        public int Purchase()
        {
            HelperMethods.ClearPage();
            PrintMenu("Purchase Menu");
            HelperMethods.Output($"Current Money Provided is {CurrentMoneyIn:C}");
            string userInput = HelperMethods.LineInput();
            if (userInput == "1")
            {
                FeedMoney();
                return 1;
            }
            else if (userInput == "2")
            {
                SelectProduct();
                return 2;
            }
            else if (userInput == "3")
            {
                FinishTransaction();
                return 3;
            }
            else
            {
                HelperMethods.Output("Not a Valid Entry. Press Enter to Return to Purchase Menu:");
                HelperMethods.LineInput();
                return 1;
            }
        }
        public void SalesReport()
        {
            string dir = Environment.CurrentDirectory;
            string dateTime = DateTime.Now.ToString("s");
            string dateTimeFixed = dateTime.Replace(':', '.');
            string fileName = $"salesReport{dateTimeFixed}.txt";
            string filePath = Path.Combine(dir, fileName);
            HelperMethods.WriteToFile(filePath, SalesReportList);
        }

        public void FeedMoney()
        {
            //Feeds money into Current money in as long as it meets the conditions.
            HelperMethods.Output("Please Insert Your Money: Only Accepts $1, $5, $10. [Enter 1, 5, or 10]");
            string moneyInput = HelperMethods.LineInput();
            if (moneyInput == "1" || moneyInput == "5" || moneyInput == "10")
            {
                int money = int.Parse(moneyInput);
                if (CurrentMoneyIn + money <= 10)
                {
                    CurrentMoneyIn += money;
                    LogList.Add($"{DateTime.Now} FEED MONEY: {money:C} {CurrentMoneyIn:C}");
                }
                else
                {
                    HelperMethods.ClearPage();
                    HelperMethods.Output("Vending Machine can not hold more than $10. Press Enter to Return to the Purchase Menu:");
                    HelperMethods.LineInput();
                }
            }
            else
            {
                HelperMethods.Output("Please Enter a Valid Bill. Press Enter to Return to Purchase Menu:");
                HelperMethods.LineInput();
            }

        }
        public void SelectProduct()
        {
            //Checks to make sure that userInput is a valid SlotNumber, the item in that slotNumber is not sold out, and that they have suffiecient funding
            HelperMethods.ClearPage();
            foreach (Item item in Stock)
            {
                HelperMethods.Output(item.Display);
            }
            HelperMethods.Output("Choose your Product by Slot Number :");
            string outputSlot = HelperMethods.LineInput();
            List<string> SlotNumberList = new List<string>();
            foreach (Item item in Stock)
            {
                SlotNumberList.Add(item.SlotNumber);
            }
            if (SlotNumberList.Contains(outputSlot.ToUpper()))
            {
                int slotNumberIndex = SlotNumberList.IndexOf(outputSlot.ToUpper());
                if (!Stock[slotNumberIndex].IsSoldOut)
                {
                    if (Stock[slotNumberIndex].Price <= CurrentMoneyIn)
                    {
                        Stock[slotNumberIndex].SellItem();
                        CurrentMoneyIn -= Stock[slotNumberIndex].Price;
                        LogList.Add($"{DateTime.Now} {Stock[slotNumberIndex].Name} {Stock[slotNumberIndex].SlotNumber}: {Stock[slotNumberIndex].Price:C} {CurrentMoneyIn:C}");
                        HelperMethods.ClearPage();
                        HelperMethods.Output($"You Chose {Stock[slotNumberIndex].Name} the Price was {Stock[slotNumberIndex].Price:C} Enjoy!");
                        HelperMethods.Output($"You Have {CurrentMoneyIn:C} Left.");
                        HelperMethods.Output(Stock[slotNumberIndex].MakeSound());
                        HelperMethods.Output("Press Enter to Return to the Purchase Menu:");
                        HelperMethods.LineInput();
                    }
                    else
                    {
                        HelperMethods.Output("Please Insert More Money. Press Enter to Return to Purchase Menu:");
                        HelperMethods.LineInput();
                    }
                }
                else
                {
                    HelperMethods.Output("Item is Sold Out Please Choose a Different Item. Press Enter to Return to Purchase Menu:");
                    HelperMethods.LineInput();
                }
            }
            else
            {
                HelperMethods.Output("Not a valid Slot Number. Press Enter to Return to Purchase Menu:");
                HelperMethods.LineInput();
            }
        }

        public void FinishTransaction()
        {
            //Return Money and writes to the log file from LogList and then returns you to StartMenu()
            LogList.Add($"{DateTime.Now} GIVE CHANGE: {CurrentMoneyIn:C} $0.00");

            HelperMethods.Output(HelperMethods.GetChange(CurrentMoneyIn));
            CurrentMoneyIn = 0;

            string dir = Environment.CurrentDirectory;
            string filePath = Path.Combine(dir, "Log.txt");
            HelperMethods.WriteToFile(filePath, LogList);
            HelperMethods.Output("Press Enter to Return to the Start Menu:");
            HelperMethods.LineInput();
        }


    }
}
