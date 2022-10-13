public class CustomerContext : DbContext
{
    private readonly bool _withLogging;

    public CustomerContext(bool withLogging = true)
        => _withLogging = withLogging;
    
    public DbSet<Customer> Customers => Set<Customer>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlServer(@"Server=localhost;Database=JsonColumns;User=SA;Password=Abcd5678;Encrypt=false");

        if (_withLogging)
        {
            optionsBuilder
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging();
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>().OwnsOne(customer => customer.Contact).ToJson();

        modelBuilder.Entity<Customer>()
            .Property(c => c.ComputedJsonCountry)
            .HasComputedColumnSql("JSON_VALUE(Contact,'$.Address.Country')");

        modelBuilder.Entity<Customer>()
            .HasIndex(c => c.ComputedJsonCountry);

        modelBuilder.Entity<Country>()
            .ToTable("Countries")
            .Property(c => c.Id)
            .ValueGeneratedNever();
    }
}
