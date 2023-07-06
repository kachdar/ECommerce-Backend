namespace WebApplication1.Data
{
    public class SqlServerContext: DbContext
    {
        public SqlServerContext(DbContextOptions<SqlServerContext> options)
            :base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { 
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=myshopdb;Trusted_Connection=true;TrustServerCertificate=true;");
        }
        public DbSet<Book> Books { get; set; }
    }
}
