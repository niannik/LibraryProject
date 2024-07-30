using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet <Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet <User> Users { get;  set; }
        public DbSet <BorrowedBook> BorrowedBooks { get; set;}
        public DbSet<BookCategory> BookCategories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseNpgsql(@"Server=server1.navicat.com; Port= 5432; Database= postgres; user Id = navicat; Password = testnavicat");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // fix problem of postgresql with DateTime in .Net
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(t => t.GetProperties())
                         .Where(p => p.ClrType == typeof(DateTime) || p.ClrType == typeof(DateTime?)))
            {
                property.SetColumnType("timestamp without time zone");
            }


            modelBuilder.Entity<Book>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Book>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Author>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Author>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Category>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Category>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<User>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<User>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<BorrowedBook>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<BorrowedBook>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<BookCategory>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<BookCategory>()
                .HasKey(x => x.Id);


            modelBuilder.Entity<Author>()
                .HasMany(x => x.Books)
                .WithOne(x => x.Author)
                .HasForeignKey(x => x.AuthorId);

            modelBuilder.Entity<Book>()
                .HasMany(x => x.BookCategories)
                .WithOne(x => x.Book)
                .HasForeignKey(x => x.BookId);

            modelBuilder.Entity<Book>()
                .HasMany(x => x.BorrowedBooks)
                .WithOne(x => x.Book)
                .HasForeignKey(x => x.BookId);

            modelBuilder.Entity<User>()
                .HasMany(x => x.BorrowedBooks)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<Category>()
                .HasMany(x => x.BookCategories)
                .WithOne(x => x.Category)
                .HasForeignKey(x => x.CategoryId);

        }


    }
}
