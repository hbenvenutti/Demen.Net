using Demen.Content.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Demen.Content.Data.HashMaps;

public static class OnSaveChangesHelper
{
	private static IDictionary<EntityState, Action<BaseEntity>> map { get; } =
		new Dictionary<EntityState, Action<BaseEntity>>
	{
		[EntityState.Added] = entity =>
		{
			entity.CreatedAt = DateTime.UtcNow;
			entity.ExternalId = Guid.NewGuid();
		},

		[EntityState.Modified] = entity => entity.UpdatedAt = DateTime.UtcNow
	};

	public static void HandleOnSaveChanges(this EntityEntry entry)
	{
		if (entry.Entity is not BaseEntity entity) return;

		map[entry.State](entity);
	}
}
