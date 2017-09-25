using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CentsTrail.Models;
using CentsTrail.Models.BankAccounts;
using CentsTrail.Models.Periods;
using CentsTrail.Models.Categories;
using CentsTrail.Models.Transactions;
using Microsoft.AspNetCore.Identity;

namespace CentsTrail.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<BankAccount>()
                   .HasOne(ba => ba.User)
                   .WithMany(u => u.BankAccounts)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<BankAccount>()
                   .HasIndex(ba => new { ba.UserId, ba.Name })
                   .IsUnique();

            builder.Entity<Category>()
                   .HasOne(c => c.User)
                   .WithMany(u => u.Categories)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Category>()
                   .HasIndex(c => new { c.UserId, c.Name })
                   .IsUnique();

            builder.Entity<Period>()
                   .HasOne(p => p.User)
                   .WithMany(u => u.Periods)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Period>()
                   .HasIndex(p => new { p.UserId, p.Name })
                   .IsUnique();

            builder.Entity<Transaction>()
                   .HasOne(t => t.User)
                   .WithMany(u => u.Transactions)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ApplicationUser>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
        }

        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<Period> Periods { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
