using Api.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDocumentationApi();

var app = builder.Build();

app.UseDocumentationApi();
app.UseHttpsRedirection();
app.UseAppEndpoints();
app.Run();
