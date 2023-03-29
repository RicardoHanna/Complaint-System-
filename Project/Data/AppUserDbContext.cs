using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Project.Data
{
    public class AppUserDbContext : IdentityDbContext
    {

   
        public AppUserDbContext(DbContextOptions<AppUserDbContext> options) : base(options)
        {

        }
       
    }

}
