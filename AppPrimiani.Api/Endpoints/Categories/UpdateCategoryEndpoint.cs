using AppPrimiani.Api.Common.Api;
using AppPrimiani.Core.Handlers;
using AppPrimiani.Core.Models;
using AppPrimiani.Core.Requests.Categories;
using AppPrimiani.Core.Responses;

namespace AppPrimiani.Api.Endpoints.Categories
{
    public class UpdateCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPut("/{id}", HandleAsync)
            .WithName("Categories: Updte")
            .WithSummary("Atualiza uma categoria")
            .WithDescription("")
            .WithOrder(2)
            .Produces<Response<Category?>>();


        private static async Task<IResult> HandleAsync(ICategoryHandler handler,
            UpdateCategoryRequest request, long id)
        {
            request.UserId = "Teste@teste";
            request.Id = id;
            var result = await handler.UpdateCategoryAsync(request);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
