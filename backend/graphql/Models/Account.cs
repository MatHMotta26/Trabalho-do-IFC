using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GraphQL.AspNet.Attributes;

namespace _.GraphQL.Models;

[GraphType]
public class Account
{
  [Key]
  public required string Id { get; set; }

  [GraphField]
  public required string Name { get; set; }

  [GraphField]
  public required byte[] Image { get; set; }

  [GraphField]
  public required string Email { get; set; }

  [GraphField]
  public string? Password { get; set; }

  [GraphField]
  public required string Provider { get; set; }

  [ForeignKey("User"), Column(TypeName = "BINARY(16)")]
  public byte[]? UserId { get; set; }

  [GraphField]
  public User? User { get; set; }
}
