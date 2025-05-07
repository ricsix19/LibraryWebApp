using LibraryAPI.Data;
using LibraryAPI.Services;
using LibraryApp.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
    private readonly BookService _service;

    public BookController(BookService service)
    {
        _service = service;
    }
    
    //HTTP GET
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
    {
        return await _service.GetAllBooksAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Book>> GetBook(int id)
    {
        var book = await _service.GetBookByIdAsync(id);
        if (book == null)
        {
            return NotFound();
        }
        return book;
    }

    [HttpPost]
    public async Task<ActionResult<Book>> PostBook(Book book)
    {
        await _service.CreateBookAsync(book);
        return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutBook(int id, Book book)
    {
        var update = await _service.UpdateAsync(id, book);
        if (!update)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Book>> DeleteBook(int id)
    {
        var succes = await _service.DeleteAsync(id);
        if (!succes)
        {
            return NotFound();
        }
        return NoContent();
    }
}