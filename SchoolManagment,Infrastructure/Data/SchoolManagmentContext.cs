using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SchoolManagment.Domain.Domain;
using SchoolManagment.Domain.Shared;

namespace SchoolManagment.Infrastructure.Data
{
    public class SchoolManagmentContext : DbContext
    {
        public SchoolManagmentContext(DbContextOptions options):base (options)
        {
            
        }

        public DbSet<EventWrapper> DomainEvents { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
