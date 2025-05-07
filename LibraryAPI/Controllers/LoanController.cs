using LibraryAPI.Data;
using LibraryApp.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoanController: ControllerBase
{
    private readonly LibraryDB _context;

    public LoanController(LibraryDB context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Loan>>> GetLoan()
    {
        return await _context.Loans.ToListAsync();
    }

    [HttpGet("reader/{readerId}")]
    public async Task<ActionResult<IEnumerable<Loan>>> GetLoanByReaderId(int readerId)
    {
        return await _context.Loans.Where(l => l.UserId == readerId).ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<Loan>> PostLoan(Loan loan)
    {
        if (loan.LoanDate.Date < DateTime.Now.Date)
        {
            return BadRequest("Loan out date cannot be earlier than today");
        }

        if (loan.LoanReturnDate <= loan.LoanDate)
        {
            return BadRequest("Loan return date cannot be earlier than Loan out date");
        }

        var ReaderExist = await _context.Readers.AnyAsync(r => r.Id == loan.UserId);
        var BookExist = await _context.Books.AnyAsync(b => b.Id == loan.BookId);

        if (!ReaderExist || !BookExist)
        {
            return BadRequest("Reader or Book does not exist in the database!");
        }
        
        _context.Loans.Add(loan);
        await _context.SaveChangesAsync();
        
        return CreatedAtAction(nameof(GetLoan), new { loanId = loan.Id }, loan);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLoan(int id)
    {
        var loan = await _context.Loans.FindAsync(id);
        if (loan == null)
        {
            return NotFound();
        }
        
        _context.Loans.Remove(loan);
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
}