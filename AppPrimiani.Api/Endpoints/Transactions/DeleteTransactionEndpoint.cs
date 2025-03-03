using AppPrimiani.Api.Common.Api;
using AppPrimiani.Core.Handlers;
using AppPrimiani.Core.Models;
using AppPrimiani.Core.Requests.Categories;
using AppPrimiani.Core.Responses;
using System.Security.Claims;

namespace AppPrimiani.Api.Endpoints.Transactions
{
    public class DeleteTransactionEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
                    => app.MapDelete("/{id}", HandleAsync)
                    .WithName("Transaction: Delete")
                    .WithSummary("Deletaa uma categoria")
                    .WithDescription("")
                    .WithOrder(3)
                    .Produces<Response<Transaction?>>();

        /*
         *  "email": "adm@adm",
            "password": "Abc!123*" 
         */


        private static async Task<IResult> HandleAsync(
            ClaimsPrincipal user,
            ICategoryHandler handler,
            long id)
        {
            var request = new DeleteCategoryRequest
            {
                UserId = user.Identity?.Name ?? string.Empty,
                Id = id
            };

            var result = await handler.DeleteCategoryAsync(request);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}