using AppPrimiani.Api.Common.Api;
using AppPrimiani.Core;
using AppPrimiani.Core.Handlers;
using AppPrimiani.Core.Models;
using AppPrimiani.Core.Requests.Transactions;
using AppPrimiani.Core.Responses;
using Microsoft.AspNetCore.Mvc;


namespace AppPrimiani.Api.Endpoints.Transactions
{
    public class GetTransactionsByPeriod : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
                  => app.MapGet("/", HandleAsync)
                   .WithName("Transaction: Get All")
                   .WithSummary("Recupera todas Transacoes")
                   .WithDescription("")
                   .WithOrder(5)
                   .Produces<PagedResponse<List<Transaction?>>>();


        private static async Task<IResult> HandleAsync(ITransactionHandler handler,
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null,
            [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
            [FromQuery] int pageSize = Configuration.DefaultPageSize
            )
        {
            var request = new GetTransactionsByPeriodRequest
            {
                UserId = "Teste@teste",
                PageNumber = pageNumber,
                PageSize = pageSize,
                StartDate = null,
                EndDate = null,
            };

            var result = await handler.GetTransactionsByPeriodAsync(request);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);

        }
    }
}

