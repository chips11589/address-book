using Application.Common.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Contacts.Queries.GetAutoComplete
{
    public class GetContactAutoCompleteQuery : IRequest<List<ContactAutoCompleteDto>>
    {
        public string SearchQuery { get; set; }
    }

    public class GetContactAutoCompleteQueryHandler : IRequestHandler<GetContactAutoCompleteQuery, List<ContactAutoCompleteDto>>
    {
        private readonly IApplicationReadDbConnection _readDbConnection;

        public GetContactAutoCompleteQueryHandler(IApplicationReadDbConnection readDbConnection)
        {
            _readDbConnection = readDbConnection;
        }

        public async Task<List<ContactAutoCompleteDto>> Handle(GetContactAutoCompleteQuery request, CancellationToken cancellationToken)
        {
            return (await _readDbConnection.QueryAsync<ContactAutoCompleteDto>("EXECUTE dbo.GetContactAutoComplete @searchQuery", new { request.SearchQuery }))
                .OrderBy(autocomplete => autocomplete.Suggestion)
                .ToList();
        }
    }
}
