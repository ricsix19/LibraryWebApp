using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Shared.Models;

public class Loan
{
    public int Id { get; set; }
    
    [Required]
    public int BookId { get; set; }

    public virtual Book Book { get; set; } = default!;
    
    [Required]
    public int ReaderId { get; set; }

    public virtual Reader Reader { get; set; } = default!;
    
    [Required]
    public DateTime LoanDate { get; set; }
    
    [Required]
    public DateTime LoanReturnDate { get; set; }
}