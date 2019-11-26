using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pitstop.ViewModels;
using PitStop.Controllers;
using Refit;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebApp.Commands;
using WebApp.Models;
using WebApp.RESTClients;

namespace WebApp.Controllers
{
    public class InventoryManagementController : Controller
    {
        private readonly IWorkshopManagementAPI workshopManagementAPI;
        private readonly IInventoryManagementAPI inventoryManagementAPI;
        private readonly ResiliencyHelper resiliencyHelper;

        public InventoryManagementController(IWorkshopManagementAPI workshopManagementAPI, IInventoryManagementAPI inventoryManagementAPI, ILogger<InventoryManagementController> logger)
        {
            this.workshopManagementAPI = workshopManagementAPI;
            this.inventoryManagementAPI = inventoryManagementAPI;
            resiliencyHelper = new ResiliencyHelper(logger);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return await resiliencyHelper.ExecuteResilient(async () =>
            {
                var model = new InventoryManagementViewModel
                {
                    Inventories = await inventoryManagementAPI.GetInventory()
                };
                return View(model);
            }, View("Offline", new InventoryOfflineViewModel()));
        }

        [HttpGet]
        public async Task<IActionResult> Details(string productCode)
        {
            return await resiliencyHelper.ExecuteResilient(async () =>
            {
                var inventory = await inventoryManagementAPI.GetInventoryByProductCode(productCode);

                var model = new InventoryDetailViewModel()
                {
                    Inventory = inventory
                };

                return View(model);
            }, View("Offline", new InventoryOfflineViewModel()));
        }

        public IActionResult New()
        {
            var inventory = new Pitstop.Models.Inventory();
            inventory.ProductCode = GetProductCode();
            var model = new InventoryNewViewModel()
            {
                Inventory = inventory
            };

            return View(model);
        }

        public string GetProductCode()
        {
            var length = 12;
            var random = new Random();
            var charPool = "0123456789ABCDEF";
            var productCodeBuilder = new StringBuilder();

            while (length > 0)
            {
                productCodeBuilder.Append(charPool[(int)(random.NextDouble() * charPool.Length)]);
                length--;
            }

            return productCodeBuilder.ToString();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm] InventoryNewViewModel inputModel)
        {
            if (ModelState.IsValid)
            {
                return await resiliencyHelper.ExecuteResilient(async () =>
                {

                    try
                    {
                        var command = new RegisterInventory(Guid.NewGuid(),
                            inputModel.Inventory.ProductCode,
                            inputModel.Inventory.Description,
                            inputModel.Inventory.Quantity,
                            inputModel.Inventory.UnitPrice);

                        await inventoryManagementAPI.RegisterInventory(command);
                    }
                    catch (ApiException ex)
                    {
                        if (ex.StatusCode == HttpStatusCode.Conflict)
                        {
                            var content = await ex.GetContentAsAsync<InventoryRuleViolation>();
                            inputModel.Error = content.ErrorMessage;

                            return View("New", inputModel);
                        }
                    }

                    return RedirectToAction("Index");

                }, View("Offline", new InventoryOfflineViewModel()));
            }
            else
            {
                return View("New", inputModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string productCode)
        {
            return await resiliencyHelper.ExecuteResilient(async () =>
            {
                var model = new InventoryEditViewModel
                {
                    Inventory = await inventoryManagementAPI.GetInventoryByProductCode(productCode)
                };
                return View(model);
            }, View("Offline", new InventoryOfflineViewModel()));
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromForm] InventoryEditViewModel inputModel)
        {
            if (ModelState.IsValid)
            {
                return await resiliencyHelper.ExecuteResilient(async () =>
                {
                    try
                    {
                        var command = new UpdateInventory(Guid.NewGuid(),
                        Guid.NewGuid(),
                        inputModel.Inventory.ProductCode,
                        inputModel.Inventory.Description,
                        inputModel.Inventory.Quantity,
                        inputModel.Inventory.UnitPrice);

                        await inventoryManagementAPI.UpdateInventory(command);

                    }
                    catch (ApiException ex)
                    {
                        if (ex.StatusCode == HttpStatusCode.Conflict)
                        {
                            var content = await ex.GetContentAsAsync<InventoryRuleViolation>();
                            inputModel.Error = content.ErrorMessage;

                            return View("Edit", inputModel);
                        }
                    }

                    return RedirectToAction("Index");
                }, View("Offline", new InventoryOfflineViewModel()));

            }
            else
            {
                return View("Edit", inputModel);
            }

        }
    }
}