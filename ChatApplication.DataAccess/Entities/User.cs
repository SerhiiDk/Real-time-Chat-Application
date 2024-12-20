using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ChatApplication.DataAccess.Entities;
public class User
{
    public int Id { get; set; }
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public ICollection<Chat> Chats { get; set; } = new List<Chat>();
    public ICollection<Message> Messages { get; set; } = new List<Message>();
}

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(x => x.Id);
        
        builder.HasMany<Message>(x => x.Messages)
            .WithOne(x => x.Sender)
            .HasForeignKey(x => x.SenderId);

        builder.HasMany<Chat>(x => x.Chats)
            .WithMany(x => x.Users)
            .UsingEntity(x => x.ToTable("UserChats"));
    }
}