using Microsoft.AspNetCore.Identity;

namespace API.Models;

public class User : IdentityUser
{   
    public List<Recipe>? Recipes { get; set; } = [];
}