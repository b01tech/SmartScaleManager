using Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAppEndpoints();
app.Run();
