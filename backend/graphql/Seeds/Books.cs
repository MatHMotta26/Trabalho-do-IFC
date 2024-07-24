using System.Reflection.Metadata;
using _.GraphQL.Models;

namespace _.Graphql.Seeds;

public static class Books
{
  public static Book[] Get(Library[] libraries)
  {
    return
    [
      new Book
      {
        Id = Guid.NewGuid().ToByteArray(),
        Title = "The Hobbit",
        Author = "J.R.R. Tolkien",
        Editor = "George Allen & Unwin",
        Image = [],
        Description = "A book about adventure, adventure, adventure.",
        Categories = ["Fantasy", "Adventure"],
        Pages = 310,
        Rate = 4,
        Year = 1937,
        File = [],
        Isbn = "978-05637-42",
        LibraryId = libraries[0].Id,
      },
      new Book
      {
        Id = Guid.NewGuid().ToByteArray(),
        Title = "The Lord of the Rings",
        Author = "J.R.R. Tolkien",
        Editor = "George Allen & Unwin",
        Image = [],
        Description = "A book about adventure, adventure, adventure.",
        Categories = ["Fantasy", "Adventure"],
        Pages = 310,
        Rate = 4,
        Year = 1937,
        File = [],
        Isbn = "978-12342-42",
        LibraryId = libraries[1].Id,
      },
      new Book
      {
        Id = Guid.NewGuid().ToByteArray(),
        Title = "The Fellowship of the Ring",
        Author = "J.R.R. Tolkien",
        Editor = "George Allen & Unwin",
        Image = [],
        Description = "A book about adventure, adventure, adventure.",
        Categories = ["Fantasy", "Adventure"],
        Pages = 310,
        Rate = 4,
        Year = 1937,
        File = [],
        Isbn = "978-05537-42",
        LibraryId = libraries[2].Id,
      }
    ];
  }
}