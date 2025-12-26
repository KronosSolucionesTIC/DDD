namespace DDD.Domain.Entities;

public class MenuItem
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string Route { get; private set; }
    public string Icon { get; private set; }
    public int Order { get; private set; }
    public bool IsActive { get; private set; }

    protected MenuItem() { }

    public MenuItem(string title, string route, string icon, int order)
    {
        Id = Guid.NewGuid();
        Title = title;
        Route = route;
        Icon = icon;
        Order = order;
        IsActive = true;
    }
}
