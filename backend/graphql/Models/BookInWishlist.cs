using System.ComponentModel.DataAnnotations.Schema;
using GraphQL.AspNet.Attributes;

namespace _.GraphQL.Models;

[GraphType]
public class BookInWishlist
{
  [ForeignKey("Wishlist")]
  public required byte[] Id_Wishlist { get; set; }

  [ForeignKey("Book")]
  public required byte[] Id_Book { get; set; }

  [GraphField]
  public Wishlist? Wishlist { get; set; }

  [GraphField]
  public Book? Book { get; set; }
}