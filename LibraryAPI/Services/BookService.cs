using LibraryAPI.Data;
using LibraryApp.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Services;

public class BookService
{
    private readonly LibraryDB _context;

    public BookService(LibraryDB context)
    {
        _context = context;
    }

    public async Task<List<Book>> GetAllBooksAsync()
    {
        return await _context.Books.ToListAsync();
    }

    public async Task<Book> GetBookByIdAsync(int id)
    {
        return await _context.Books.FindAsync(id);
    }

    public async Task<Book> CreateBookAsync(Book book)
    {
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
        return book;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null)
        {
            return false;
        }
        
        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
        return true;
    }
    
    public async Task<bool> UpdateAsync(int id, Book book)
    {
        if (id != book.Id)
            return false;

        var exists = await _context.Books.AnyAsync(b => b.Id == id);
        if (!exists)
            return false;

        _context.Entry(book).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return true;
    }

}