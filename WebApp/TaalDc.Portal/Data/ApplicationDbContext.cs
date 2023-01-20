using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TaalDc.Portal.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<UserProfile> UserProfiles { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
       
 
        base.OnModelCreating(builder);
        
        builder.Entity<UserProfile>().HasKey(i => i.UserId);
        
        builder.Entity<UserProfile>().Property(i => i.UserId).UseHiLo("userseq", "dbo");

        builder.Entity<UserProfile>().HasIndex(i => i.Identity).IsUnique();
        
        builder.Entity<UserProfile>().Property(i => i.Identity).IsRequired();

    }
}