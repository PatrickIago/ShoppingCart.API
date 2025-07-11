using Microsoft.AspNetCore.Identity;
namespace ShoppingCart.Domain.Models.Login;
public class ApplicationUser : IdentityUser
{
    public string Nome { get; set; }
}
