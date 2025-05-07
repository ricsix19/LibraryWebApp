using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Shared.Models;

public class Reader
{
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Location { get; set; }
    
    [Range(1900, 2100)]
    public int DateOfBirth { get; set; }
}