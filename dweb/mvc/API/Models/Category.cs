using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class Category
{
    /// <summary>
    /// Identifier for a certain category
    /// </summary>
    [Key]
    public int CategoryId { get; set; }
    
    [Required(ErrorMessage = "Category Name is required")]
    [StringLength(20)]
    public string Name { get; set; } = string.Empty;
    
    [StringLength(20)]
    public string Details { get; set; } = string.Empty;
    
    public List<Ingredient> Ingredients { get; set; } = [];
    
}