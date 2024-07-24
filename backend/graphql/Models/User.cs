using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GraphQL.AspNet.Attributes;

namespace _.GraphQL.Models;

[GraphType]
public class User
{
  [Key, Column(TypeName = "BINARY(16)")]
  public required byte[] Id { get; set; }

  [GraphField]
  public string? Description { get; set; }

  [GraphField]
  public string[]? CategoriesPreferences { get; set; }

  [GraphField]
  public required string Telephone { get; set; }

  [GraphField]
  public required DateOnly Birthday { get; set; }

  [GraphField]
  public Wishlist? Wishlist { get; set; }

  [GraphField]
  public required Account Account { get; set; }

  [GraphField]
  public ICollection<MyBook> MyBooks { get; set; } = [];
}