using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Models;

[PrimaryKey(nameof(RecipeId), nameof(IngredientId))]
public class RecipeIngredient
{
    
    public int RecipeId { get; set; }
    public int IngredientId { get; set; }
    public int Quantity { get; set; }
    
    [ForeignKey(nameof(MeasurementUnit))]
    public int? UnitIdFk { get; set; }
    
    public MeasurementUnits? MeasurementUnit { get; set; }
}