using LibraryAPI.Data;
using LibraryAPI.Services;
using LibraryApp.Shared.Models;
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

    [Fact]
    public async Task AddReaderAsync_ShouldAddReader()
    {
        var context = GetInMemoryDbContext();
        var service = new ReaderService(logger: null, context);
        var reader = new Reader { Name = "Test", Location = "Somewhere", DateOfBirth = 1990};
        
        var result = await service.AddReaderAsync(reader);
        var readers = await service.GetAllReadersAsync();
        
        Assert.Single(readers);
        Assert.Equal("Test", readers[0].Name);
        Assert.Equal("Somewhere", readers[0].Location);
        Assert.Equal(1990, readers[0].DateOfBirth);
    }
    
    [Fact]
    public async Task DeleteReaderAsync_ShouldRemoveReader()
    {
        var context = GetInMemoryDbContext();
        var service = new ReaderService(logger: null, context);
        var reader = new Reader { Name = "Test", Location = "Nowhere" };
        await service.AddReaderAsync(reader);

        var result = await service.DeleteReaderAsync(reader.Id);
        var readers = await service.GetAllReadersAsync();

        Assert.Empty(readers);
    }
}