using Microsoft.EntityFrameworkCore;

#region AppDbContext
public class AppDbContext : DbContext
{
    public DbSet<Person> People { get; set; }
    public DbSet<Animal> Animals { get; set; }
    public DbSet<Dog> Dogs { get; set; }
    public DbSet<Cat> Cats { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<Bike> Bikes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        
        optionsBuilder.UseSqlServer(
            @"Data Source=DESKTOP-UT0B80T;Initial Catalog=Northwind;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region TPH
        modelBuilder.Entity<Person>()
            .HasDiscriminator<string>("PersonType")
            .HasValue<CustomerPerson>("Customer")
            .HasValue<EmployeePerson>("Employee");
        #endregion

        #region TPT
        modelBuilder.Entity<Animal>().ToTable("Animals");
        modelBuilder.Entity<Dog>().ToTable("Dogs");
        modelBuilder.Entity<Cat>().ToTable("Cats");
        #endregion

        #region TPC
        modelBuilder.Entity<Car>().UseTpcMappingStrategy();
        modelBuilder.Entity<Bike>().UseTpcMappingStrategy();
        #endregion
    }
}
#endregion
