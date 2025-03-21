using System.ComponentModel.DataAnnotations;

namespace AppPrimiani.Core.Requests.Account
{
    public class LoginRequest : Request
    {
        [Required(ErrorMessage = "Email obrigatorio")]
        [EmailAddress(ErrorMessage = "Email invalido")]
        public string Email { get; set; } = string.Empty;


        [Required(ErrorMessage = "Email obrigatorio")]
        public string Password { get; set; } = string.Empty;
    }
}
