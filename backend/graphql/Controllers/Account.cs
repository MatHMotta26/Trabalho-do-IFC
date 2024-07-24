using System.ComponentModel;
using _.Connection;
using _.GraphQL.Models;
using GraphQL.AspNet.Attributes;
using GraphQL.AspNet.Controllers;
using Microsoft.EntityFrameworkCore;

namespace _.GraphQL.Controllers;

public class AccountController(C_BDContext context) : GraphController
{
  [QueryRoot("accounts")]
  [Description("Pega todas as contas")]
  public IEnumerable<Account> GetAccounts()
  {
    return [.. context.Accounts.Include(a => a.User)];
  }

  [QueryRoot("accountByEmail")]
  [Description("Pega uma conta pelo Email")]
  public Account? GetAccountByEmail(string email)
  {
    return context.Accounts.Include(a => a.User).SingleOrDefault(a => a.Email == email);
  }

  [MutationRoot("createAccount")]
  [Description("Cria uma conta")]
  public Account CreateAccount(string id, string name, string image, string email, string? password, string provider, byte[]? userId, User? user)
  {
    byte[] imageBytes = Convert.FromBase64String(image);
    var account = new Account
    {
      Id = id,
      Name = name,
      Image = imageBytes,
      Email = email,
      Password = password,
      Provider = provider,
      UserId = userId,
      User = user
    };

    context.Accounts.Add(account);
    context.SaveChanges();
    return account;
  }

  [MutationRoot("updateAccount")]
  [Description("Atualiza uma conta")]
  public Account UpdateAccount(Account account)
  {
    context.Accounts.Update(account);
    context.SaveChanges();
    return account;
  }

  [MutationRoot("deleteAccount")]
  [Description("Deleta uma conta")]
  public Account DeleteAccount(Account account)
  {
    context.Accounts.Remove(account);
    context.SaveChanges();
    return account;
  }
}