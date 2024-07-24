using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GraphQL.AspNet.Attributes;

namespace _.GraphQL.Models;

[GraphType]
public class MyBook
{
  [Key, Column(TypeName = "BINARY(16)")]
  public required byte[] Id { get; set; }

  [ForeignKey("User")]
  public required byte[] UserId { get; set; }

  [ForeignKey("Book")]
  public required byte[] BookId { get; set; }

  [GraphField]
  public required int PagesRead { get; set; }

  [GraphField]
  public required DateTime DateStarted { get; set; }

  [GraphField]
  public required DateTime DateFinished { get; set; }

  [GraphField]
  public User? User { get; set; }

  [GraphField]
  public Book? Book { get; set; }
}