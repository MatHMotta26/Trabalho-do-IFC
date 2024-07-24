using System.ComponentModel;
using _.Connection;
using _.GraphQL.Models;
using GraphQL.AspNet.Attributes;
using GraphQL.AspNet.Controllers;
using Microsoft.EntityFrameworkCore;

namespace _.GraphQL.Controllers;

public class BookInWishlistController(C_BDContext context) : GraphController
{
  [QueryRoot("bookInWishlists")]
  [Description("Pega todos os livros em wishlist")]
  public IEnumerable<BookInWishlist> GetBookInWishlists()
  {
    return [.. context.BooksInWishlists.Include(b => b.Book).Include(b => b.Wishlist)];
  }

  [QueryRoot("bookInWishlistsByBook")]
  [Description("Pegar as listas de desejo relacionadas a um livro")]
  public IEnumerable<Wishlist> GetWishlistsByBook(byte[] bookId)
  {
    return [.. context.BooksInWishlists
      .Include(b => b.Book)
      .Include(b => b.Wishlist)
      .Where(b => b.Id_Book == bookId)
      .Select(b => b.Wishlist)
      .Distinct()];
  }

  [QueryRoot("bookInWishlistsByWishlist")]
  [Description("Pegar os livros relacionados a uma lista de desejo")]
  public IEnumerable<Book> GetBooksByWishlist(byte[] wishlistId)
  {
    return [.. context.BooksInWishlists
      .Include(b => b.Book)
      .Include(b => b.Wishlist)
      .Where(b => b.Id_Wishlist == wishlistId)
      .Select(b => b.Book)
      .Distinct()];
  }

  [MutationRoot("createBookInWishlist")]
  [Description("Cria um livro em wishlist")]
  public BookInWishlist CreateBookInWishlist(BookInWishlist bookInWishlist)
  {
    context.BooksInWishlists.Add(bookInWishlist);
    context.SaveChanges();
    return bookInWishlist;
  }

  [MutationRoot("updateBookInWishlist")]
  [Description("Atualiza um livro em wishlist")]
  public BookInWishlist UpdateBookInWishlist(BookInWishlist bookInWishlist)
  {
    context.BooksInWishlists.Update(bookInWishlist);
    context.SaveChanges();
    return bookInWishlist;
  }

  [MutationRoot("deleteBookInWishlist")]
  [Description("Deleta um livro em wishlist")]
  public BookInWishlist DeleteBookInWishlist(BookInWishlist bookInWishlist)
  {
    context.BooksInWishlists.Remove(bookInWishlist);
    context.SaveChanges();
    return bookInWishlist;
  }
}