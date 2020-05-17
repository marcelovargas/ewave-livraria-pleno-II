namespace Infrastructure.Configuration
{
    using Entities;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class ContextBase :  DbContext
    {      
        public ContextBase()
        {

        }
        public ContextBase(DbContextOptions<ContextBase> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(GetStringConectionConfig());
            base.OnConfiguring(optionsBuilder);
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

        private string GetStringConectionConfig()
        {
            string con = "Server = (localdb)\\mssqllocaldb; Database =LivreriaDB01; Trusted_Connection = True; MultipleActiveResultSets = true";
            return con;
        }



        public DbSet<Autor> Autores { get; set; }
        public DbSet<Emprestimo> Emprestimos { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Instituicao> Instituicoes { get; set; }
        public DbSet<Livro> Livros { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Leitor> Leitores { get; set; }

    }

}

