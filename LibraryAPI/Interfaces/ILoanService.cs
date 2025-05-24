using LibraryApp.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Interfaces;

public interface ILoanService
{
    Task<List<Loan>> GetAllLoansAsync();
    
    Task<Loan?> GetLoanAsync(int id);
    
    Task<ActionResult<Reader?>> AddLoanAsync(Loan? loan);
    
    Task UpdateLoanAsync(int id, Loan? loan);
    
    Task<ActionResult<Loan?>> DeleteLoanAsync(int id);
}