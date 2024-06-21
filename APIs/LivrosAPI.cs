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