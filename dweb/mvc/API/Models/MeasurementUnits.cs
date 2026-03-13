using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

public class MeasurementUnits
{
    [Key]
    public int UnitId { get; set; }
    
    [Required(ErrorMessage = "Unit Name is required")]
    [StringLength(15)]
    public string Name { get; set; } = string.Empty;
    
    public List<RecipeIngredient> RecipeIngredients { get; set; } = [];
}