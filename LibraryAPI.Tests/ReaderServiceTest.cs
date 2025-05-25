using LibraryAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Tests;

public class ReaderServiceTest
{
    private LibraryDB GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<LibraryDB>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        
        return new LibraryDB(options);
    }
    
    
}