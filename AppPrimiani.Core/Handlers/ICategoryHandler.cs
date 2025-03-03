using AppPrimiani.Core.Models;
using AppPrimiani.Core.Requests.Categories;
using AppPrimiani.Core.Responses;

namespace AppPrimiani.Core.Handlers
{
    public interface ICategoryHandler
    {
        Task<Response<Category?>> GetCategoryByIdAsync(GetCategoryByIdRequest request);
        Task<PagedResponse<List<Category?>>> GetAllCategoriesAsync(GetAllCategoriesRequest request);
        Task<Response<Category?>> CreateCategoryAsync(CreateCategoryRequest request);
        Task<Response<Category?>> UpdateCategoryAsync(UpdateCategoryRequest request);
        Task<Response<Category?>> DeleteCategoryAsync(DeleteCategoryRequest request);
    }
}
