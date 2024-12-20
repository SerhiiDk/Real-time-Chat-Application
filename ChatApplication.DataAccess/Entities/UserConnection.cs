using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatApplication.DataAccess.Entities;
public class UserConnection
{
    public required string UserId { get; set; }
    public required string Name { get; set; }
    public required string ConnectionId { get; set; }
    public bool IsConnected { get; set; }
    public DateTime FirstConnectedUTC { get; set; }
    public DateTime LastConnectedUTC { get; set; }
}

internal sealed class UserConnectionConfiguration : IEntityTypeConfiguration<UserConnection>
{
    public void Configure(EntityTypeBuilder<UserConnection> builder)
    {
        builder.ToTable("UserConnections");
        builder.Property(x => x.UserId).HasMaxLength(128);
        builder.Property(x => x.Name).HasMaxLength(128);
        builder.Property(x => x.ConnectionId).HasMaxLength(128);
        builder.HasKey(x => x.UserId);
    }
}
