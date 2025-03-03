using AppPrimiani.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace AppPrimiani.Core.Requests.Transactions
{
    public class CreateTransactionRequest : Request
    {
        [Required(ErrorMessage = "Titulo Invalido")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tipo Invalido")]
        public ETransactionType Type { get; set; }

        [Required(ErrorMessage = "Valor Invalido")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Categoria Invalido")]
        public long CategoryId { get; set; }

        [Required(ErrorMessage = "Data Invalido")]
        public DateTime? PaidOrReceivedAt { get; set; }
    }
}
