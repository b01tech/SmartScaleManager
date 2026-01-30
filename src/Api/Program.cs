using Api.Extensions;
using Application.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddDocumentationApi()
    .AddApplication();

var app = builder.Build();

app.UseDocumentationApi();
app.UseHttpsRedirection();
app.UseAppEndpoints();
app.Run();
