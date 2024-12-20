using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ChatApplication.DataAccess.Entities;
public class Chat
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Type { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool Active { get; set; }

    public ICollection<User> Users { get; set; } = new List<User>();
    public ICollection<Message> Messages { get; set; } = new List<Message>();
}

internal sealed class ChatConfiguration : IEntityTypeConfiguration<Chat>
{
    public void Configure(EntityTypeBuilder<Chat> builder)
    {
        builder.ToTable("Chats");
        builder.HasKey(x => x.Id);

        builder.HasMany<User>(x=> x.Users)
            .WithMany(x => x.Chats)
            .UsingEntity(x => x.ToTable("UserChats"));

        builder.HasMany<Message>(x => x.Messages)
            .WithOne(x => x.Chat)
            .HasForeignKey(x => x.ChatId);
    }
}
