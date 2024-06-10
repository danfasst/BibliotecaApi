using Microsoft.EntityFrameworkCore;

public static class EmprestimosAPI
{

    public static void MapEmprestimosAPI(this WebApplication app)
    {

        var group = app.MapGroup("/emprestimos");

        group.MapGet("/", async (BibliotecaContext db) =>
        {
            return await db.Emprestimos.ToListAsync();
        }
        );

        group.MapGet("/{id}", async (int id, BibliotecaContext db) =>
            await db.Emprestimos.FindAsync(id) is Emprestimo emprestimo ? Results.Ok(emprestimo) : Results.NotFound()
        );

        group.MapPost("/", async (Emprestimo emprestimo, BibliotecaContext db) =>
        {
            if (emprestimo.Usuario != null || emprestimo.Livros == null || !emprestimo.Livros.Any())
        {
            return Results.BadRequest("Emprestimo must have a valid UsuarioId and at least one Livro.");
        }

        foreach (var livro in emprestimo.Livros)
        {
            var livroExistente = await db.Livros.FindAsync(livro.Id);
            if (livroExistente != null)
            {
                emprestimo.Livros.Add(livroExistente);
            }
        }

        db.Emprestimos.Add(emprestimo);
        await db.SaveChangesAsync();
        return Results.Created($"/emprestimos/{emprestimo.Id}", emprestimo);
        });

        async Task<List<Livro>> SalvarLivros(Emprestimo emprestimo, BibliotecaContext db)
        {
            List<Livro> livros = new();
            if (emprestimo is not null && emprestimo.Livros is not null
                && emprestimo.Livros.Count > 0)
            {

                foreach (var livro in emprestimo.Livros)
                {
                    if (livro.Id > 0)
                    {
                        var dExistente = await db.Livros.FindAsync(livro.Id);
                        if (dExistente is not null)
                        {
                            livros.Add(dExistente);
                        }
                    }
                    else
                    {
                        livros.Add(livro);
                    }
                }
            }
            return livros;
        }

        async Task<Usuario?> SalvarUsuario(Emprestimo emprestimo, BibliotecaContext db)
        {
            Usuario? usuarioRetorno = emprestimo.Usuario;
            if (usuarioRetorno is not null)
            {
                if (usuarioRetorno.Id > 0)
                {
                    var eExistente = await db.Usuarios.FindAsync(usuarioRetorno.Id);
                    if (eExistente is not null)
                    {
                        usuarioRetorno = eExistente;
                    }
                }
            }
            return usuarioRetorno;
        }


        group.MapPut("/{id}", async (int id, Emprestimo emprestimoAlterado, BibliotecaContext db) =>
        {
            var emprestimo = await db.Emprestimos.FindAsync(id);
            if (emprestimo is null) return Results.NotFound();

            emprestimo.Id = emprestimoAlterado.Id;
            emprestimo.DataEmprestimo = emprestimoAlterado.DataEmprestimo;
            emprestimo.DataDevolucao = emprestimoAlterado.DataDevolucao;

            emprestimo.Usuario = await SalvarUsuario(emprestimo, db);
            emprestimo.Livros = await SalvarLivros(emprestimo, db);

            await db.SaveChangesAsync();
            return Results.NoContent();
        });

        group.MapDelete("/{id}", async (int id, BibliotecaContext db) =>
        {
            if (await db.Emprestimos.FindAsync(id) is Emprestimo emprestimo)
            {
                db.Emprestimos.Remove(emprestimo);
                await db.SaveChangesAsync();
                return Results.NoContent();
            }
            return Results.NotFound();
        });

    }

}