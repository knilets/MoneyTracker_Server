using MoneyTracker.Storage.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace MoneyTracker.Storage.Models.Entities;

public class Sign
{
    [Key]
    public SignType Id { get; set; }
    public string Symbol { get; set; }
}