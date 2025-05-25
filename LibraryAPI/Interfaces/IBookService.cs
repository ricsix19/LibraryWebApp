using LibraryApp.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Interfaces;

public interface IBookService
{
    Task<List<Book?>> GetAllBooksAsync();
    
    Task<Book?> GetBookById(int id);
    
    Task<ActionResult<Book?>> AddBookAsync(Book? book);

    Task UpdateBooksAsync(int id, Book book);

    Task<ActionResult<Book>> DeleteBookAsync(int id);
}