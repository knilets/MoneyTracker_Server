using MoneyTracker.Storage.Models.Interfaces;

namespace MoneyTracker.Storage.Models.Entities;

public class User : IBaseModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public DateTime CreatedAt { get; set; }
}