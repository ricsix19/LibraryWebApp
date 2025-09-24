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
        var entity = await _context.Loans.FindAsync(id);
        if (entity == null)
        {
            throw new Exception("The loan that you are trying to update does not exist");
        }
        
        entity.Id = loan.Id;
        entity.UserId = loan.UserId;
        entity.BookId = loan.BookId;
        entity.LoanDate = loan.LoanDate;
        entity.LoanReturnDate = loan.LoanReturnDate;
        
        await _context.SaveChangesAsync();
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