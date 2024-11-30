using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MyChat.Models;

public class MyChatContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public DbSet<User> Users { get; set; }
    public DbSet<ForumTopic> ForumTopics { get; set; }
    public DbSet<ForumReply> ForumReplies { get; set; }

    public MyChatContext(DbContextOptions<MyChatContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ForumTopic>()
            .HasOne(f => f.User)
            .WithMany() 
            .HasForeignKey(f => f.UserId)
            .OnDelete(DeleteBehavior.Restrict); 
    }
}