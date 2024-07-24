using System.ComponentModel;
using _.Connection;
using _.GraphQL.Models;
using GraphQL.AspNet.Attributes;
using GraphQL.AspNet.Controllers;
using Microsoft.EntityFrameworkCore;

namespace _.GraphQL.Controllers;

public class MyBookController(C_BDContext context) : GraphController
{
  [QueryRoot("myBooks")]
  [Description("Pega todos os 'meus livros'")]
  public IEnumerable<MyBook> GetMyBooks()
  {
    return [.. context.MyBooks.Include(b => b.Book).Include(b => b.User)];
  }

  [QueryRoot("myBookByUser")]
  [Description("Pega todos os 'meus livro' do usuário pelo Id dele")]
  public IEnumerable<MyBook> GetMyBookByUser(byte[] userId)
  {
    return [.. context.MyBooks.Include(b => b.Book).Include(b => b.User).Where(b => b.UserId == userId)];
  }

  [QueryRoot("myBooksByBook")]
  [Description("Pega todos os 'meus livros' do usuário pelo Id do livro")]
  public IEnumerable<MyBook> GetMyBooksByBook(byte[] bookId)
  {
    return [.. context.MyBooks.Include(b => b.Book).Include(b => b.User).Where(b => b.BookId == bookId)];
  }

  [QueryRoot("myBookById")]
  [Description("Pega um 'meu livro' pelo Id")]
  public MyBook? GetMyBookById(byte[] id)
  {
    return context.MyBooks.Include(b => b.Book).Include(b => b.User).SingleOrDefault(b => b.Id == id);
  }

  [MutationRoot("createMyBook")]
  [Description("Adiciona um livro ao 'meus livros'")]
  public MyBook CreateMyBook(MyBook myBook)
  {
    context.MyBooks.Add(myBook);
    context.SaveChanges();
    return myBook;
  }

  [MutationRoot("updateMyBook")]
  [Description("Atualiza um livro do 'meus livros'")]
  public MyBook UpdateMyBook(MyBook myBook)
  {
    context.MyBooks.Update(myBook);
    context.SaveChanges();
    return myBook;
  }

  [MutationRoot("deleteMyBook")]
  [Description("Deleta um 'livro do meus livros'")]
  public MyBook DeleteMyBook(MyBook myBook)
  {
    context.MyBooks.Remove(myBook);
    context.SaveChanges();
    return myBook;
  }
}