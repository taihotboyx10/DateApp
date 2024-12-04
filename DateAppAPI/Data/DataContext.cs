namespace DateAppAPI.Data;
using Entities;
using Microsoft.EntityFrameworkCore;

public class DataContext(DbContextOptions options) : DbContext(options)
{
    // public DataContext(DbContextOptions options) : base(options)
    // {
    // }
    public DbSet<AppUser> AppUsers { get; set; }
}