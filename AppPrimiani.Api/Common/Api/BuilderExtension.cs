using AppPrimiani.Api.Data;
using AppPrimiani.Api.Handlers;
using AppPrimiani.Api.Models;
using AppPrimiani.Core;
using AppPrimiani.Core.Handlers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AppPrimiani.Api.Common.Api
{
    public static class BuilderExtension
    {
        public static void AddConfiguration(this WebApplicationBuilder builder)
        {
            Configuration.ConnectionString =
                builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;

            Configuration.BackendUrl = builder.Configuration.GetValue<string>("BackendUrl") ?? string.Empty;
            Configuration.BackendUrl = builder.Configuration.GetValue<string>("FrontendUrl") ?? string.Empty;
        }

        public static void AddDocumentation(this WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(
                x =>
                {
                    x.CustomSchemaIds(
                    n => n.FullName);
                });
        }

        public static void AddSecurity(this WebApplicationBuilder builder)
        {
            //Add autenticacao e autorizacao (sempre na ordem abaixo)
            builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
                .AddIdentityCookies();

            builder.Services.AddAuthorization();
            //fim autenticacao e autorizacao
        }

        public static void AddDataContexts(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration.ConnectionString);
            });

            //Autenticacao e autorizacao
            builder.Services.AddIdentityCore<User>()
                .AddRoles<IdentityRole<long>>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddApiEndpoints();
        }

        public static void AddServices(this WebApplicationBuilder builder)
        {
            /* AddTransient -> Cria sempre uma nova instancia do objeto
            * AddScoped -> Cria uma instancia por requisição
             * AddSingleton -> Cria uma instancia unica para toda a aplicação
            */
            builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();
            builder.Services.AddTransient<ITransactionHandler, TransactionHandler>();
        }

        public static void AddCrossOrigin(this WebApplicationBuilder builder)
        {
            builder.Services.AddCors
                (
                x => x.AddPolicy
                (ApiConfiguration.CorsPolicyName,
                policy => policy.WithOrigins([
                    Configuration.BackendUrl,
                    Configuration.FrontendUrl ])
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                ));
        }

    }
}
