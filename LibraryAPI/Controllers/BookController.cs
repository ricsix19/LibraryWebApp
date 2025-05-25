using LibraryAPI.Data;
using LibraryAPI.Interfaces;
using LibraryAPI.Services;
using LibraryApp.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
    private readonly ILogger<IBookService> _logger;
    private readonly IBookService _bookService;

    public BookController(ILogger<IBookService> logger, IBookService bookService)
    {
        _logger = logger;
        _bookService = bookService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Book>>> GetAllBooksAsync()
    {
        var books = await _bookService.GetAllBooksAsync();
        return Ok(books); //200-as http informaci kod generalas hogy sikeresen lekertuk az osszes konyvet
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Book?>> GetBookByIdAsync(int id)
    {
        var book = await _bookService.GetBookById(id);
        if (book == null)
        {
            _logger.LogWarning($"Book with id {id} not found");
            return NotFound();
        }
        return Ok(book);
    }

    [HttpPost]
    public async Task<ActionResult<Book>> CreateBookAsync(Book book)
    {
        _logger.LogInformation("Creating book: {0}", book?.Title);
        if (book == null)
        {
            _logger.LogWarning("Book is null");
            return BadRequest();
        }

        try
        {
            var result = await _bookService.AddBookAsync(book);
            if (result.Result is BadRequestResult)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(GetBookByIdAsync), new { id = book.Id }, book);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Valami hiba történt");
            return StatusCode(500, "belső szerverhiba");
        }
        
        // return CreatedAtRoute("GetBookById", new { id = result.Result }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateBookAsync(int id, Book book)
    {
        if (book == null || id != book.Id)
        {
            return BadRequest();
        }
        var existing = await _bookService.GetBookById(id);
        if (existing == null)
        {
            return NotFound();
        }

        await _bookService.UpdateBooksAsync(id, book);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteBookAsync(int id)
    {
        var existing = await _bookService.GetBookById(id);
        if (existing == null)
        {
            return NotFound();
        }
        
        await _bookService.DeleteBookAsync(id);
        return NoContent();
    }
}