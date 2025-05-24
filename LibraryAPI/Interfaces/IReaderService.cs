using LibraryApp.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Interfaces;

public interface IReaderService
{
    Task<List<Reader>> GetReadersAsync();
    
    Task<Reader?> GetReaderAsync(int id);
    
    Task<ActionResult<Reader?>> AddReaderAsync(Reader? reader);
    
    Task UpdateReaderAsync(int id, Reader? reader);
    
    Task<ActionResult<Reader?>> DeleteReaderAsync(int id);
}