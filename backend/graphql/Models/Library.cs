using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GraphQL.AspNet.Attributes;

namespace _.GraphQL.Models;

[GraphType]
public class Library
{
  [Key, Column(TypeName = "BINARY(16)")]
  public required byte[] Id { get; set; }

  [GraphField]
  public required string Name { get; set; }

  [GraphField]
  public ICollection<Book> Books { get; set; } = [];
}
