using AppPrimiani.Api.Data;
using AppPrimiani.Core.Handlers;
using AppPrimiani.Core.Models;
using AppPrimiani.Core.Requests.Categories;
using AppPrimiani.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace AppPrimiani.Api.Handlers
{
    public class CategoryHandler(AppDbContext context) : ICategoryHandler
    {
        public async Task<Response<Category?>> CreateCategoryAsync(CreateCategoryRequest request)
        {
            try
            {
                var category = new Category()
                {
                    UserId = request.UserId,
                    Title = request.Title,
                    Description = request.Description
                };
                await context.Categories.AddAsync(category);
                await context.SaveChangesAsync();

                return new Response<Category?>(category, 201, "Criado com sucesso");
            }
            catch 
            {
                //Serilog olhar depois
                return new Response<Category?>(null, 500, "Nao foi possivel criar");
            }
        }

        public async Task<Response<Category?>> DeleteCategoryAsync(DeleteCategoryRequest request)
        {
            try
            {
                var category = await context.Categories
            .FirstOrDefaultAsync(x => x.Id == request.Id &&
                                    x.UserId == request.UserId);

                if (category is null)
                    return new Response<Category?>(null, 404, "Nao encontrada");

                context.Categories.Remove(category);
                await context.SaveChangesAsync();

                return new Response<Category?>(category, 200, "Excluido com sucesso");
            }
            catch
            {
                return new Response<Category?>(null, 500, "Nao foi deletar atualizar");
            }
        }

        public async Task<PagedResponse<List<Category?>>> GetAllCategoriesAsync(GetAllCategoriesRequest request)
        {
            try
            {
                var query = context.Categories
                .AsNoTracking()
                .Where(x => x.UserId == request.UserId)
                .OrderBy(x=>x.Title);

                var categories = await query
                    .Skip(request.PageSize * (request.PageNumber -1) )
                    .Take(request.PageSize)
                    .ToListAsync();

                var count = await query
                    .CountAsync();

                return new PagedResponse<List<Category?>>
                    (categories, count, request.PageNumber, request.PageSize);
            }
            catch 
            {
                return new PagedResponse<List<Category?>>(null, 500, "Nao foi possivel consultar a lista de categorias");
            }
        }

        public async Task<Response<Category?>> GetCategoryByIdAsync(GetCategoryByIdRequest request)
        {
            try
            {
                var category = await context.Categories
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                return category is null
                    ? new Response<Category?>(null, 404, "Nao encontrado")
                    : new Response<Category?>(category);

            }
            catch (Exception)
            {
                return new Response<Category?>(null, 500, "ERRO.");
            }
        }

        public async Task<Response<Category?>> UpdateCategoryAsync(UpdateCategoryRequest request)
        {
            try
            {
                var category = await context.Categories
            .FirstOrDefaultAsync(x => x.Id == request.Id &&
                                    x.UserId == request.UserId);

                if (category is null)
                    return new Response<Category?>(null, 404, "Nao encontrada");

                category.Title = request.Title;
                category.Description = request.Description;

                context.Categories.Update(category);
                await context.SaveChangesAsync();

                return new Response<Category?>(category);
            }
            catch
            {
                return new Response<Category?>(null, 500, "Nao foi possivel atualizar");
            }

        }
    }
}
