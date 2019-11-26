using InventoryManagementApi.Commands;
using InventoryManagementApi.Models;
using Pitstop.InventoryManagementApi.Domain.Exceptions;

namespace Pitstop.InventoryManagementApi.Domain.Rules
{
    public static class InventoryRules
    {
        public static void InventoryQuantityShouldBeZeroOrLarger(this RegisterInventory registerInventory)
        {
            if (registerInventory.Quantity < 0)
                throw new InventoryRuleViolationException("Quantity should be larger than 0.");
        }

        public static void InventoryPriceShouldBeZerorOrLarger(this RegisterInventory registerInventory)
        {
            if (registerInventory.UnitPrice < 0)
                throw new InventoryRuleViolationException("UnitPrice should be larger than 0.");
        }

        public static void InventoryProductCodeLengthShouldBe12CharactersLong(this RegisterInventory registerInventory)
        {
            if (registerInventory.ProductCode.Length != 12)
                throw new InventoryRuleViolationException("Inventory ProductCode should be 12 characters long.");
        }

        public static void InventoryQuantityShouldBeZeroOrLarger(this UpdateInventory updateInventory)
        {
            if (updateInventory.Quantity < 0)
                throw new InventoryRuleViolationException("Quantity should be larger than 0.");
        }

        public static void InventoryPriceShouldBeZerorOrLarger(this UpdateInventory updateInventory)
        {
            if (updateInventory.UnitPrice < 0)
                throw new InventoryRuleViolationException("UnitPrice should be larger than 0.");
        }

        public static void InventoryProductCodeLengthShouldBe12CharactersLong(this UpdateInventory updateInventory)
        {
            if (updateInventory.ProductCode.Length != 12)
                throw new InventoryRuleViolationException("Inventory ProductCode should be 12 characters long.");
        }

        public static void UseInventoryQuantityMustBeSmallerOrEqualToInventoryLevel(this UseInventoryItem useInventoryItem, Inventory inventory)
        {
            if(inventory == null)
                throw new InventoryRuleViolationException("Product does not exist");

            if (useInventoryItem.Quantity > inventory.Quantity)
                throw new InventoryRuleViolationException("Not able to use item, not enough in stock");
        }
    }
}
