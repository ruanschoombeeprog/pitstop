using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pitstop.Models
{
    public class Inventory
    {
        public string ProductCode { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }

        public Inventory()
        {

        }

        public Inventory(string productCode, string description, int quantity, double unitPrice)
        {
            ProductCode = productCode;
            Description = description;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }
    }
}
