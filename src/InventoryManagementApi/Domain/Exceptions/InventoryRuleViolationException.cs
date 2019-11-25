using System;

namespace Pitstop.InventoryManagementApi.Domain.Exceptions
{
    public class InventoryRuleViolationException : Exception
    {
        public InventoryRuleViolationException()
        {
        }

        public InventoryRuleViolationException(string message) : base(message)
        {
        }

        public InventoryRuleViolationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
