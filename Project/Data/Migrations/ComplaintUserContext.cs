using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Data.Migrations
{
    public class ComplaintUserContext : DbContext
    {
        public ComplaintUserContext()
        {
        }

        public ComplaintUserContext(DbContextOptions<ComplaintUserContext> options)
        : base(options)
        {

        }
        public DbSet<ComplaintUser> ComplaintUser { get; set; }
        
    }
}
