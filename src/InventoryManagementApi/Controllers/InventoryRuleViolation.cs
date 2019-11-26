namespace InventoryManagementApi.Controllers
{
    public class InventoryRuleViolation
    {
        public string ErrorMessage { get; set; }

        public InventoryRuleViolation(string message)
        {
            this.ErrorMessage = message;
        }
    }
}