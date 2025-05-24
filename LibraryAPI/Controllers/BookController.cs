using LibraryAPI.Data;
using LibraryAPI.Interfaces;
using LibraryAPI.Services;
using LibraryApp.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Controllers;

[ApiController]
[Route("Books")]
public class BookController : ControllerBase
{
    private readonly ILogger<IBookService> _logger;
    private readonly LibraryDB _context;

    public BookController(ILogger<IBookService> logger, LibraryDB context)
    {
        _logger = logger;
        _context = context;
    }
    
    public async Task<List<Book?>> GetAllBooksAsync()
    {
        return await _context.Books.ToListAsync();
    }

    public async Task<Book?> GetBookById(int id)
    {
        return await _context.Books.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<ActionResult<Book?>> AddBookAsync(Book? book)
    {
        if (book == null)
            return new BadRequestResult();
    
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
        return new ActionResult<Book?>(book);
    }

    public Task UpdateBooksAsync(int id, Book book)
    {
        throw new NotImplementedException();
    }

    public Task<ActionResult<Book>> DeleteBookAsync(int id)
    {
        throw new NotImplementedException();
    }
}