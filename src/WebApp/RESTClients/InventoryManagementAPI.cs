using Microsoft.Extensions.Configuration;
using Pitstop.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WebApp.Commands;

namespace WebApp.RESTClients
{
    public class InventoryManagementAPI : IInventoryManagementAPI
    {
        private readonly IInventoryManagementAPI restClient;

        public InventoryManagementAPI(IConfiguration config, HttpClient httpClient)
        {

            string apiHostAndPort = config.GetSection("APIServiceLocations").GetValue<string>("InventoryManagementAPI");
            httpClient.BaseAddress = new Uri($"http://{apiHostAndPort}/api");
            restClient = RestService.For<IInventoryManagementAPI>(httpClient);
        }

        public async Task<List<Inventory>> GetInventory()
        {
            return await restClient.GetInventory();
        }

        public async Task<Inventory> GetInventoryByProductCode(string productCode)
        {
            try
            {
                return await restClient.GetInventoryByProductCode(productCode);
            }
            catch (ApiException ex)
            {
                if (ex.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task RegisterInventory(RegisterInventory command)
        {
            await restClient.RegisterInventory(command);
        }

        public async Task UpdateInventory(UpdateInventory command)
        {
            await restClient.UpdateInventory(command);
        }

        public async Task UseInventoryItem(string productCode, UseInventoryItem command)
        {
            await restClient.UseInventoryItem(productCode, command);
        }
    }
}
