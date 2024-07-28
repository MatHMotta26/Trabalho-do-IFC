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
  public async Task<IEnumerable<Account>> GetAccounts()
  {
    var accounts = await context.Accounts.Include(a => a.User).ToListAsync();
    return accounts;
  }

  [QueryRoot("accountByEmail")]
  [Description("Pega uma conta pelo Email")]
  public async Task<Account> GetAccountByEmail(string email)
  {
    var account = await context.Accounts.Include(a => a.User).SingleOrDefaultAsync(a => a.Email == email) ?? throw new Exception("Usuário não encontrado");
    return account;
  }

  [MutationRoot("createAccount")]
  [Description("Cria uma conta")]
  public async Task<bool> CreateAccount(string name, string image, string email, string password)
  {
    var id = Guid.NewGuid().ToByteArray();
    byte[] imageBytes = Convert.FromBase64String(image);

    var account = new Account
    {
      Id = id,
      Name = name,
      Image = imageBytes,
      Email = email,
      Password = password,
    };

    context.Accounts.Add(account);
    await context.SaveChangesAsync();
    return true;
  }

  [MutationRoot("updateAccount")]
  [Description("Atualiza uma conta")]
  public async Task<bool> UpdateAccount(string id, Account updatedAccount)
  {
    var existingAccount = await context.Accounts.FindAsync(id) ?? throw new Exception("Conta não encontrada");

    context.Entry(existingAccount).CurrentValues.SetValues(updatedAccount);
    await context.SaveChangesAsync();
    return true;
  }

  [MutationRoot("deleteAccount")]
  [Description("Deleta uma conta")]
  public async Task<bool> DeleteAccount(string id)
  {
    var account = await context.Accounts.FindAsync(id) ?? throw new Exception("Conta não encontrada");

    context.Accounts.Remove(account);
    await context.SaveChangesAsync();
    return true;
  }
}