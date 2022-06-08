using MoneyTracker.Storage.Models.Interfaces;

namespace MoneyTracker.Storage.Models.Entities;

public class Category : IBaseModel, IUserCreatable
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsShared { get; set; }
    public int CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
}