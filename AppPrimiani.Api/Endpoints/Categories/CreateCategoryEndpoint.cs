using AppPrimiani.Api.Common.Api;
using AppPrimiani.Core.Handlers;
using AppPrimiani.Core.Models;
using AppPrimiani.Core.Requests.Categories;
using AppPrimiani.Core.Responses;
using System.Security.Claims;

namespace AppPrimiani.Api.Endpoints.Categories
{
    public class CreateCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPost("/", HandleAsync)
            .WithName("Categories: Create")
            .WithSummary("Cria nova categoria")
            .WithDescription("")
            .WithOrder(1)
            .Produces<Response<Category?>>();

        private static async Task<IResult> HandleAsync(
            ClaimsPrincipal user,
            ICategoryHandler handler,
            CreateCategoryRequest request)
        {
            request.UserId = user.Identity?.Name ?? string.Empty;
            var result = await handler.CreateCategoryAsync(request);

            return result.IsSuccess
                ? TypedResults.Created($"/{result.Data?.Id}", result)
                : TypedResults.BadRequest(result);

            //if (result.IsSuccess)
            //{
            //    return TypedResults.Created($"/{result.Data?.Id}", result.Data);
            //}
            //return TypedResults.BadRequest(result.Data);
            //}
        }
    }
}
