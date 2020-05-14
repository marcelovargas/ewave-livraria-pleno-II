namespace Infrastructure.Configuration
{
    using Entities;
    using Microsoft.EntityFrameworkCore;

    public class ContextBase : DbContext
    {
        public DbSet<Livro> Livros { get; set; }

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
        
        private string GetStringConectionConfig()
        {
            string con = "Data Source=(LocalDb)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\aspnet-teste-20200513040108.mdf;Initial Catalog=LivreriaBD;Integrated Security=True";
            con = "Server = (localdb)\\mssqllocaldb; Database = DevIO.App; Trusted_Connection = True; MultipleActiveResultSets = true";
            return con;
        }

       
    }


}

