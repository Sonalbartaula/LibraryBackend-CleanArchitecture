
using LibraryBackend_CleanArchitecture.Entities;
using LibraryBackend_CleanArchitecture.Model;
using LibraryBackend_CleanArchitecture.Model.Dashboard;
using Microsoft.EntityFrameworkCore;


namespace LibraryBackend_CleanArchitecture.Data
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        { }
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<Activity> Activities { get; set; }

        
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    // Store enums as strings to match existing values
        //    modelBuilder.Entity<Student>()
        //        .Property(s => s.Status)
        //        .HasConversion<string>();

        //    modelBuilder.Entity<Student>()
        //        .Property(s => s.Type)
        //        .HasConversion<string>();

        //    base.OnModelCreating(modelBuilder);
        //}

        public DbSet<transaction> Transactions { get; set; }



    }

}