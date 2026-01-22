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
        var response = ApiResponseMapper
            .FromResult(result, "Menu obtenido correctamente");

        if (!response.Success)
            return BadRequest(response);

        return Ok(response);
    }
}