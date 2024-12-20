using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ChatApplication.DataAccess.Entities;
public class Message
{
    public int Id { get; set; }
    public string? Content { get; set; }
    public DateTime CreateAt { get; set; }
    public string? Sentiment { get; set; }

    public int SenderId { get; set; }
    public User Sender { get; set; } = null!;

    public int ChatId { get; set; }
    public Chat Chat { get; set; } = null!;
}

internal sealed class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.ToTable("Messages");
        builder.HasKey(x => x.Id);

        builder.HasOne<User>(x => x.Sender)
            .WithMany(x => x.Messages)
            .HasForeignKey(x => x.SenderId);

        builder.HasOne<Chat>(x => x.Chat)
            .WithMany(x => x.Messages)
            .HasForeignKey(x => x.ChatId);
    }
}
