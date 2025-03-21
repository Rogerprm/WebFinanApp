namespace AppPrimiani.Core.Models.Account
{
    public class User
    {
        //v1/identity/manage/info

        public string Email { get; set; } = string.Empty;
        public Dictionary<string, string> Claims { get; set; } = [];
    }
}
