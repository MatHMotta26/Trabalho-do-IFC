using System.ComponentModel;
using _.Connection;
using _.GraphQL.Models;
using GraphQL.AspNet.Attributes;
using GraphQL.AspNet.Controllers;
using Microsoft.EntityFrameworkCore;

namespace _.GraphQL.Controllers;

public class BookController(C_BDContext context) : GraphController
{
  [QueryRoot("books")]
  [Description("Pega todos os livros")]
  public IEnumerable<Book> GetBooks()
  {
    return [.. context.Books.Include(b => b.Library)];
  }

  [QueryRoot("bookById")]
  [Description("Pega um livro pelo Id")]
  public Book? GetBookById(byte[] id)
  {
    return context.Books.Include(b => b.Library).SingleOrDefault(b => b.Id == id);
  }

  [MutationRoot("createBook")]
  [Description("Cria um livro")]
  public Book CreateBook(Book book)
  {
    context.Books.Add(book);
    context.SaveChanges();
    return book;
  }

  [MutationRoot("updateBook")]
  [Description("Atualiza um livro")]
  public Book UpdateBook(Book book)
  {
    context.Books.Update(book);
    context.SaveChanges();
    return book;
  }

  [MutationRoot("deleteBook")]
  [Description("Deleta um livro")]
  public Book DeleteBook(Book book)
  {
    context.Books.Remove(book);
    context.SaveChanges();
    return book;
  }
}