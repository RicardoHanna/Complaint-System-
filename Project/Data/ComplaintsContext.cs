using Microsoft.EntityFrameworkCore;
using Project.Models;
using System;
using System.Collections.Generic;

namespace Project.Data
{
    public class ComplaintsUserContext : DbContext
    {
        public ComplaintsUserContext()
        {
        }

        public ComplaintsUserContext(DbContextOptions<ComplaintsUserContext> options)
        : base(options)
        {

        }
        public DbSet<Complaint> Complaint { get; set; }
        public IEnumerable<object> Goal { get; internal set; }
        public DbSet<ComplaintUser> ComplaintUser { get; set; }

        internal object FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }

}

