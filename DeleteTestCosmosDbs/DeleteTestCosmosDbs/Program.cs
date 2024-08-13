using Microsoft.Azure.Cosmos;

using var client = new CosmosClient("AccountEndpoint=https://localhost:8081/;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==");

using var iterator = client.GetDatabaseQueryIterator<DatabaseProperties>();

while (iterator.HasMoreResults)
{
    foreach (DatabaseProperties dbProps in await iterator.ReadNextAsync())
    {
        if (dbProps.Id.Contains("test", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine($"Deleting '{dbProps.Id}'");
            await client.GetDatabase(dbProps.Id).DeleteAsync();
        }
    }
}
