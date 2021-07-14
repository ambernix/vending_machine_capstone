using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    public class Item
    {
        
        public Item (string name, decimal price, string slotNumber)
        {
            Name = name;
            Price = price;
            SlotNumber = slotNumber;
        }
        public string Name { get; }

        public decimal Price { get; }

        public string SlotNumber { get; }

        public int ItemCount { get; private set; } = 5;

        public int NumberSold { get; private set; } = 0;

        public string Display 
        { 
            get
            {
                if (IsSoldOut)
                {
                    return ($"{SlotNumber}.) !!!SOLD OUT!!!");
                }
                else 
                {
                    return ($"{SlotNumber}.) {Name}: {Price:C} -- {ItemCount} remaining");
                }
            } 
        }

        public bool IsSoldOut 
        { 
            get
            {
                if (ItemCount == 0)
                {
                    return true;
                }
                return false;
            } 
        }

        public void SellItem()
        {
            if (!IsSoldOut)
            {
                ItemCount--;
                NumberSold++;
            }
        }

        public virtual string MakeSound()
        {
            return "NomNom, Yum!";
        }

    }
}
