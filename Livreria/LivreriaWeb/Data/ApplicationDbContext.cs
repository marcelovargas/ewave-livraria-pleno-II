using System;
using System.Collections.Generic;
using System.Text;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LivreriaWeb.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            base.OnModelCreating(builder);

            //builder.Entity<Usuario>().ToTable("Usuarios").Property(p => p.Id).HasColumnName("Id");
            //builder.Entity<IdentityRole>().ToTable("Roles").Property(p => p.Id).HasColumnName("Id");

            //builder.Entity<IdentityUserClaim<long>>().ToTable("UserClaim");
            //builder.Entity<IdentityUserLogin<long>>().ToTable("UserLogin");
            //builder.Entity<IdentityUserRole<long>>().ToTable("UserRole");
            //builder.Entity<IdentityUserToken<long>>().ToTable("UserToken");
            //builder.Entity<IdentityRoleClaim<long>>().ToTable("RoleClaim");
        }
    }
}
