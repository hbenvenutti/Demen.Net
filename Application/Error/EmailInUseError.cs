namespace Demen.Application.Error;

public struct EmailInUseError : IApplicationError
{
	public const string Message = "Email is already in use.";
}
