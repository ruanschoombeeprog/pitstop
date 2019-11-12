using Microsoft.AspNetCore.Mvc;
using Pitstop.WorkshopManagementAPI.Domain;
using System;

namespace WorkshopManagement.AcceptanceTests.Context
{
    public class WorkshopPlanningContext
    {
        public DateTime PlanningDate { get; set; }
        public IActionResult ActionResult { get; set; }
    }
}
