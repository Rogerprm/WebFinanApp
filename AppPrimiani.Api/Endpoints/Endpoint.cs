using AppPrimiani.Api.Common.Api;
using AppPrimiani.Api.Endpoints.Categories;
using AppPrimiani.Api.Endpoints.Transactions;

namespace AppPrimiani.Api.Endpoints
{
    public static class Endpoint
    {
        //Usando o this vira um método de extensão do WebApplication
        //para usar no program o app.MapEndpoints();
        public static void MapEndpoints(this WebApplication app)
        {
            var endpoints = app.MapGroup("");

            //Aqui é onde você vai adicionar os endpoints
            endpoints.MapGroup("v1/categories")
                .WithTags("Categories")
                .RequireAuthorization ()
                .MapEndpoint<CreateCategoryEndpoint>()
                .MapEndpoint<UpdateCategoryEndpoint>()
                .MapEndpoint<DeleteCategoryEndpoint>()
                .MapEndpoint<GetCategoryByIdEndpoint>()
                .MapEndpoint<GetAllCategoriesEndpoint>();

            endpoints.MapGroup("v1/transactions")
                .WithTags("Transactions")
                .RequireAuthorization ()
                .MapEndpoint<CreateTransactionEndpoint>()
                .MapEndpoint<UpdateTransactionEndpoint>()
                .MapEndpoint<DeleteTransactionEndpoint>()
                .MapEndpoint<GetTransactionByIdEndpoint>()
                .MapEndpoint<GetTransactionsByPeriod>();
        }

        private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
            where TEndpoint : IEndpoint
        {
            TEndpoint.Map(app);
            return app;
        }
    }
}
