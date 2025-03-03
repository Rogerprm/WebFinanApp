using AppPrimiani.Api.Common.Api;
using System.Security.Claims;

namespace AppPrimiani.Api.Endpoints.Identity
{
    public class GetRolesEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/roles", Handle)
            .RequireAuthorization();
        private static Task<IResult> Handle(ClaimsPrincipal user)
        {
            if (user.Identity?.IsAuthenticated != true)
                return Task.FromResult(Results.Unauthorized());

                     var identity = (ClaimsIdentity)user.Identity;
                     var roles = identity.FindAll(identity.RoleClaimType)
                     .Select(x => new
                     {
                         x.Issuer,
                         x.OriginalIssuer,
                         x.Type,
                         x.Value,
                         x.ValueType
                     });

            return Task.FromResult<IResult>(TypedResults.Json(roles));
        }
    
    }
}
