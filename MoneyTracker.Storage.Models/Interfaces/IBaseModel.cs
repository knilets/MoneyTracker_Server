using System.ComponentModel.DataAnnotations;

namespace MoneyTracker.Storage.Models.Interfaces;

public interface IBaseModel
{
    [Key]
    public int Id { get; set; }
    /// <summary>
    /// DateTime in UTC.
    /// </summary>
    public DateTime CreatedAt { get; set; }
}