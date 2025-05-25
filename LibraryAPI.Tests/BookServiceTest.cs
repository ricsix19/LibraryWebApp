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
        var service = new BookService(logger: null, context);
        var book = new Book{Title = "Test", Author = "Test Author", Publisher = "Test Publisher", ReleaseYear = 2025};

        var created = await service.AddBookAsync(book);
        var books = await service.GetAllBooksAsync();

        Assert.Single(books);
        Assert.Equal("Test", books[0].Title);
        Assert.Equal("Test Author", books[0].Author);
        Assert.Equal("Test Publisher", books[0].Publisher);
        Assert.Equal(2025, books[0].ReleaseYear);
    }
    [Fact]
    public async Task DeleteAsync_ShouldRemoveBook()
    {
        var context = GetInMemoryDbContext();
        var service = new BookService(logger: null, context);
        var book = new Book { Title = "To Delete", Author = "Someone", Publisher = "Publisher", ReleaseYear = 2022 };
        await service.AddBookAsync(book);

        var result = await service.DeleteBookAsync(book.Id);
        var all = await service.GetAllBooksAsync();

        // Assert.True();
        Assert.Empty(all);
    }
}
