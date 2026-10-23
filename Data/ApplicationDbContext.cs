using AI_Powered_Ticketing_and_Support_System.Feature.CategoryService.Model;
using AI_Powered_Ticketing_and_Support_System.Feature.CommentService.Model;
using AI_Powered_Ticketing_and_Support_System.Feature.TicketService.Model;
using AI_Powered_Ticketing_and_Support_System.Feature.UserService.Model;
using Microsoft.EntityFrameworkCore;
namespace AI_Powered_Ticketing_and_Support_System.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<TicketStatusHistory> TicketStatusHistories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.Category)
            .WithMany(c => c.Tickets)
            .HasForeignKey(t => t.CategoryId)
            .IsRequired(false);


        // Ticket -> User who created the ticket
        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.CreatedByUser)
            .WithMany()
            .HasForeignKey(t => t.CreatedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Ticket -> User assigned to handle the ticket (optional)
        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.AssignedToUser)
            .WithMany()
            .HasForeignKey(t => t.AssignedToUserId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
