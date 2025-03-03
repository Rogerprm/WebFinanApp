using AppPrimiani.Api.Common.Api;
using AppPrimiani.Core.Handlers;
using AppPrimiani.Core.Models;
using AppPrimiani.Core.Requests.Categories;
using AppPrimiani.Core.Requests.Transactions;
using AppPrimiani.Core.Responses;

namespace AppPrimiani.Api.Endpoints.Transactions
{
    public class GetTransactionByIdEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
                    => app.MapGet("/{id}", HandleAsync)
                    .WithName("Transaction: Get By Id")
                    .WithSummary("Recupera uma Transacao")
                    .WithDescription("")
                    .WithOrder(4)
                    .Produces<Response<Transaction?>>();


        private static async Task<IResult> HandleAsync(ITransactionHandler handler,
            long id)
        {
            var request = new GetTransactionByIdRequest
            {
                UserId = "Teste@teste",
                Id = id
            };

            var result = await handler.GetTransactionByIdAsync(request);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
