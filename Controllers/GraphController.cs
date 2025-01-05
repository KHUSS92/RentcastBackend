using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/[controller]")]
public class GraphController : ControllerBase
{
    private readonly Neo4jService _neo4jService;

    public GraphController(Neo4jService neo4jService)
    {
        _neo4jService = neo4jService;
    }

    // GET: api/graph/nodes
    [HttpGet("nodes")]
    public async Task<IActionResult> GetNodes()
    {
        try
        {
            var nodes = await _neo4jService.ExecuteQueryWithResultsAsync<object>("MATCH (n) RETURN n");
            return Ok(nodes);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error fetching nodes: {ex.Message}");
        }
    }

    // GET: api/graph/relationships
    [HttpGet("relationships")]
    public async Task<IActionResult> GetRelationships()
    {
        try
        {
            var relationships = await _neo4jService.ExecuteQueryWithResultsAsync<object>(
                "MATCH ()-[r]->() RETURN r");
            return Ok(relationships);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error fetching relationships: {ex.Message}");
        }
    }

    // POST: api/graph/nodes
    [HttpPost("nodes")]
    public async Task<IActionResult> CreateNode([FromQuery] string label, [FromBody] Dictionary<string, object> properties)
    {
        try
        {
            if (string.IsNullOrEmpty(label))
            {
                return BadRequest("Label is required.");
            }

            await _neo4jService.CreateNodeAsync(label, properties);
            return Ok("Node created successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error creating node: {ex.Message}");
        }
    }

    // POST: api/graph/relationships
    [HttpPost("relationships")]
    public async Task<IActionResult> CreateRelationship([FromBody] RelationshipRequest relationship)
    {
        try
        {
            if (string.IsNullOrEmpty(relationship.SourceId) ||
                string.IsNullOrEmpty(relationship.TargetId) ||
                string.IsNullOrEmpty(relationship.RelationshipType))
            {
                return BadRequest("SourceId, TargetId, and RelationshipType are required.");
            }

            await _neo4jService.CreateRelationshipAsync(relationship.SourceId, relationship.RelationshipType, relationship.TargetId);
            return Ok("Relationship created successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error creating relationship: {ex.Message}");
        }
    }
}

// DTO for creating relationships
public class RelationshipRequest
{
    public string SourceId { get; set; }
    public string TargetId { get; set; }
    public string RelationshipType { get; set; }
}
