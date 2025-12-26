using DDD.Application.Common;

public class GetMenuQuery
{
    private readonly IMenuRepository _repository;

    public GetMenuQuery(IMenuRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<MenuItemDto>>> ExecuteAsync()
    {
        var menu = await _repository.GetMenuAsync();

        var result = menu.Select(x => new MenuItemDto
        {
            Title = x.Title,
            Route = x.Route,
            Icon = x.Icon
        });

        return Result<IEnumerable<MenuItemDto>>.Success(result);
    }
}
