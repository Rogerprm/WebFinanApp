using AppPrimiani.Api;
using AppPrimiani.Api.Common.Api;
using AppPrimiani.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.AddConfiguration();
builder.AddSecurity();
builder.AddDataContexts();
builder.AddCrossOrigin();
builder.AddDocumentation();
builder.AddServices();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.ConfigureDevEnviroment();
}

app.UseCors(ApiConfiguration.CorsPolicyName);
app.UseSecurity();

//Originado na classe Endpoint, trata-se do metodo de extensão
app.MapEndpoints();

app.Run();


