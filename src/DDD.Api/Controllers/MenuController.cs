using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/menu")]
public class MenuController : ControllerBase
{
    private readonly GetMenuQuery _query;

    public MenuController(GetMenuQuery query)
    {
        _query = query;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _query.ExecuteAsync();
        return Ok(result);
    }
}
