using Microsoft.EntityFrameworkCore;
using TopSpeed.Web.Models;

namespace TopSpeed.Web.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)        // Configuration setup : Constructor concept
        {
            
        }

        public DbSet<Brand> Brand { get; set; }                      // Whatever give the name like name { }-->that name is creating the table name . example : Brand { }
    
    }
}
