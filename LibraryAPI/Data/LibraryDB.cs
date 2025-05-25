using LibraryApp.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Data;

public class LibraryDB : DbContext
{
    public LibraryDB(DbContextOptions<LibraryDB> options) : base(options){}
    
    public DbSet<Book> Books { get; set; }
    public DbSet<Reader> Readers { get; set; }
    public DbSet<Loan> Loans { get; set; }
}