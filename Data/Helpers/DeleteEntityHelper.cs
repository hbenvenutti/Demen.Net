using Demen.Common.Enums;
using Demen.Data.Entities;

namespace Demen.Data.Helpers;

public static class DeleteEntityHelper
{
	public static void DeleteEntity<T>(this T entity) where T : BaseEntity
	{
		entity.DeletedAt = DateTime.UtcNow;
		entity.Status = Status.Deleted;
	}
}
