using LibraryAPI.Data;
using LibraryAPI.Interfaces;
using LibraryApp.Shared.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Services;

public class BookService : IBookService
{
    private readonly ILogger<Book> _logger;
    private readonly LibraryDB _context;

    public BookService(ILogger<Book> logger, LibraryDB context)
    {
        _context = context;
        _logger = logger;
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
        {
            return new BadRequestResult();
        }
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
        
        return new ActionResult<Book?>(book);
    }

    public async Task UpdateBooksAsync(int id, Book book)
    {
        _context.Entry(book).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task<ActionResult<Book>> DeleteBookAsync(int id)
    {
        var entity = await _context.Books.FindAsync(id);
        if (entity == null)
        {
            return new NotFoundResult();
        }
        _context.Books.Remove(entity);
        await _context.SaveChangesAsync();

        return new ActionResult<Book>(entity);
    }
}