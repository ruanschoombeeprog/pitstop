namespace InventoryManagementApi.Models
{
    public class Inventory
    {
        public string ProductCode { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }

        public Inventory(string productCode, string description, int quantity, double unitPrice)
        {
            this.ProductCode = productCode;
            this.Description = description;
            this.Quantity = quantity;
            this.UnitPrice = unitPrice;
        }
    }
}