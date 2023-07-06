namespace WebApplication1.Data
{
    public class PostgreSqlContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=shopdb;Username=postgres;Password=postgres;IncludeErrorDetail=true");
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var addedEntries = this.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added).Select(e => e.Entity);

            foreach(var entry in addedEntries) 
            {
                var item = entry as BaseEntity;

                if (item != null)
                    item.CteatedDate = DateTime.UtcNow;
            }

            var modifiedEntries = this.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Modified).Select(e => e.Entity);

            foreach (var entry in modifiedEntries)
            {
                var item = entry as BaseEntity;

                if (item != null)
                    item.UpdatedDate = DateTime.UtcNow;
            }


            return base.SaveChangesAsync(cancellationToken);
        }
        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderItem> OrderItems { get; set; } = null!;
        public DbSet<Review> Reviews { get; set; } = null!;

    }
}
