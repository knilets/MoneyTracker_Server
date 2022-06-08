namespace MoneyTracker.Logic.Categories;

public class CategoryCreateUpdateRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsShared { get; set; }
}