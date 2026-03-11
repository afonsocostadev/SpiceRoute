using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace API.Models;

/// <summary>
/// Recipe Model
/// </summary>
public class Recipe
{
    /// <summary>
    /// Id
    /// </summary>
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
    // public string (?) Image {...}
    
    // public int IngredientFK {...}
    
    
}