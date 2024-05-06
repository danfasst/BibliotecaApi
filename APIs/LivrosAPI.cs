using Microsoft.EntityFrameworkCore;

public static class LivrosAPI
{

    public static void MapLivrosAPI(this WebApplication app)
    {

        var group = app.MapGroup("/livros");
        

        group.MapGet("/", async (BibliotecaContext db) =>
        await db.Livros.ToListAsync()
        );

        group.MapGet("/{id}", async (int id, BibliotecaContext db) =>
        await db.Livros.FindAsync(id)
        is Livro Livro
        ? Results.Ok(Livro)
        : Results.NotFound()
        );

        group.MapPost("/", async (Livro Livro, BibliotecaContext db) =>
        {
            db.Livros.Add(Livro);
            await db.SaveChangesAsync();
            return Results.Created($"/Livros/{Livro.Id}", Livro);

        });

        group.MapPut("/{id}", async (int id, Livro LivroAlterado, BibliotecaContext db) =>
        {
            var Livro = await db.Livros.FindAsync(id);
            if (Livro is null) {
                return Results.NotFound();
            }

            Livro.Id = LivroAlterado.Id;
            Livro.Nome = LivroAlterado.Nome;
            Livro.Autor = LivroAlterado.Autor;
            Livro.AnoPublicado = LivroAlterado.AnoPublicado;
            Livro.Categoria = LivroAlterado.Categoria;

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