using MoneyTracker.Storage.Models.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyTracker.Storage.Models.Entities;

public class Wallet : IBaseModel, IUserCreatable
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    [Required]
    public int CurrencyId { get; set; }
    [ForeignKey(nameof(CurrencyId))]
    public virtual Currency Currency { get; set; }

    public int CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }

    public bool IsActive { get; set; }
}