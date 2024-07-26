using Microsoft.EntityFrameworkCore;
using GraphQL.AspNet.Configuration;

namespace _.Connection;

public class Startup
{
  public void ConfigureServices(IServiceCollection services)
  {
    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddGraphQL();
    services.AddDbContext<C_BDContext>(options =>
        options.UseMySQL(Connection.Conectar()));
  }

  public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
  {
    if (env.IsDevelopment())
    {
      app.UseDeveloperExceptionPage();
      app.UseGraphQLPlayground(path: "/playground");
    }

    app.UseGraphQL();
    app.UseRouting();
    app.UseAuthorization();

    app.ApplicationServices
          .GetRequiredService<IServiceScopeFactory>()
          .CreateScope()
          .ServiceProvider
          .GetRequiredService<C_BDContext>()
          .Database
          .EnsureCreated();

    app.UseEndpoints(endpoints =>
    {
      endpoints.MapControllers();
    });
  }
}