using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    public class Chips : Item
    {
        //////////////////////////////////////////////////////CTOR/////////////////////////////////////////////////////////////////////////////
        public Chips(string name, decimal price, string slotNumber) : base(name, price, slotNumber)
        {

        }
        //////////////////////////////////////////////////////METHODS/////////////////////////////////////////////////////////////////////////////
        public override string MakeSound()
        {
            return ("Crunch Crunch, Yum!");
        }
    }
}
