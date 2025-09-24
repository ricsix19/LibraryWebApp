using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Shared.Models;

public class Reader
{
    public int Id { get; set; }
    
    [Required]
    public virtual string Name { get; set; }
    
    [Required]
    public virtual string Location { get; set; }
    
    [Range(1900, 2100)]
    public int DateOfBirth { get; set; }
    
    public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();
}