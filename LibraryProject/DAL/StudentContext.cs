using LibraryProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LibraryProject.DAL
{
    public class StudentContext: DbContext
    {
            public StudentContext() : base()
            {
            }
            protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {
                
                modelBuilder.Entity<Student>().ToTable("Student");
                modelBuilder.Entity<Book>().ToTable("Book");
                modelBuilder.Entity<Library>().ToTable("Library");
               
            }
            public DbSet<Student> Students { get; set; }
            public DbSet<Book> Books { get;set; }
            
            public DbSet<Library> Libraries {  get; set; }

        
    }
  }
