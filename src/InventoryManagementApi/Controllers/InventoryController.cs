using System.Threading.Tasks;
using InventoryManagementApi.Commands;
using InventoryManagementApi.Commands.Executors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost]
        public async Task<IActionResult> RegisterInventory(RegisterInventory command)
        {
            try
            {
                await commandExecutor.ExecuteAsync(command);
            }
            catch (System.Exception ex)
            {
                Log.Error($"Unable to process command: {command.GetType().Name} : {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError);
                throw;
            }

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateInventory(UpdateInventory command)
        {
            try
            {
                await commandExecutor.ExecuteAsync(command);
            }
            catch (System.Exception ex)
            {
                Log.Error($"Unable to process command: {command.GetType().Name} : {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError);
                throw;
            }

            return Ok();
        }

        

        [HttpPut("{productCode}")]
        public async Task<IActionResult> UseInventoryItem(string productCode, UseInventoryItem command)
        {
            try
            {
                await commandExecutor.ExecuteAsync(command);
            }
            catch (System.Exception ex)
            {
                Log.Error($"Unable to process command: {command.GetType().Name} : {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError);
                throw;
            }

            return Ok();
        }
    }
}