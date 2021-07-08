using System;
using System.Collections.Generic;
using System.Text;
namespace ConsoleApp5
{
    public class Car
    {
        public string Vendor { get; set; }

        public string Model { get; set; }

        public int Year { get; set; }

        public bool IsNew { get; set; }

        public override string ToString()
        {
            return $"${Vendor} \t{Model} \t{Year} \t{IsNew}";
        }
    }
}