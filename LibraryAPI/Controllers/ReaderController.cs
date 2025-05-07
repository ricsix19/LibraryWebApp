using LibraryAPI.Data;
using LibraryApp.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReaderController : ControllerBase
{
    private readonly LibraryDB _context;

    public ReaderController(LibraryDB context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Reader>>> GetReaders()
    {
        return await _context.Readers.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Reader>> GetReader(int id)
    {
        var reader = await _context.Readers.FindAsync(id);
        if (reader == null)
        {
            return NotFound();
        }
        
        return reader;
    }

    [HttpPost]
    public async Task<ActionResult<Reader>> PostReader(Reader reader)
    {
        _context.Readers.Add(reader);
        await _context.SaveChangesAsync();
        
        return CreatedAtAction(nameof(GetReader), new { id = reader.Id }, reader);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutReader(int id, Reader reader)
    {
        if (id != reader.Id)
        {
            return BadRequest();
        }
        
        _context.Entry(reader).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Readers.Any(r => r.Id == id))
            {
                return NotFound();
            }
            throw;
        }
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReader(int id)
    {
        var reader = await _context.Readers.FindAsync(id);
        if (reader == null)
        {
            return NotFound();
        }
        
        _context.Readers.Remove(reader);
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
}