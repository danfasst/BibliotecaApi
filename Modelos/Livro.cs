public class Livro
{

    public int Id { get; set; }
    public string? Nome { get; set; }
    public string? Autor { get; set; }
    public string? AnoPublicado { get; set; }
    public string? Categoria { get; set; }

    //um para muitos (editora)
    public Editora? Editora { get; set; }
    
    //muitos para muitos (emprestimos)
    public List<Emprestimo>? Emprestimos { get; set; }

}