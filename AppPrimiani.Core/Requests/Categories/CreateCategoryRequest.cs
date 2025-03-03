using System.ComponentModel.DataAnnotations;

namespace AppPrimiani.Core.Requests.Categories
{
    public class CreateCategoryRequest : Request
    {
        [Required(ErrorMessage = "Titulo Invalido")]
        [MaxLength(80, ErrorMessage = "Titulo deve conter ate 80 caracteres")]
        public string Title { get; set; } = string.Empty;


        [Required(ErrorMessage = "Descricao Invalido")]
        public string Description { get; set; } = string.Empty;
    }
}
