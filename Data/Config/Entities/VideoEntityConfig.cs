using System.Diagnostics.CodeAnalysis;
using Demen.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demen.Data.Config.Entities;

[ExcludeFromCodeCoverage]
public class VideoEntityConfig : IEntityTypeConfiguration<VideoEntity>
{
	public void Configure(EntityTypeBuilder<VideoEntity> builder)
	{
		builder
			.ToTable(
				name: "videos",
				table => table
					.CreateStatusConstraint(name: "CK_video_status")
			);

		builder
			.ConfigureBaseEntityProperties();

		// ---- keys -------------------------------------------------------- //

		builder
			.HasKey(video => video.Id)
			.HasName("PK_video_id");

		builder
			.HasOne(video => video.Manager)
			.WithMany(manager => manager.Videos)
			.HasForeignKey(video => video.ManagerId)
			.OnDelete(DeleteBehavior.Restrict)
			.HasConstraintName("FK_video_manager_id");

		// ---- columns ----------------------------------------------------- //

		builder
			.Property(video => video.ManagerId)
			.HasColumnName("manager_id")
			.IsRequired();

		builder
			.Property(video => video.Title)
			.HasColumnName("title")
			.HasColumnType("varchar")
			.IsRequired();

		builder
			.Property(video => video.Description)
			.HasColumnName("description")
			.HasColumnType("varchar")
			.IsRequired();

		builder
			.Property(video => video.ThumbnailUrl)
			.HasColumnName("thumbnail_url")
			.HasColumnType("varchar")
			.IsRequired();

		builder
			.Property(video => video.YoutubeId)
			.HasColumnName("youtube_id")
			.HasColumnType("varchar")
			.IsRequired();

		builder
			.Property(video => video.PublishedAt)
			.HasColumnName("published_at")
			.IsRequired();
	}
}
