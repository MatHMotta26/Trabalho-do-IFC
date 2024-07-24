// Bibliotecas Baixadas:
// GraphQL.AspNet
// GraphQL.Server.Ui.Playground
// Mysql.Data
// MySql.EntityFrameworkCore
// Microsoft.EntityFrameworkCore
// Microsoft.EntityFrameworkCore.Design

using _.Connection;
using GraphQL.AspNet.Configuration;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connectionString))
{
  throw new InvalidOperationException("A string de conexão 'DefaultConnection' não foi encontrada.");
}

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddGraphQL();
builder.Services.AddDbContext<C_BDContext>(options =>
    options.UseMySQL(connectionString));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
  var context = scope.ServiceProvider.GetRequiredService<C_BDContext>();
  context.Database.EnsureCreated();
}

app.UseGraphQL();
app.UseGraphQLPlayground(path: "/playground");

app.UseAuthorization();
app.MapControllers();

app.Run();
