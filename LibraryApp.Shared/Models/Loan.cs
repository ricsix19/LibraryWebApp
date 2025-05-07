using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Models;

public class Loan
{
    public int Id { get; set; }
    
    [Required]
    public int BookId { get; set; }
    
    [Required]
    public int UserId { get; set; }
    
    [Required]
    public DateTime LoanDate { get; set; }
    
    [Required]
    public DateTime LoanReturnDate { get; set; }
}