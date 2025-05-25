using LibraryAPI.Data;
using LibraryAPI.Services;
using LibraryApp.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Tests;

public class LoanServiceTest
{
    private LibraryDB GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<LibraryDB>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new LibraryDB(options);
    }
    [Fact]
    public async Task AddLoanAsync_ShouldAddLoan()
    {
        var context = GetInMemoryDbContext();
        var service = new LoanService(logger: null, context);
        var loan = new Loan { BookId = 1, UserId = 1, LoanDate = DateTime.Now };
        
        var result = await service.AddLoanAsync(loan);
        var loans = await service.GetAllLoansAsync();

        Assert.Single(loans);
        Assert.Equal(1, loans[0].BookId);
        Assert.Equal(1, loans[0].UserId);
        Assert.Equal(DateTime.Now, loans[0].LoanDate);
    }

    [Fact]
    public async Task DeleteLoanAsync_ShouldRemoveLoan()
    {
        var context = GetInMemoryDbContext();
        var service = new LoanService(logger: null, context);
        var loan = new Loan { BookId = 1, UserId = 1, LoanDate = DateTime.Now };
        await service.AddLoanAsync(loan);
        
        var result = await service.DeleteLoanAsync(loan.Id);
        var loans = await service.GetAllLoansAsync();
        
        Assert.Empty(loans);
    }
}