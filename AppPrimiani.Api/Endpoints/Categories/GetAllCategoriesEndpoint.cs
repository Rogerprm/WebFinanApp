using AppPrimiani.Api.Common.Api;
using AppPrimiani.Core;
using AppPrimiani.Core.Handlers;
using AppPrimiani.Core.Models;
using AppPrimiani.Core.Requests.Categories;
using AppPrimiani.Core.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AppPrimiani.Api.Endpoints.Categories
{
    public class GetAllCategoriesEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
                   => app.MapGet("/", HandleAsync)
                   .WithName("Categories: Get All")
                   .WithSummary("Recupera todas categorias")
                   .WithDescription("")
                   .WithOrder(5)
                   .Produces<PagedResponse<List<Category?>>>();


        private static async Task<IResult> HandleAsync(
            ClaimsPrincipal user,
            ICategoryHandler handler,
            [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
            [FromQuery] int pageSize = Configuration.DefaultPageSize
            )
        {
            var request = new GetAllCategoriesRequest
            {
                UserId = user.Identity?.Name ?? string.Empty,
                PageNumber = pageNumber,
                PageSize = pageSize,
            };

            var result = await handler.GetAllCategoriesAsync(request);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);

        }
    }
}
