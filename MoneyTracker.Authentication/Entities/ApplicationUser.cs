using Microsoft.AspNetCore.Identity;
using MoneyTracker.Storage.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyTracker.Authentication.Entities;

public class ApplicationUser : IdentityUser
{
    public int UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    public virtual User User { get; set; }
}