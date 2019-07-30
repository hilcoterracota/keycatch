using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace keycatch.Contex
{
    public class SampekeyDbContex : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public SampekeyDbContex()
        { }

        public SampekeyDbContex(DbContextOptions<SampekeyDbContex> options) : base(options)
        { }

        public virtual DbSet<IdentityUser> User { get; set; }
        public virtual DbSet<IdentityRole> Role { get; set; }
        public virtual DbSet<IdentityUserClaim<string>> UserClaim { get; set; }
        public virtual DbSet<IdentityUserLogin<string>> UserLogin { get; set; }
        public virtual DbSet<IdentityUserRole<string>> UserRole { get; set; }
        public virtual DbSet<IdentityUserToken<string>> UserToken { get; set; }
        public virtual DbSet<IdentityRoleClaim<string>> RoleClaim { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("SQL_SAMPEKEY"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityUser>().ToTable("T_USER");
            modelBuilder.Entity<IdentityRole>().ToTable("T_ROLE");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("T_USER_CLAIM");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("T_ROLE_CLAIM");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("T_USER_LOGIN");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("T_USER_ROLE");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("T_USER_TOKEN");
        }
    }
}