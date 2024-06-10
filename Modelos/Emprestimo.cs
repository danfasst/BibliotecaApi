public class Emprestimo
{
    public int Id { get; set; }
    public string? DataEmprestimo { get; set; }
    public string? DataDevolucao { get; set; }

    //muitos para muitos (livros)
    public List<Livro>? Livros { get; set; }

    //um para muitos (usuario)
    public Usuario? Usuario { get; set; }
}