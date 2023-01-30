using minapi.Entities;
// using Microsoft.EntityFrameworkCore;

namespace minapi.Persistence
{
    public class DevEventsDbContext
    {
        public List<DevEvent> DevEvents { get; set; }

        public DevEventsDbContext()
        {
            DevEvents = new List<DevEvent>();
        }
    }

    // public class DevEventsDbContext : DbContext
    // {
    //     public DevEventsDbContext(DbContextOptions<DevEventsDbContext> options) : base(options) { }
    //     public DbSet<DevEvent> DevEvents { get; set; }
    //     protected override void OnModelCreating(ModelBuilder modelBuilder)
    //     {
    //         modelBuilder.Entity<DevEvent>()
    //             .HasKey(d => d.Id);

    //         modelBuilder.Entity<DevEvent>()
    //             .Property(d => d.Title)
    //             .IsRequired()
    //             .HasColumnType("varchar(200)");

    //         modelBuilder.Entity<DevEvent>()
    //             .Property(d => d.Description)
    //             .IsRequired()
    //             .HasColumnType("varchar(MAX)");

    //         modelBuilder.Entity<DevEvent>()
    //             .ToTable("DevEvents");
            
    //         base.OnModelCreating(modelBuilder);
    //     }
    // }



}