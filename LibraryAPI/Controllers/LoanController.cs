using LibraryAPI.Data;
using LibraryAPI.Interfaces;
using LibraryApp.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoanController: ControllerBase
{
    private readonly ILogger<ILoanService> _logger;
    private readonly ILoanService _loanService;

    public LoanController(ILogger<ILoanService> logger, ILoanService loanservice)
    {
        _loanService = loanservice;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Loan>>> GetLoan()
    {
        var loans = await _loanService.GetAllLoansAsync();
        return Ok(loans);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<IEnumerable<Loan?>>> GetLoanById(int id)
    {
        var loan = await _loanService.GetLoanAsync(id);
        if (loan == null)
        {
            _logger.LogWarning($"Loan with id {id} not found");
            return NotFound();
        }
        return Ok(loan);
    }
    
    [HttpGet("reader/{readerId:int}")]
    public async Task<ActionResult<IEnumerable<Loan>>> GetLoanByReaderIdAsync(int readerId)
    {
        var loans = await _loanService.GetAllLoansAsync();
        var filteredLoans = loans.Where(l => l.UserId == readerId).ToList();
        return Ok(filteredLoans);
    }
    
    [HttpPost]
    public async Task<ActionResult<Loan>> PostLoan(Loan loan)
    {
        if (loan == null)
        {
            return BadRequest();
        }
        if (loan.LoanDate.Date < DateTime.Now.Date)
        {
            return BadRequest("Loan out date cannot be earlier than today");
        }

        if (loan.LoanReturnDate <= loan.LoanDate)
        {
            return BadRequest("Loan return date cannot be earlier than Loan out date");
        }

        var result = await _loanService.AddLoanAsync(loan);

        if (result.Result is BadRequestResult)
        {
            return BadRequest();
        }
        
        return CreatedAtAction(nameof(GetLoanByReaderIdAsync), new { loanId = loan.Id }, loan);
    }
    
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateLoanAsync(int id, Loan loan)
    {
        if (loan == null || id != loan.Id)
        {
            return BadRequest();
        }
        
        var existing = await _loanService.GetLoanAsync(id);
        if (existing == null)
        {
            return NotFound();
        }

        await _loanService.UpdateLoanAsync(id, loan);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLoan(int id)
    {
        var existing = await _loanService.GetLoanAsync(id);
        if (existing == null)
        {
            return NotFound();
        }
        
        await _loanService.DeleteLoanAsync(id);
        return NoContent();
    }
}