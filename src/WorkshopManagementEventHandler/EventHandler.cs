using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Linq;
using Pitstop.Infrastructure.Messaging;
using Pitstop.WorkshopManagementEventHandler.DataAccess;
using Pitstop.WorkshopManagementEventHandler.Events;
using Pitstop.WorkshopManagementEventHandler.Model;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Pitstop.WorkshopManagementEventHandler
{
    public class EventHandler : IHostedService, IMessageHandlerCallback
    {
        WorkshopManagementDBContext _dbContext;
        IMessageHandler _messageHandler;

        public EventHandler(IMessageHandler messageHandler, WorkshopManagementDBContext dbContext)
        {
            _messageHandler = messageHandler;
            _dbContext = dbContext;
        }

        public void Start()
        {
            _messageHandler.Start(this);
        }

        public void Stop()
        {
            _messageHandler.Stop();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _messageHandler.Start(this);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _messageHandler.Stop();
            return Task.CompletedTask;
        }

        public async Task<bool> HandleMessageAsync(string messageType, string message)
        {
            JObject messageObject = MessageSerializer.Deserialize(message);
            try
            {
                switch (messageType)
                {
                    case "CustomerRegistered":
                        await HandleAsync(messageObject.ToObject<CustomerRegistered>());
                        break;
                    case "CustomerUpdated":
                        await HandleAsync(messageObject.ToObject<CustomerUpdated>());
                        break;
                    case "VehicleRegistered":
                        await HandleAsync(messageObject.ToObject<VehicleRegistered>());
                        break;
                    case "VehicleUpdated":
                        await HandleAsync(messageObject.ToObject<VehicleUpdated>());
                        break;
                    case "MaintenanceJobPlanned":
                        await HandleAsync(messageObject.ToObject<MaintenanceJobPlanned>());
                        break;
                    case "MaintenanceJobUpdated":
                        await HandleAsync(messageObject.ToObject<MaintenanceJobUpdated>());
                        break;
                    case "MaintenanceJobFinished":
                        await HandleAsync(messageObject.ToObject<MaintenanceJobFinished>());
                        break;
                }
            }
            catch (Exception ex)
            {
                string messageId = messageObject.Property("MessageId") != null ? messageObject.Property("MessageId").Value<string>() : "[unknown]";
                Log.Error(ex, "Error while handling {MessageType} message with id {MessageId}.", messageType, messageId);
            }

            // always akcnowledge message - any errors need to be dealt with locally.
            return true;
        }

        private async Task<bool> HandleAsync(VehicleRegistered e)
        {
            Log.Information("Register Vehicle: {LicenseNumber}, {Brand}, {Type}, Owner Id: {OwnerId}",
                e.LicenseNumber, e.Brand, e.Type, e.OwnerId);

            try
            {
                await _dbContext.Vehicles.AddAsync(new Vehicle
                {
                    LicenseNumber = e.LicenseNumber,
                    Brand = e.Brand,
                    Type = e.Type,
                    OwnerId = e.OwnerId
                });
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Skipped adding vehicle with license number {e.LicenseNumber}.");
            }

            return true;
        }

        private async Task<bool> HandleAsync(VehicleUpdated e)
        {
            Log.Information("Updated Vehicle: {LicenseNumber}, {Brand}, {Type}, Owner Id: {OwnerId}",
                   e.LicenseNumber, e.Brand, e.Type, e.OwnerId);

            try
            {
                using (var transaction = await _dbContext.Database.BeginTransactionAsync())
                {
                    var vehicle = await _dbContext.Vehicles.FirstAsync(v => v.LicenseNumber == e.LicenseNumber);

                    vehicle.LicenseNumber = e.LicenseNumber;
                    vehicle.Brand = e.Brand;
                    vehicle.Type = e.Type;
                    vehicle.OwnerId = e.OwnerId;

                    _dbContext.Vehicles.Update(vehicle);

                    await _dbContext.SaveChangesAsync();

                    await transaction.CommitAsync();
                }
            }
            catch (DbUpdateException)
            {
                Console.WriteLine($"Skipped updating vehicle with license number {e.LicenseNumber}.");
            }

            return true;
        }

        private async Task<bool> HandleAsync(CustomerRegistered e)
        {
            Log.Information("Register Customer: {CustomerId}, {Name}, {TelephoneNumber}",
                e.CustomerId, e.Name, e.TelephoneNumber);

            try
            {
                using (var transaction = await _dbContext.Database.BeginTransactionAsync())
                {
                    await _dbContext.Customers.AddAsync(new Customer
                    {
                        CustomerId = e.CustomerId,
                        Name = e.Name,
                        TelephoneNumber = e.TelephoneNumber
                    });

                    await _dbContext.SaveChangesAsync();

                    await transaction.CommitAsync();
                }
            }
            catch (DbUpdateException)
            {
                Log.Warning("Skipped adding customer with customer id {CustomerId}.", e.CustomerId);
            }

            return true;
        }

        private async Task<bool> HandleAsync(CustomerUpdated e)
        {
            Log.Information("Updated Customer: {CustomerId}, {Name}, {TelephoneNumber}",
                e.CustomerId, e.Name, e.TelephoneNumber);

            try
            {
                using (var transaction = await _dbContext.Database.BeginTransactionAsync())
                {
                    var customer = await _dbContext.Customers.FirstAsync(c => c.CustomerId == e.CustomerId);

                    customer.CustomerId = e.CustomerId;
                    customer.Name = e.Name;
                    customer.TelephoneNumber = e.TelephoneNumber;

                    _dbContext.Customers.Update(customer);

                    await _dbContext.SaveChangesAsync();

                    await transaction.CommitAsync();
                }
            }
            catch (DbUpdateException)
            {
                Log.Warning("Skipped adding customer with customer id {CustomerId}.", e.CustomerId);
            }

            return true;
        }

        private async Task<bool> HandleAsync(MaintenanceJobPlanned e)
        {
            Log.Information("Register Maintenance Job: {JobId}, {StartTime}, {EndTime}, {CustomerName}, {LicenseNumber}",
                e.JobId, e.StartTime, e.EndTime, e.CustomerInfo.Name, e.VehicleInfo.LicenseNumber);

            try
            {
                using (var transaction = await _dbContext.Database.BeginTransactionAsync())
                {
                    // determine customer
                    Customer customer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.CustomerId == e.CustomerInfo.Id);
                    if (customer == null)
                    {
                        customer = new Customer
                        {
                            CustomerId = e.CustomerInfo.Id,
                            Name = e.CustomerInfo.Name,
                            TelephoneNumber = e.CustomerInfo.TelephoneNumber
                        };
                    }

                    // determine vehicle
                    Vehicle vehicle = await _dbContext.Vehicles.FirstOrDefaultAsync(v => v.LicenseNumber == e.VehicleInfo.LicenseNumber);
                    if (vehicle == null)
                    {
                        vehicle = new Vehicle
                        {
                            LicenseNumber = e.VehicleInfo.LicenseNumber,
                            Brand = e.VehicleInfo.Brand,
                            Type = e.VehicleInfo.Type,
                            OwnerId = customer.CustomerId
                        };
                    }

                    // insert maintetancejob
                    await _dbContext.MaintenanceJobs.AddAsync(new MaintenanceJob
                    {
                        Id = e.JobId,
                        StartTime = e.StartTime,
                        EndTime = e.EndTime,
                        Customer = customer,
                        Vehicle = vehicle,
                        WorkshopPlanningDate = e.StartTime.Date,
                        Description = e.Description
                    });
                    await _dbContext.SaveChangesAsync();

                    await transaction.CommitAsync();
                }
            }
            catch (DbUpdateException)
            {
                Log.Warning("Skipped adding maintenance job with id {JobId}.", e.JobId);
            }

            return true;
        }

        private async Task<bool> HandleAsync(MaintenanceJobUpdated e)
        {
            Log.Information("Updated Maintenance Job: {JobId}, {StartTime}, {EndTime}, {CustomerName}, {LicenseNumber}",
                  e.JobId, e.StartTime, e.EndTime, e.CustomerInfo.Name, e.VehicleInfo.LicenseNumber);

            try
            {
                using (var transaction = await _dbContext.Database.BeginTransactionAsync())
                {
                    var customer = await _dbContext.Customers
                        .FirstOrDefaultAsync(c => c.CustomerId == e.CustomerInfo.Id);

                    if (customer == null)
                        throw new InvalidOperationException($"Customer not found {e.CustomerInfo.Id}");

                    customer.CustomerId = e.CustomerInfo.Id;
                    customer.Name = e.CustomerInfo.Name;
                    customer.TelephoneNumber = e.CustomerInfo.TelephoneNumber;

                    var vehicle = await _dbContext.Vehicles
                        .FirstOrDefaultAsync(v => v.LicenseNumber == e.VehicleInfo.LicenseNumber);

                    if (vehicle == null)
                        throw new InvalidOperationException($"Vehicle not found {e.VehicleInfo.LicenseNumber}");

                    vehicle.LicenseNumber = e.VehicleInfo.LicenseNumber;
                    vehicle.Brand = e.VehicleInfo.Brand;
                    vehicle.OwnerId = customer.CustomerId;
                    vehicle.Type = e.VehicleInfo.Type;

                    var job = await _dbContext.MaintenanceJobs
                        .FirstOrDefaultAsync(j => j.Id == e.JobId);

                    if (job == null)
                        throw new InvalidOperationException($"Job not found {e.JobId}");

                    job.Id = e.JobId;
                    job.StartTime = e.StartTime;
                    job.EndTime = e.EndTime;
                    job.Customer = customer;
                    job.Vehicle = vehicle;

                    // update maintetancejob

                    _dbContext.MaintenanceJobs.Update(job);

                    await _dbContext.SaveChangesAsync();

                    await transaction.CommitAsync();
                }
            }
            catch (InvalidOperationException ex)
            {
                Log.Warning($"Skipped updating maintenance job: {ex.Message}");
            }
            catch (DbUpdateException)
            {
                Log.Warning("Skipped updating maintenance job with id {JobId}.", e.JobId);
            }

            return true;
        }

        private async Task<bool> HandleAsync(MaintenanceJobFinished e)
        {
            Log.Information("Finish Maintenance job: {JobId}, {ActualStartTime}, {EndTime}",
                e.JobId, e.StartTime, e.EndTime);

            try
            {
                using (var transaction = await _dbContext.Database.BeginTransactionAsync())
                {
                    // insert maintetancejob
                    var job = await _dbContext.MaintenanceJobs.FirstOrDefaultAsync(j => j.Id == e.JobId);
                    job.ActualStartTime = e.StartTime;
                    job.ActualEndTime = e.EndTime;
                    job.Notes = e.Notes;
                    await _dbContext.SaveChangesAsync();

                    await transaction.CommitAsync();
                }
            }
            catch (DbUpdateException)
            {
                Log.Warning("Skipped adding maintenance job with id {JobId}.", e.JobId);
            }

            return true;
        }
    }
}
