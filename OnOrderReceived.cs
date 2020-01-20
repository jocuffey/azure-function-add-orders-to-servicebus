using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace _4service_dev_functions
{
    public static class OnOrderReceived
    {
        [FunctionName("OnOrderReceived")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Received an order.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var order = JsonConvert.DeserializeObject<Entity>(requestBody);
            log.LogInformation($"Order received for {order.Id} at {order.Created}");
            return new OkObjectResult($"Order received");
        }
    }

     public class Entity
    {
        public string Id { get; set; }
        public string Created { get; set; }
        public string LastUpdated { get; set; }
    }
}
