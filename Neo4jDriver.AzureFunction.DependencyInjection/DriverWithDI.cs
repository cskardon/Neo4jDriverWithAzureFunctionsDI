using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Neo4j.Driver;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Neo4jDriver.AzureFunction.DependencyInjection
{
    public class DriverWithDI
    {
        private readonly IDriver _driver;

        public DriverWithDI(IDriver driver)
        {
            _driver = driver;
        }

        [FunctionName("DriverWithDI")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]
            HttpRequest req,
            ILogger log)
        {

            var session = _driver.AsyncSession();
            var count = await session.ReadTransactionAsync(async tx =>
            {
                var res = await tx.RunAsync("MATCH (n) RETURN COUNT(n)");
                var record = await res.SingleAsync();
                return record["COUNT(n)"].As<int>();
            });

            var responseMessage = $"There are {count} nodes in the database";
            return new OkObjectResult(responseMessage);
        }
    }
}