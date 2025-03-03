using AppPrimiani.Api.Data;
using AppPrimiani.Api.Endpoints;
using AppPrimiani.Api.Handlers;
using AppPrimiani.Api.Models;
using AppPrimiani.Core.Handlers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var cnnStr = builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;


//Add autenticacao e autorizacao (sempre na ordem abaixo)
builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
    .AddIdentityCookies();

builder.Services.AddAuthorization();
//fim autenticacao e autorizacao

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(cnnStr);
});

//Autenticacao e autorizacao
builder.Services.AddIdentityCore<User>()
    .AddRoles<IdentityRole<long>>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddApiEndpoints();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    x =>
    {
        x.CustomSchemaIds(
        n => n.FullName);
    });

builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();
builder.Services.AddTransient<ITransactionHandler, TransactionHandler>();

/* AddTransient -> Cria sempre uma nova instancia do objeto
 * AddScoped -> Cria uma instancia por requisição
 * AddSingleton -> Cria uma instancia unica para toda a aplicação
 */

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => new {message = "OK"});

//Originado na classe Endpoint, trata-se do metodo de extensão
app.MapEndpoints();

app.MapGroup("v1/identity")
    .WithTags("Identity")
    .MapIdentityApi<User>();

app.Run();


