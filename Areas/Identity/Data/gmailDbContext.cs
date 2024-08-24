using gmail.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using gmail.Models;

namespace gmail.Data
{
    public class gmailDbContext : IdentityDbContext<gmailUser>
    {
        public gmailDbContext(DbContextOptions<gmailDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DbSet<Email> Emails { get; set; }
    }
}
