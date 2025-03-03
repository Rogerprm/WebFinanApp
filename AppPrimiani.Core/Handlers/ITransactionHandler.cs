using AppPrimiani.Core.Models;
using AppPrimiani.Core.Requests.Transactions;
using AppPrimiani.Core.Responses;


namespace AppPrimiani.Core.Handlers
{
    public interface ITransactionHandler
    {
        Task<Response<Transaction?>> CreateTransactionAsync (CreateTransactionRequest request);
        Task<Response<Transaction?>> UpdateTransactionAsync(UpdateTransactionRequest request);
        Task<Response<Transaction?>> DeleteTransactionAsync(DeleteTransactionRequest request);
        Task<Response<Transaction?>> GetTransactionByIdAsync(GetTransactionByIdRequest request);
        Task<PagedResponse<List<Transaction?>>> GetTransactionsByPeriodAsync(GetTransactionsByPeriodRequest request);
    }
}
