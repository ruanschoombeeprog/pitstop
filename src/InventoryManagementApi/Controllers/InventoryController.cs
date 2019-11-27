using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryManagementApi.Commands;
using InventoryManagementApi.Commands.Executors;
using InventoryManagementApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pitstop.InventoryManagementApi.Domain.Exceptions;
using Serilog;

namespace InventoryManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly ICommandExecutor commandExecutor;

        public InventoryController(ICommandExecutor commandExecutor)
        {
            this.commandExecutor = commandExecutor;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllInventory()
        {
            var command = new GetAllInventory();

            try
            {
                var response = await commandExecutor.RunAsync<IEnumerable<Inventory>>(command);

                return Ok(response);
            }
            catch (System.Exception ex)
            {
                string errorMessage = "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.";
                Log.Error(ex, errorMessage);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{productCode}")]
        public async Task<IActionResult> GetInventoryByProductCode([FromRoute] string productCode)
        {
            var command = new GetInventoryByCode(productCode);

            try
            {
                if (ModelState.IsValid)
                {
                    var response = await commandExecutor.RunAsync<Inventory>(command);

                    return Ok(response);
                }
                return BadRequest();
            }
            catch (System.Exception ex)
            {
                string errorMessage = "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.";
                Log.Error(ex, errorMessage);
                ModelState.AddModelError("ErrorMessage", errorMessage);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public async Task<IActionResult> RegisterInventory(RegisterInventory command)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await commandExecutor.RunAsync(command);

                    return Ok();
                }
                return BadRequest();
            }
            catch (InventoryRuleViolationException ex)
            {
                return StatusCode(StatusCodes.Status409Conflict, new InventoryRuleViolation(ex.Message));
            }
            catch (Exception ex)
            {
                string errorMessage = "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.";
                Log.Error(ex, errorMessage);
                ModelState.AddModelError("ErrorMessage", errorMessage);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateInventory(UpdateInventory command)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await commandExecutor.RunAsync(command);

                    return Ok();
                }
                return BadRequest();
            }
            catch (InventoryRuleViolationException ex)
            {
                return StatusCode(StatusCodes.Status409Conflict, new InventoryRuleViolation(ex.Message));
            }
            catch (Exception ex)
            {
                string errorMessage = "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.";
                Log.Error(ex, errorMessage);
                ModelState.AddModelError("ErrorMessage", errorMessage);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok();
        }



        [HttpPut("{productCode}")]
        public async Task<IActionResult> UseInventoryItem(string productCode, UseInventoryItem command)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await commandExecutor.RunAsync(command);

                    return Ok();
                }
                return BadRequest();
            }
            catch (InventoryRuleViolationException ex)
            {
                return StatusCode(StatusCodes.Status409Conflict, new InventoryRuleViolation(ex.Message));
            }
            catch (Exception ex)
            {
                string errorMessage = "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.";
                Log.Error(ex, errorMessage);
                ModelState.AddModelError("ErrorMessage", errorMessage);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}