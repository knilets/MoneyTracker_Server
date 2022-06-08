using MoneyTracker.Storage.Models.Enums;
using MoneyTracker.Storage.Models.Interfaces;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyTracker.Storage.Models.Entities;

public class Transaction : IBaseModel
{
    public int Id { get; set; }
    
    [Required]
    public int UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    public virtual User User { get; set; }

    [Required]
    public int WalletId { get; set; }
    [ForeignKey(nameof(WalletId))]
    public virtual Wallet Wallet { get; set; }

    [Required]
    [DefaultValue(0)]
    public int CategoryId { get; set; }
    [ForeignKey(nameof(CategoryId))]
    public virtual Category Category { get; set; }

    [Required]
    public SignType SignId { get; set; }
    [ForeignKey(nameof(SignId))]
    public virtual Sign Sign { get; set; }

    public double Sum { get; set; }

    public string Description { get; set; }

    public DateTime CreatedAt { get; set; }
}