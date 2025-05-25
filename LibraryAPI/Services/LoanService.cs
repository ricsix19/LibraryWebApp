using LibraryAPI.Data;
using LibraryAPI.Interfaces;
using LibraryApp.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Services;

public class LoanService : ILoanService
{
    private readonly ILogger<LoanService> _logger;
    private readonly LibraryDB _context;

    public LoanService(ILogger<LoanService> logger, LibraryDB context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<List<Loan>> GetAllLoansAsync()
    {
        return await _context.Loans.ToListAsync();
    }

    public async Task<Loan?> GetLoanAsync(int id)
    {
        return await _context.Loans.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<ActionResult<Loan?>> AddLoanAsync(Loan? loan)
    {
        if (loan == null)
        {
            return new BadRequestResult();
        }
        _context.Loans.Add(loan);
        await _context.SaveChangesAsync();
        
        return new ActionResult<Loan?>(loan);
    }

    public async Task UpdateLoanAsync(int id, Loan? loan)
    {
        if (loan != null)
        {
            _context.Entry(loan).State = EntityState.Modified;
            await _context.SaveChangesAsync();   
        }
    }

    public async Task<ActionResult<Loan?>> DeleteLoanAsync(int id)
    {
        var entity = await _context.Loans.FindAsync(id);
        if (entity == null)
        {
            return new NotFoundResult();
        }
        _context.Loans.Remove(entity);
        await _context.SaveChangesAsync();
        
        return new ActionResult<Loan?>(entity);
    }
}