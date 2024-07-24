using _.Graphql.Seeds;
using _.GraphQL.Models;
using Microsoft.EntityFrameworkCore;

namespace _.Connection;

public static class ModelBuilderExtensions
{
  public static void Seed(this ModelBuilder modelBuilder)
  {
    var libraries = Libraries.Get();
    modelBuilder.Entity<Library>().HasData(libraries);

    var books = Books.Get(libraries);
    modelBuilder.Entity<Book>().HasData(books);
  }
}
