using LibraryAPI.Data;
using LibraryAPI.Services;
using LibraryApp.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Tests;

public class BookServiceTest
{
    private LibraryDB GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<LibraryDB>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // új memória DB minden teszthez
            .Options;

        return new LibraryDB(options);
    }
    
    [Fact]
    public async Task CreateAsync_AddBook()
    {
        var context = GetInMemoryDbContext();
        var service = new BookService(context);
        var book = new Book{Title = "Test", Author = "Test Author", Publisher = "Test Publisher", ReleaseYear = 2025};

        var created = await service.CreateBookAsync(book);
        var books = await service.GetAllBooksAsync();

        Assert.Single(books);
        Assert.Equal("Test", books[0].Title);
    }
    [Fact]
    public async Task DeleteAsync_ShouldRemoveBook()
    {
        var context = GetInMemoryDbContext();
        var service = new BookService(context);
        var book = new Book { Title = "To Delete", Author = "Someone", Publisher = "Publisher", ReleaseYear = 2022 };
        await service.CreateBookAsync(book);

        var result = await service.DeleteAsync(book.Id);
        var all = await service.GetAllBooksAsync();

        Assert.True(result);
        Assert.Empty(all);
    }
}
