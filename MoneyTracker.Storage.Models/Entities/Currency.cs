using MoneyTracker.Storage.Models.Interfaces;

namespace MoneyTracker.Storage.Models.Entities;

public class Currency : IBaseModel
{
    public int Id { get; set; }

    /// <summary>
    /// Should be Unique
    /// </summary>
    public string Code { get; set; }
    public string Symbol { get; set; }

    public DateTime CreatedAt { get; set; }
}