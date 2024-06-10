using Microsoft.EntityFrameworkCore;

public static class LivrosAPI
{

    public static void MapLivrosAPI(this WebApplication app)
    {

        var group = app.MapGroup("/livros");


        group.MapGet("/", async (BibliotecaContext db) =>
        {
            return await db.Livros.ToListAsync();
        }
        );

        group.MapGet("/{id}", async (int id, BibliotecaContext db) =>
        
            await db.Livros.FindAsync(id) is Livro Livro ? Results.Ok(Livro) : Results.NotFound()
        );

        group.MapPost("/", async (Livro livro, BibliotecaContext db) =>
        {
            livro.Emprestimos = await SalvarEmprestimos(livro, db);
            livro.Editora = await SalvarEditora(livro, db);

            db.Livros.Add(livro);
            await db.SaveChangesAsync();
            return Results.Created($"/Livros/{livro.Id}", livro);

        });

        async Task<Editora?> SalvarEditora(Livro livro, BibliotecaContext db)
        {
            Editora? editoraRetorno = livro.Editora;
            if (editoraRetorno is not null)
            {
                if (editoraRetorno.Id > 0)
                {
                    var eExistente = await db.Editoras.FindAsync(editoraRetorno.Id);
                    if (eExistente is not null)
                    {
                        editoraRetorno = eExistente;
                    }
                }
            }
            return editoraRetorno;
        }

        async Task<List<Emprestimo>> SalvarEmprestimos(Livro livro, BibliotecaContext db)
        {
            List<Emprestimo> emprestimos = new();
            if (livro is not null && livro.Emprestimos is not null
                && livro.Emprestimos.Count > 0)
            {

                foreach (var emprestimo in livro.Emprestimos)
                {
                    if (emprestimo.Id > 0)
                    {
                        var dExistente = await db.Emprestimos.FindAsync(emprestimo.Id);
                        if (dExistente is not null)
                        {
                            emprestimos.Add(dExistente);
                        }
                    }
                    else
                    {
                        emprestimos.Add(emprestimo);
                    }
                }
            }
            return emprestimos;
        }

        group.MapPut("/{id}", async (int id, Livro LivroAlterado, BibliotecaContext db) =>
        {
            var livro = await db.Livros.FindAsync(id);
            if (livro is null)
            {
                return Results.NotFound();
            }

            livro.Id = LivroAlterado.Id;
            livro.Nome = LivroAlterado.Nome;
            livro.Autor = LivroAlterado.Autor;
            livro.AnoPublicado = LivroAlterado.AnoPublicado;
            livro.Categoria = LivroAlterado.Categoria;

            livro.Emprestimos = await SalvarEmprestimos(livro, db);
            livro.Editora = await SalvarEditora(livro, db);

            await db.SaveChangesAsync();
            return Results.NoContent();

        });

        group.MapDelete("/{id}", async (int id, BibliotecaContext db) =>
        {
            if (await db.Livros.FindAsync(id) is Livro Livro)
            {
                db.Livros.Remove(Livro);
                await db.SaveChangesAsync();
                return Results.NoContent();
            }
            return Results.NotFound();

        });

    }

}