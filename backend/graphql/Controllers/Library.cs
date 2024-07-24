using System.ComponentModel;
using _.Connection;
using _.GraphQL.Models;
using GraphQL.AspNet.Attributes;
using GraphQL.AspNet.Controllers;
using Microsoft.EntityFrameworkCore;

namespace _.GraphQL.Controllers;

public class LibraryController(C_BDContext context) : GraphController
{
  [QueryRoot("libraries")]
  [Description("Pega todas as bibliotecas")]
  public IEnumerable<Library> GetLibraries()
  {
    return [.. context.Libraries.Include(l => l.Books)];
  }

  [QueryRoot("libraryById")]
  [Description("Pega uma biblioteca pelo Id")]
  public Library? GetLibraryById(byte[] id)
  {
    return context.Libraries.Include(l => l.Books).SingleOrDefault(l => l.Id == id);
  }

  [MutationRoot("createLibrary")]
  [Description("Cria uma biblioteca")]
  public Library CreateLibrary(Library library)
  {
    context.Libraries.Add(library);
    context.SaveChanges();
    return library;
  }

  [MutationRoot("updateLibrary")]
  [Description("Atualiza uma biblioteca")]
  public Library UpdateLibrary(Library library)
  {
    context.Libraries.Update(library);
    context.SaveChanges();
    return library;
  }

  [MutationRoot("deleteLibrary")]
  [Description("Deleta uma biblioteca")]
  public Library DeleteLibrary(Library library)
  {
    context.Libraries.Remove(library);
    context.SaveChanges();
    return library;
  }
}