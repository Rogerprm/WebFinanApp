using AppPrimiani.Api.Common.Api;
using AppPrimiani.Core.Handlers;
using AppPrimiani.Core.Models;
using AppPrimiani.Core.Requests.Transactions;
using AppPrimiani.Core.Responses;
using System.Security.Claims;

namespace AppPrimiani.Api.Endpoints.Transactions
{
    public class CreateTransactionEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
                    => app.MapPost("/", HandleAsync)
                    .WithName("Transaction: Create")
                    .WithSummary("Cria nova Transacao")
                    .WithDescription("")
                    .WithOrder(1)
                    .Produces<Response<Transaction?>>();

        private static async Task<IResult> HandleAsync(
            ClaimsPrincipal user,
            ITransactionHandler handler,
            CreateTransactionRequest request)
        {
            request.UserId = user.Identity?.Name ?? string.Empty;
            var result = await handler.CreateTransactionAsync(request);

            return result.IsSuccess
                ? TypedResults.Created($"/{result.Data?.Id}", result)
                : TypedResults.BadRequest(result);


        }
    }
}
