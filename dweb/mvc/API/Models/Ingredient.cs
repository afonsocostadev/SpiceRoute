using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Models;

public class Ingredient
{
    /// <summary>
    /// Identifier for a certain ingredient
    /// </summary>
    [Key]
    public int IngredientId { get; set; }
    
    [Required(ErrorMessage = "Ingredient Name is required")]
    [StringLength(20)]
    public string Name { get; set; } = string.Empty;
    
    [StringLength(500)]
    public string Description { get; set; } = string.Empty;
    
    [JsonIgnore]
    public List<Category> Categories { get; set; } = [];
    
    public List<RecipeIngredient> RecipeIngredients { get; set; } = [];
}