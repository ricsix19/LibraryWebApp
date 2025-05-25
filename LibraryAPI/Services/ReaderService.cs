using LibraryAPI.Data;
using LibraryAPI.Interfaces;
using LibraryApp.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Services;

public class ReaderService : IReaderService
{
    private readonly ILogger<ReaderService> _logger;
    private readonly LibraryDB _context;

    public ReaderService(ILogger<ReaderService> logger, LibraryDB context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<List<Reader>> GetAllReadersAsync()
    {
        return await _context.Readers.ToListAsync();
    }

    public async Task<Reader?> GetReaderAsync(int id)
    {
        return await _context.Readers.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<ActionResult<Reader?>> AddReaderAsync(Reader? reader)
    {
        if (reader == null)
        {
            return new BadRequestResult();
        }
        _context.Readers.Add(reader);
        await _context.SaveChangesAsync();
        
        return new ActionResult<Reader?>(reader);
    }

    public async Task UpdateReaderAsync(int id, Reader? reader)
    {
        if (reader != null)
        {
            _context.Entry(reader).State = EntityState.Modified;
            await _context.SaveChangesAsync();   
        }
    }

    public async Task<ActionResult<Reader?>> DeleteReaderAsync(int id)
    {
        var entity = await _context.Readers.FindAsync(id);
        if (entity == null)
        {
            return new NotFoundResult();
        }
        _context.Readers.Remove(entity);
        await _context.SaveChangesAsync();
        
        return new ActionResult<Reader?>(entity);
    }
}