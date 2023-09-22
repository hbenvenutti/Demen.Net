using Demen.Common.Enums;

namespace Demen.Application.Error;

public interface IApplicationError
{
	static string Message { get; set; } = null!;
}
