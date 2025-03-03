using AppPrimiani.Api.Common.Api;
using AppPrimiani.Api.Models;
using AppPrimiani.Core.Models;
using Microsoft.AspNetCore.Identity;

namespace AppPrimiani.Api.Endpoints.Identity
{
    public class LogoutEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPost("/logout", HandleAsync)
            .RequireAuthorization();

        private static async Task<IResult> HandleAsync(SignInManager<User> signInManager)
        {
            await signInManager.SignOutAsync();
            return Results.Ok();
        }

    }
}
