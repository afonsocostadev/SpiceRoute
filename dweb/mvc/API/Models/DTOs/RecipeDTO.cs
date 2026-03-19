namespace API.Models.DTOs;

public class RecipeDTO
{
   public int RecipeId { get; set; }
   public string Name { get; set; }
   public string Description { get; set; }
   public string Preparation { get; set; }
   public int Portions { get; set; }
   public int CookingTime { get; set; }
   public int PrepTime { get; set; }
}
