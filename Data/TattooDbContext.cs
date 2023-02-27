using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace tattoo.Data
{
    public class TattooDbContext : IdentityDbContext<Customer>
    {
        public TattooDbContext(DbContextOptions<TattooDbContext> options)
            : base(options)
        {
        }
        public DbSet<Categorie> Categories { get; set; }
        public DbSet<Employer> Employers { get; set; }
        public DbSet<Rezervation> Rezervations { get; set; }
        public DbSet<Tattoo> Tattoos { get; set; }
    }
}