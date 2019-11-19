using Pitstop.WorkshopManagementAPI.Commands;
using Pitstop.WorkshopManagementAPI.Domain;
using Pitstop.WorkshopManagementAPI.Domain.Entities;
using System;
using WorkshopManagement.UnitTests.TestdataBuilders;

namespace Pitstop.WorkshopManagement.Specs
{
    public class WorkshopPlanningContext
    {
        public string VehicleId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public MaintenanceJobBuilder MaintenanceJobBuilder { get; set; } = new MaintenanceJobBuilder();
        public VehicleBuilder VehicleBuilder { get; set; } = new VehicleBuilder();
        public CustomerBuilder CustomerBuilder { get; set; } = new CustomerBuilder();
        public WorkshopPlanning WorkshopPlanning { get; set; }

        public MaintenanceJob SelectedJob { get; set; }
    }
}
