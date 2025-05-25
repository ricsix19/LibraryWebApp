using LibraryAPI.Data;
using LibraryAPI.Interfaces;
using LibraryApp.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReaderController : ControllerBase
{
     private readonly ILogger<IReaderService> _logger;
    private readonly IReaderService _readerService;

    public ReaderController(ILogger<IReaderService> logger, IReaderService readerService)
    {
        _logger = logger;
        _readerService = readerService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Reader>>> GetReadersAsync()
    {
        var readers = await _readerService.GetAllReadersAsync();
        return Ok(readers);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Reader?>> GetReaderByIdAsync(int id)
    {
        var reader = await _readerService.GetReaderAsync(id);
        if (reader == null)
        {
            _logger.LogWarning($"Reader with id {id} not found");
            return NotFound();
        }
        return Ok(reader);
    }

    [HttpPost]
    public async Task<ActionResult<Reader>> CreateReaderAsync(Reader reader)
    {
        if (reader == null)
        {
            return BadRequest();
        }
        
        var result = await _readerService.AddReaderAsync(reader);
        if (result.Result is BadRequestResult)
        {
            return BadRequest();
        }
        
        return CreatedAtAction(nameof(GetReaderByIdAsync), new { id = reader.Id }, reader);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateReaderAsync(int id, Reader reader)
    {
        if (reader == null || id != reader.Id)
        {
            return BadRequest();
        }
        
        var existing = await _readerService.GetReaderAsync(id);
        if (existing == null)
        {
            return NotFound();
        }

        await _readerService.UpdateReaderAsync(id, reader);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteReaderAsync(int id)
    {
        var existing = await _readerService.GetReaderAsync(id);
        if (existing == null)
        {
            return NotFound();
        }
        
        await _readerService.DeleteReaderAsync(id);
        return NoContent();
    }
}