using MySql.Data.MySqlClient;
using Microsoft.EntityFrameworkCore;
using _.GraphQL.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace _.Connection
{
  public class Connection
  {
    public static MySqlConnection Conectar()
    {
      string servidor = "localhost";
      string porta = "3306";
      string usuario = "root";
      string senha = "1234";
      string database = "programacao";

      string stringConexao = $"server={servidor};port={porta};user={usuario};password={senha};database={database}";
      return new MySqlConnection(stringConexao);
    }
  }

  public class C_BDContext(DbContextOptions<C_BDContext> options) : DbContext(options)
  {
    public DbSet<User> Users { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Wishlist> Wishlists { get; set; }
    public DbSet<Library> Libraries { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<BookInWishlist> BooksInWishlists { get; set; }
    public DbSet<MyBook> MyBooks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseMySQL(Connection.Conectar());
      base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Library>(entity =>
      {
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Name).HasMaxLength(100).IsRequired();
        entity.HasMany(e => e.Books).WithOne(e => e.Library).HasForeignKey(e => e.LibraryId);
      });

      modelBuilder.Entity<Book>(entity =>
      {
        entity.HasKey(e => e.Id);
        entity.HasIndex(e => e.LibraryId);
        entity.Property(e => e.Title).HasMaxLength(100).IsRequired();
        entity.Property(e => e.Author).HasMaxLength(50).IsRequired();
        entity.Property(e => e.Editor).HasMaxLength(50).IsRequired();
        entity.Property(e => e.Image).IsRequired().HasColumnType("LONGBLOB");
        entity.Property(e => e.Description).IsRequired();
        entity.Property(e => e.Categories).IsRequired().HasConversion(
            v => string.Join(',', v),
            v => v.Split(',', StringSplitOptions.RemoveEmptyEntries))
            .Metadata.SetValueComparer(new ValueComparer<string[]>(
                        (c1, c2) => c1 == c2 || (c1 != null && c2 != null && c1.SequenceEqual(c2)),
                        c => c == null ? 0 : c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                        c => c.ToArray()));
        entity.Property(e => e.Pages).IsRequired();
        entity.Property(e => e.Rate).IsRequired();
        entity.Property(e => e.Year).IsRequired();
        entity.Property(e => e.File).IsRequired().HasColumnType("LONGBLOB");
        entity.Property(e => e.Isbn).HasMaxLength(13).IsRequired().HasColumnType("CHAR(13)");
        entity.Property(e => e.Available).HasDefaultValue(true);
        entity.HasOne(e => e.Library).WithMany(e => e.Books).HasForeignKey(e => e.LibraryId);
        entity.HasMany(e => e.MyBooks).WithOne(e => e.Book).HasForeignKey(e => e.BookId);
        entity.HasMany(e => e.BooksInWishlist).WithOne(e => e.Book).HasForeignKey(e => e.Id_Book);
        entity.ToTable(t => t.HasCheckConstraint("CK_Book_Rate", "`Rate` >= 0 AND `Rate` <= 5"));
      });

      modelBuilder.Entity<User>(entity =>
      {
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CategoriesPreferences).HasConversion(
          v => v == null ? null : string.Join(',', v),
          v => v == null ? null : v.Split(',', StringSplitOptions.RemoveEmptyEntries))
          .Metadata.SetValueComparer(new ValueComparer<string[]>(
                      (c1, c2) => c1 == c2 || (c1 != null && c2 != null && c1.SequenceEqual(c2)),
                      c => c == null ? 0 : c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                      c => c.ToArray()));
        entity.Property(e => e.Description).HasMaxLength(300);
        entity.Property(e => e.Telephone).HasMaxLength(15);
        entity.Property(e => e.Birthday).HasColumnType("DATE").HasConversion(
            v => v.ToDateTime(TimeOnly.MinValue),
            v => DateOnly.FromDateTime(v));
        entity.HasOne(e => e.Wishlist).WithOne(e => e.User).HasForeignKey<Wishlist>(e => e.UserId);
        entity.HasOne(e => e.Account).WithOne(e => e.User).HasForeignKey<Account>(e => e.UserId);
        entity.HasMany(e => e.MyBooks).WithOne(e => e.User).HasForeignKey(e => e.UserId);
      });

      modelBuilder.Entity<Account>(entity =>
      {
        entity.HasKey(e => e.Id);
        entity.HasIndex(e => new { e.UserId, e.Email }).IsUnique();
        entity.Property(e => e.Name).IsRequired().HasMaxLength(150);
        entity.Property(e => e.Image).IsRequired().HasColumnType("LONGBLOB");
        entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
        entity.Property(e => e.Password).IsRequired().HasMaxLength(100);
        entity.HasOne(e => e.User).WithOne(e => e.Account).HasForeignKey<Account>(e => e.UserId);
      });

      modelBuilder.Entity<MyBook>(entity =>
      {
        entity.HasKey(e => e.Id);
        entity.HasIndex(e => new { e.UserId, e.BookId }).IsUnique();
        entity.Property(e => e.DateStarted).IsRequired().HasColumnType("DATETIME").HasDefaultValue(DateTime.Now);
        entity.Property(e => e.DateFinished).IsRequired().HasColumnType("DATETIME").HasDefaultValue(DateTime.Now.AddDays(7));
        entity.Property(e => e.PagesRead).IsRequired().HasDefaultValue(0);
        entity.HasOne(e => e.User).WithMany(e => e.MyBooks).HasForeignKey(e => e.UserId);
        entity.HasOne(e => e.Book).WithMany(e => e.MyBooks).HasForeignKey(e => e.BookId);
      });

      modelBuilder.Entity<Wishlist>(entity =>
      {
        entity.HasKey(e => e.Id);
        entity.HasIndex(e => e.UserId).IsUnique();
        entity.HasMany(e => e.BooksInWishlist).WithOne(e => e.Wishlist).HasForeignKey(e => e.Id_Wishlist);
        entity.HasOne(e => e.User).WithOne(e => e.Wishlist).HasForeignKey<Wishlist>(e => e.UserId);
      });

      modelBuilder.Entity<BookInWishlist>(entity =>
      {
        entity.HasKey(e => new { e.Id_Wishlist, e.Id_Book });
        entity.HasIndex(e => new { e.Id_Wishlist, e.Id_Book }).IsUnique();
        entity.HasOne(e => e.Wishlist).WithMany(e => e.BooksInWishlist).HasForeignKey(e => e.Id_Wishlist);
        entity.HasOne(e => e.Book).WithMany(e => e.BooksInWishlist).HasForeignKey(e => e.Id_Book);
      });

      modelBuilder.Seed();
      base.OnModelCreating(modelBuilder);
    }
  }
}