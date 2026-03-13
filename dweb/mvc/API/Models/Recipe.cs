using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Identity;

namespace API.Models;

/// <summary>
/// Recipe Model
/// </summary>
public class Recipe
{
    /// <summary>
    /// Identifier for a certain recipe
    /// </summary>
    [Key]
    public int RecipeId { get; set; }
    
    /// <summary>
    /// Name of the recipe
    /// </summary>
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }    
    
    /// <summary>
    /// Description of the recipe
    /// </summary>
    [StringLength(100)]
    public string Description { get; set; } = string.Empty;
    public DateTime Published { get; set; }
    
    /// <summary>
    /// The actual recipe guide
    /// </summary>
    [Required(ErrorMessage="Preparation is required")]
    [StringLength(1000)]
    public string Preparation  { get; set; } = string.Empty;
    
    /// <summary>
    /// Number of people for the shown units
    /// </summary>
    [Required(ErrorMessage = "Portions are required")]
    public int Portions { get; set; }
    
    public int CookingTime { get; set; }
    public int PrepTime { get; set; }
    public Difficulty Difficulty { get; set; }
    
    public List<RecipeIngredient> RecipeIngredients { get; set; } = [];
    public List<User> Users { get; set; } = [];
    
}
/// <summary>
/// Possible difficulty values to attribute to a recipe
/// </summary>
public enum Difficulty
{
    Easy,
    Medium,
    Hard
}