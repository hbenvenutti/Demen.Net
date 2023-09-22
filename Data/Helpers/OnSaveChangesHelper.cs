using Demen.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Demen.Data.Helpers;

public static class OnSaveChangesHelper
{
	private static readonly IDictionary<EntityState, Action<BaseEntity>> Map =
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

		if (!Map.ContainsKey(entry.State)) return;

		Map[entry.State](entity);
	}
}
