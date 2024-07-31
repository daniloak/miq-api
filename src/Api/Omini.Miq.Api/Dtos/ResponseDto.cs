using Omini.Miq.Shared.Entities;

namespace Omini.Miq.Api.Dtos;

internal record ResponseDto
{
    public static ResponseDto ApiSuccess<T>(T body)
    {
        return new ResponseDto<T>()
        {
            Data = body!
        };
    }

    public static ResponsePagedDto<T> ApiSuccess<T>(PagedResult<T> body)
    {
        return new ResponsePagedDto<T>()
        {
            Data = body?.Response!,
            CurrentPage = body.CurrentPage,
            PageSize = body.PageSize,
            RowCount = body.RowCount,
            PageCount = body.PageCount,
        };
    }
}

internal sealed record ResponseDto<T> : ResponseDto
{
    public T Data { get; init; }
}

internal sealed record ResponsePagedDto<T> : ResponseDto
{
    public int CurrentPage { get; init; }
    public int PageSize { get; init; }
    public int RowCount { get; init; }
    public int PageCount { get; init; }
    public IEnumerable<T> Data { get; init; }
}