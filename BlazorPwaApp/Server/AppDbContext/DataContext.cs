using BlazorPwaApp.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlazorPwaApp.Server.AppDbContext
{
   public class DataContext : DbContext
   {
      public DataContext(DbContextOptions<DataContext> options) : base(options)
      {

      }

      public DbSet<Country> Countries { get; set; }

      public DbSet<Province> Provinces { get; set; }

      public DbSet<District> Districts { get; set; }

      public DbSet<Facility> Facilities { get; set; }

      public DbSet<UserAccount> UserAccounts { get; set; }
   }
}