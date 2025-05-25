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
}