using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Shared.Models;

public class Loan
{
    public int Id { get; set; }
    
    [Required]
    public int BookId { get; set; }

    public Book Book { get; set; } = default!;
    
    [Required]
    public int UserId { get; set; }

    public Reader Reader { get; set; } = default!;
    
    [Required]
    public DateTime LoanDate { get; set; }
    
    [Required]
    public DateTime LoanReturnDate { get; set; }
}