using System.ComponentModel;
using _.Connection;
using _.GraphQL.Models;
using GraphQL.AspNet.Attributes;
using GraphQL.AspNet.Controllers;
using Microsoft.EntityFrameworkCore;

namespace _.GraphQL.Controllers;

public class UserController(C_BDContext context) : GraphController
{
  [QueryRoot("users")]
  [Description("Pega todos os usuários")]
  public IEnumerable<User> GetUsers()
  {
    return [.. context.Users.Include(u => u.Account).Include(u => u.Wishlist).Include(u => u.MyBooks)];
  }

  [QueryRoot("userById")]
  [Description("Pega um usuario pelo Id")]
  public User? GetUserById(byte[] id)
  {
    return context.Users
      .Include(u => u.Account)
      .Include(u => u.Wishlist)
      .Include(u => u.MyBooks)
      .SingleOrDefault(u => u.Id == id);
  }

  [MutationRoot("createUser")]
  [Description("Cria um usuario")]
  public User CreateUser(User user)
  {
    context.Users.Add(user);
    context.SaveChanges();
    return user;
  }

  [MutationRoot("updateUser")]
  [Description("Atualiza um usuario")]
  public User UpdateUser(User user)
  {
    context.Users.Update(user);
    context.SaveChanges();
    return user;
  }

  [MutationRoot("deleteUser")]
  [Description("Deleta um usuario")]
  public User DeleteUser(User user)
  {
    context.Users.Remove(user);
    context.SaveChanges();
    return user;
  }
}