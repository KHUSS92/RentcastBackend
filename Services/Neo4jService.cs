using Neo4jClient;

public class Neo4jService
{
    private readonly IGraphClient _graphClient;

    public Neo4jService(IGraphClient graphClient)
    {
        _graphClient = graphClient;
    }

    public async Task ExecuteQueryAsync(string query, Dictionary<string, object> parameters = null)
    {
        try
        {
            await _graphClient.Cypher
                .Match(query)
                .WithParams(parameters ?? new Dictionary<string, object>())
                .ExecuteWithoutResultsAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error executing query: {ex.Message}");
            throw;
        }
    }

    public async Task<IEnumerable<T>> ExecuteQueryWithResultsAsync<T>(string query, Dictionary<string, object> parameters = null)
    {
        try
        {
            return await _graphClient.Cypher
                .Match(query)
                .WithParams(parameters ?? new Dictionary<string, object>())
                .Return(result => result.As<T>())
                .ResultsAsync;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error executing query with results: {ex.Message}");
            throw;
        }
    }

    public async Task CreateNodeAsync(string label, Dictionary<string, object> properties)
    {
        try
        {
            await _graphClient.Cypher
                .Create($"(n:{label} $props)")
                .WithParam("props", properties)
                .ExecuteWithoutResultsAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating node: {ex.Message}");
            throw;
        }
    }

    public async Task CreateRelationshipAsync(string sourceId, string relationshipType, string targetId)
    {
        try
        {
            await _graphClient.Cypher
                .Match("(a {id: $sourceId}), (b {id: $targetId})")
                .WithParams(new
                {
                    sourceId,
                    targetId
                })
                .Merge($"(a)-[:{relationshipType}]->(b)")
                .ExecuteWithoutResultsAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating relationship: {ex.Message}");
            throw;
        }
    }
}
