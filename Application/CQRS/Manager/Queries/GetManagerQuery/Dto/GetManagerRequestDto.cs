namespace Demen.Application.CQRS.Manager.Queries.GetManagerQuery.Dto;

public class GetManagerRequestDto
{
	public Guid Id { get; init; }

	public GetManagerRequestDto(Guid id)
	{
		Id = id;
	}
}
