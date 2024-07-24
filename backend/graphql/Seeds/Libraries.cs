using _.GraphQL.Models;

namespace _.Graphql.Seeds;

public static class Libraries
{
  public static Library[] Get()
  {
    return
    [
      new Library { Id = Guid.NewGuid().ToByteArray(), Name = "Central Library" },
      new Library { Id = Guid.NewGuid().ToByteArray(), Name = "Community Library" },
      new Library { Id = Guid.NewGuid().ToByteArray(), Name = "University Library" }
    ];
  }
}
