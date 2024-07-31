using Omini.Miq.Application.Abstractions.Messaging;
using Omini.Miq.Domain.Repositories;
using Omini.Miq.Domain.Sales;
using Omini.Miq.Shared.Entities;

namespace Omini.Miq.Business.Queries;

public class GetAllPromissoriesQuery : IQuery<Promissory>
{
    public GetAllPromissoriesQuery() { }

    public GetAllPromissoriesQuery(QueryFilter queryFilter, PaginationFilter paginationFilter)
    {
        QueryFilter = queryFilter;
        PaginationFilter = paginationFilter;
    }
    public PaginationFilter PaginationFilter { get; set; }
    public QueryFilter QueryFilter { get; set; }

    public class GetAllPromissoriesQueryHandler : IQueryHandler<GetAllPromissoriesQuery, Promissory>
    {
        private readonly IPromissoryRepository _promissoryRepository;
        public GetAllPromissoriesQueryHandler(IPromissoryRepository promissoryRepository)
        {
            _promissoryRepository = promissoryRepository;
        }

        public async Task<PagedResult<Promissory>> Handle(GetAllPromissoriesQuery request, CancellationToken cancellationToken)
        {
            var promissories = await _promissoryRepository.GetAll(
                currentPage: request.PaginationFilter.CurrentPage,
                pageSize: request.PaginationFilter.PageSize,
                orderByField: request.PaginationFilter.OrderBy,
                sortDirection: request.PaginationFilter.Direction,
                queryField: request.QueryFilter.QueryField,
                queryValue: request.QueryFilter.QueryValue,
                cancellationToken);

            return promissories;
        }
    }
}