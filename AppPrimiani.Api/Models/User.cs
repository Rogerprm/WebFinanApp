using Microsoft.AspNetCore.Identity;

namespace AppPrimiani.Api.Models
{
    public class User : IdentityUser<long>
    {
        public List<IdentityRole<long>>? Roles { get; set; }
    }
}
