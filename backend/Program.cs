// Bibliotecas Baixadas:
// GraphQL.AspNet
// GraphQL.Server.Ui.Playground
// Mysql.Data
// MySql.EntityFrameworkCore
// Microsoft.EntityFrameworkCore
// Microsoft.EntityFrameworkCore.Design

using _.Connection;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup();
startup.ConfigureServices(builder.Services);

var app = builder.Build();
var env = app.Environment;

startup.Configure(app, env);

app.Run();