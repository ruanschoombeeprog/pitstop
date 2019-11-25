namespace InventoryManagementApi.Models
{
    public class Inventory
    {
        public string ProductCode { get; set; }
        public string Description { get; set; }
        public string Quantity { get; set; }
        public string UnitPrice { get; set; }

        public Inventory(string productCode, string description, string quantity, string unitPrice)
        {
            this.ProductCode = productCode;
            this.Description = description;
            this.Quantity = quantity;
            this.UnitPrice = unitPrice;
        }
    }
}