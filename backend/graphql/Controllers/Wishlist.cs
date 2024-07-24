using System.ComponentModel;
using _.Connection;
using _.GraphQL.Models;
using GraphQL.AspNet.Attributes;
using GraphQL.AspNet.Controllers;
using Microsoft.EntityFrameworkCore;

namespace _.GraphQL.Controllers;

public class WishlistController(C_BDContext context) : GraphController
{
  [QueryRoot("wishlists")]
  [Description("Pega todas as wishlists")]
  public IEnumerable<Wishlist> GetWishlists()
  {
    return [.. context.Wishlists.Include(w => w.User).Include(w => w.BooksInWishlist)];
  }

  [QueryRoot("wishlistById")]
  [Description("Pega uma wishlist pelo Id")]
  public Wishlist? GetWishlistById(byte[] id)
  {
    return context.Wishlists
      .Include(w => w.User)
      .Include(w => w.BooksInWishlist)
      .SingleOrDefault(w => w.Id == id);
  }

  [QueryRoot("wishlistByUser")]
  [Description("Pega a wishlist de um usuario")]
  public Wishlist? GetWishlistByUser(byte[] userId)
  {
    return context.Wishlists.Include(w => w.User).Include(w => w.BooksInWishlist).SingleOrDefault(w => w.UserId == userId);
  }

  [QueryRoot("wishlistsByBook")]
  [Description("Pega as wishlists de um livro")]
  public IEnumerable<Wishlist> GetWishlistsByBook(byte[] bookId)
  {
    return [.. context.BooksInWishlists
      .Include(b => b.Book)
      .Include(b => b.Wishlist)
      .Where(b => b.Id_Book == bookId)
      .Select(b => b.Wishlist)
      .Distinct()];
  }

  [MutationRoot("createWishlist")]
  [Description("Cria uma wishlist")]
  public Wishlist CreateWishlist(Wishlist wishlist)
  {
    context.Wishlists.Add(wishlist);
    context.SaveChanges();
    return wishlist;
  }

  [MutationRoot("updateWishlist")]
  [Description("Atualiza uma wishlist")]
  public Wishlist UpdateWishlist(Wishlist wishlist)
  {
    context.Wishlists.Update(wishlist);
    context.SaveChanges();
    return wishlist;
  }

  [MutationRoot("deleteWishlist")]
  [Description("Deleta uma wishlist")]
  public Wishlist DeleteWishlist(Wishlist wishlist)
  {
    context.Wishlists.Remove(wishlist);
    context.SaveChanges();
    return wishlist;
  }
}