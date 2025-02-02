﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pitstop.CustomerManagementAPI.Commands;
using Pitstop.CustomerManagementAPI.DataAccess;
using Pitstop.CustomerManagementAPI.Events;
using Pitstop.CustomerManagementAPI.Mappers;
using Pitstop.CustomerManagementAPI.Model;
using Pitstop.Infrastructure.Messaging;
using System;
using System.Threading.Tasks;

namespace Pitstop.Application.CustomerManagementAPI.Controllers
{
    [Route("/api/[controller]")]
    public class CustomersController : Controller
    {
        IMessagePublisher _messagePublisher;
        CustomerManagementDBContext _dbContext;

        public CustomersController(CustomerManagementDBContext dbContext, IMessagePublisher messagePublisher)
        {
            _dbContext = dbContext;
            _messagePublisher = messagePublisher;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _dbContext.Customers.ToListAsync());
        }

        [HttpGet]
        [Route("{customerId}", Name = "GetByCustomerId")]
        public async Task<IActionResult> GetByCustomerId(string customerId)
        {
            var customer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.CustomerId == customerId);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterCustomer command)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // insert customer
                    Customer customer = command.MapToCustomer();
                    _dbContext.Customers.Add(customer);
                    await _dbContext.SaveChangesAsync();

                    // send event
                    CustomerRegistered e = command.MapToCustomerRegistered();
                    await _messagePublisher.PublishMessageAsync(e.MessageType, e, "");

                    // return result
                    return CreatedAtRoute("GetByCustomerId", new { customerId = customer.CustomerId }, customer);
                }
                return BadRequest();
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
                return StatusCode(StatusCodes.Status500InternalServerError);
                throw;
            }
        }

        [HttpPut]
        [Route("{customerId}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] string customerId, [FromBody] UpdateCustomer command)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existingCustomer = await _dbContext.Customers
                        .FirstOrDefaultAsync(o => o.CustomerId == customerId);

                    if (existingCustomer == null)
                        return NotFound();

                    existingCustomer.CustomerId = command.CustomerId;
                    existingCustomer.Address = command.Address;
                    existingCustomer.City = command.City;
                    existingCustomer.EmailAddress = command.EmailAddress;
                    existingCustomer.Name = command.Name;
                    existingCustomer.PostalCode = command.PostalCode;
                    existingCustomer.TelephoneNumber = command.TelephoneNumber;

                    try
                    {
                        _dbContext.Customers.Update(existingCustomer);
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError("", "Unable to save changes. " +
                              "Try again, and if the problem persists " +
                              "see your system administrator.");
                        return StatusCode(StatusCodes.Status500InternalServerError);
                        throw;
                    }

                    await _dbContext.SaveChangesAsync();

                    var e = command.MapToCustomerUpdated();
                    await _messagePublisher.PublishMessageAsync(e.MessageType, e, "");

                    return Ok();
                }
                return BadRequest();
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                      "Try again, and if the problem persists " +
                      "see your system administrator.");
                return StatusCode(StatusCodes.Status500InternalServerError);
                throw;
            }
        }
    }
}
