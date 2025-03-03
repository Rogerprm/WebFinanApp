using AppPrimiani.Api.Data;
using AppPrimiani.Core.Common.Extensions;
using AppPrimiani.Core.Handlers;
using AppPrimiani.Core.Models;
using AppPrimiani.Core.Requests.Transactions;
using AppPrimiani.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace AppPrimiani.Api.Handlers
{
    public class TransactionHandler(AppDbContext context) : ITransactionHandler
    {
        public async Task<Response<Transaction?>> CreateTransactionAsync(CreateTransactionRequest request)
        {
            try
            {
                var transaction = new Transaction
                {
                    UserId = request.UserId,
                    CategoryId = request.CategoryId,
                    CreatedAt = DateTime.Now,
                    Amount = request.Amount,
                    PaidOrReceivedAt = request.PaidOrReceivedAt,
                    Title = request.Title,
                    Type = request.Type
                };

                await context.Transactions.AddAsync(transaction);
                await context.SaveChangesAsync();

                return new Response<Transaction?>(transaction);
            }
            catch (Exception)
            {
                return new Response<Transaction?>(null, 500, "Nao foi possivel criar a transacao");
            }
        }

        public async Task<Response<Transaction?>> DeleteTransactionAsync(DeleteTransactionRequest request)
        {
            var transaction = await context.Transactions.FirstOrDefaultAsync(x => x.Id == request.Id &&
                x.UserId == request.UserId);

            if (transaction == null)
                return new Response<Transaction?>(null, 404, "Transacao nao encontrada");

            context.Transactions.Remove(transaction);
            await context.SaveChangesAsync();

            return new Response<Transaction?>(transaction);
        }

        public async Task<Response<Transaction?>> UpdateTransactionAsync(UpdateTransactionRequest request)
        {
            var transaction = await context.Transactions.FirstOrDefaultAsync(x => x.Id == request.Id &&
                x.UserId == request.UserId);

            if (transaction == null)
                return new Response<Transaction?>(null, 404, "Transacao nao encontrada");

            transaction.CategoryId = request.CategoryId;
            transaction.Amount = request.Amount;
            transaction.PaidOrReceivedAt = request.PaidOrReceivedAt;
            transaction.Title = request.Title;
            transaction.Type = request.Type;

            context.Transactions.Update(transaction);
            await context.SaveChangesAsync();

            return new Response<Transaction?>(transaction);

        }

        public async Task<Response<Transaction?>> GetTransactionByIdAsync(GetTransactionByIdRequest request)
        {
            try
            {
                var transaction = await context.Transactions
                    .AsNoTracking()
                    //.Include(x => x.Category)
                    .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                return transaction is null
                    ? new Response<Transaction?>(null, 404, "Nao encontrado")
                    : new Response<Transaction?>(transaction);
            }
            catch (Exception)
            {
                return new Response<Transaction?>(null, 500, "ERRO.");
            }
        }

        public async Task<PagedResponse<List<Transaction?>>> GetTransactionsByPeriodAsync(GetTransactionsByPeriodRequest request)
        {

            try
            {
                request.StartDate ??= DateTime.Now.GetFirstDay();
                request.EndDate ??= DateTime.Now.GetLastDay();
                //??= é um operador de coalescência nula que atribui
                //o valor do lado direito à variável
                //do lado esquerdo se a variável do lado esquerdo for nula.

                var query = context.Transactions
                    .AsNoTracking()
                    .Where(x => x.CreatedAt >= request.StartDate
                        && x.CreatedAt <= request.EndDate
                        && x.UserId == request.UserId)
                    .OrderBy(x => x.CreatedAt);

                var transactions = await query
                    .Skip(request.PageSize * (request.PageNumber - 1))
                    .Take(request.PageSize)
                    .ToListAsync();

                var count = await query.CountAsync();

                return new PagedResponse<List<Transaction?>>(
                    transactions, count, request.PageNumber, request.PageSize);
            }
            catch (Exception)
            {
                return new PagedResponse<List<Transaction?>>(null, 500, "Nao foi possivel consultar a lista de transacoes com essas datas");
            }




        }

    }
}
