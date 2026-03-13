using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class Course
{
    [Key]
    public int CourseId { get; set; }
    
    [Required(ErrorMessage = "Name is required")]
    [StringLength(20)]
    public string Name { get; set; } = string.Empty;
}