using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Data
{
    public class GetUserIdContext:DbContext
    {
        public GetUserIdContext()
        {
        }

        public GetUserIdContext(DbContextOptions<GetUserIdContext> options)
        : base(options)
        {

        }
        public DbSet<GetUserId> GetUserId { get; set; }

    }

}
