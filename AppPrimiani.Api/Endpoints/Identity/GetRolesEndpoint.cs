using AppPrimiani.Api.Common.Api;
using AppPrimiani.Core.Models.Account;
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
                     .Select(x => new RoleClaim
                     {
                         Issuer = x.Issuer,
                         OriginalIssuer = x.OriginalIssuer,
                         Type = x.Type,
                         Value = x.Value,
                         ValueType = x.ValueType
                     });

            return Task.FromResult<IResult>(TypedResults.Json(roles));
        }
    
    }
}
