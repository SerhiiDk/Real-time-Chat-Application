using ChatApplication.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChatApplication.DataAccess.Context;
public class ChatDbContext : DbContext
{
    public ChatDbContext(DbContextOptions<ChatDbContext> options) :base(options) { }

    public DbSet<UserConnection> UserConnections { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Chat> Chats { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConnectionConfiguration).Assembly);
    }
}
