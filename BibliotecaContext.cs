using Microsoft.EntityFrameworkCore;

public class BibliotecaContext : DbContext
{

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Livro> Livros { get; set; }
    public DbSet<Emprestimo> Emprestimos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseMySQL("server=localhost;port=3306;database=biblioteca;user=root;password=1234");
    }

}