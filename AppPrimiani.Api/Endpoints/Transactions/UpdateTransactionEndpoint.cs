﻿using AppPrimiani.Api.Common.Api;
using AppPrimiani.Core.Handlers;
using AppPrimiani.Core.Models;
using AppPrimiani.Core.Requests.Categories;
using AppPrimiani.Core.Requests.Transactions;
using AppPrimiani.Core.Responses;
using System.Security.Claims;

namespace AppPrimiani.Api.Endpoints.Transactions
{
    public class UpdateTransactionEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPut("/{id}", HandleAsync)
            .WithName("Transaction: Updte")
            .WithSummary("Atualiza uma Transacao")
            .WithDescription("")
            .WithOrder(2)
            .Produces<Response<Transaction?>>();


        private static async Task<IResult> HandleAsync(
            ClaimsPrincipal user,
            ITransactionHandler handler,
            UpdateTransactionRequest request, long id)
        {
            request.UserId = user.Identity?.Name ?? string.Empty;
            request.Id = id;
            var result = await handler.UpdateTransactionAsync(request);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
