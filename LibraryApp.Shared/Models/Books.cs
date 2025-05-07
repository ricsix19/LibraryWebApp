using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Shared.Models;

public class Book
{
    public int Id { get; set; }
    
    [Required]
    public string Title { get; set; }
    
    [Required]
    public string Author { get; set; }
    
    [Required]
    public string Publisher { get; set; }
    
    [Range(0, 2100)]
    public int ReleaseYear { get; set; }
}