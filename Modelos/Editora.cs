public class Editora
{
    public int Id { get; set; }
    public string? Nome { get; set; }

    //um para muitos (livro)
    public List<Livro>? Livros { get; set; }

}