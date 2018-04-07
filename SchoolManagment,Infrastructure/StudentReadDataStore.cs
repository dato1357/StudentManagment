using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SchoolManagment.Infrastructure.ReadModels;

namespace SchoolManagment.Infrastructure
{
    public class StudentReadDataStore : DbContext
    {
        public StudentReadDataStore(DbContextOptions options):base(options)
        {
            
        }
        public DbSet<StudentReadModel> Students { get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
