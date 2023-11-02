using Demen.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demen.Data.Config.Entities;

public class ChannelEntityConfig : IEntityTypeConfiguration<ChannelEntity>
{
	public void Configure(EntityTypeBuilder<ChannelEntity> builder)
	{
		builder.ToTable(
			name: "channels",
			buildAction: table => table
				.CreateStatusConstraint(name: "CK_channel_status")
		);

		builder
			.ConfigureBaseEntityProperties();

		// ---- keys -------------------------------------------------------- //

		builder
			.HasKey(channel => channel.Id)
			.HasName("PK_channel_id");

		// ---- columns ----------------------------------------------------- //

		builder
			.Property(channel => channel.Name)
			.HasColumnName("name")
			.HasColumnType("varchar")
			.IsRequired();

		builder
			.Property(channel => channel.Description)
			.HasColumnName("description")
			.HasColumnType("varchar")
			.IsRequired(false);

		builder
			.Property(channel => channel.YoutubeId)
			.HasColumnName("youtube_id")
			.HasColumnType("varchar")
			.IsRequired();

		builder
			.Property(channel => channel.ThumbnailUrl)
			.HasColumnName("thumbnail_url")
			.HasColumnType("varchar")
			.IsRequired();

		// ---- indexes ----------------------------------------------------- //

		builder
			.HasIndex(channel => channel.YoutubeId)
			.HasDatabaseName("IX_channel_youtube_id")
			.IsUnique();
	}
}
