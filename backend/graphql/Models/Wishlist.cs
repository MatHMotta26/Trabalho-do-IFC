using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GraphQL.AspNet.Attributes;

namespace _.GraphQL.Models;

[GraphType]
public class Wishlist
{
  [Key, Column(TypeName = "BINARY(16)")]
  public required byte[] Id { get; set; }

  [ForeignKey("User")]
  public required byte[] UserId { get; set; }

  [GraphField]
  public required User User { get; set; }

  [GraphField]
  public ICollection<BookInWishlist> BooksInWishlist { get; set; } = [];
}