using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Neo4j.Driver;

[assembly: FunctionsStartup(typeof(Neo4jDriver.AzureFunction.DependencyInjection.Startup))]
namespace Neo4jDriver.AzureFunction.DependencyInjection
{
    
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<IDriver>((s) =>
            {
                var driver = GraphDatabase.Driver("neo4j://localhost:7687", AuthTokens.Basic("neo4j", "neo"));
                return driver;
            });
        }
    }
}