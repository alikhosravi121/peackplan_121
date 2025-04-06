using Microsoft.EntityFrameworkCore;
using peackplan.Entities;

namespace peackplan;
/// <summary>
/// /معرفی دیتا بیس
/// </summary>
public class AppDbContext(DbContextOptions options):DbContext(options)
{
    public DbSet<UserEntity> Users { get; set; } = null!;
    
    public DbSet<TeamWorkEntity> TeamWorks { get; set; } = null!;
    public DbSet<CompanyEntity> Companies { get; set; } = null!;
    
}