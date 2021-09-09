# Neo4j.Driver With Azure Functions DI


Using Neo4j.Driver with Azure Functions Dependency Injection

Two classes of importance here - the first is the `Startup` class, which constructs the Driver and adds it to the builder.
The second is the actual `DriverWithDI` class, and the injection of the `IDriver` is done in the constructor.

So. If you create an Azure Function you will need to do a few things:

1. Change the 'function' - make it non-static, and add a constructor taking in the things you want.
2. Add a 'startup' class, and ensure you add the `assembly` bit!
```
[assembly: FunctionsStartup(typeof(Neo4jDriver.AzureFunction.DependencyInjection.Startup))]
```
Above the namespace!!