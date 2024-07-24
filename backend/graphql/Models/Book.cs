using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using GraphQL.AspNet.Attributes;

namespace _.GraphQL.Models;

[GraphType]
public class Book
{
  [Key, Column(TypeName = "BINARY(16)")]
  public required byte[] Id { get; set; }

  [GraphField]
  public required string Title { get; set; }

  [GraphField]
  public required string Author { get; set; }

  [GraphField]
  public required string Editor { get; set; }

  [GraphField]
  public required byte[] Image { get; set; }

  [GraphField]
  public required string Description { get; set; }

  [GraphField]
  public required string[] Categories { get; set; }

  [GraphField]
  public short Pages { get; set; }

  [GraphField]
  public short Rate { get; set; }

  [GraphField]
  public int Year { get; set; }

  [GraphField]
  public required string Isbn { get; set; }

  [GraphField]
  public bool Available { get; set; }

  [GraphField]
  public required byte[] File { get; set; }

  [ForeignKey("Library")]
  public required byte[] LibraryId { get; set; }

  [GraphField]
  public Library? Library { get; set; }

  [GraphField]
  public ICollection<MyBook> MyBooks { get; set; } = [];

  [GraphField]
  public ICollection<BookInWishlist> BooksInWishlist { get; set; } = [];
}