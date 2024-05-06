using Microsoft.EntityFrameworkCore;

public static class EditorasAPI
{

    public static void MapEditorasAPI(this WebApplication app)
    {

        var group = app.MapGroup("/editoras");


        group.MapGet("/", async (BibliotecaContext db) =>
        
        await db.Editoras.ToListAsync()
        );

        group.MapGet("/{id}", async (int id, BibliotecaContext db) =>
        
        await db.Editoras.FindAsync(id) is Editora editora ? 
        Results.Ok(editora) : Results.NotFound()
        );

        group.MapPost("/", async (Editora editora, BibliotecaContext db) =>
        {
            db.Editoras.Add(editora);
            await db.SaveChangesAsync();
            return Results.Created($"/editoras/{editora.Id}", editora);
        });

        group.MapPut("/{id}", async (int id, Editora editoraAlterada, BibliotecaContext db) =>
        {
            var editora = await db.Editoras.FindAsync(id);
            if (editora is null) return Results.NotFound();

            editora.Id = editoraAlterada.Id;
            editora.Nome = editoraAlterada.Nome;
            editora.NomeLivro = editoraAlterada.NomeLivro;

            await db.SaveChangesAsync();
            return Results.NoContent();
        });

        group.MapDelete("/{id}", async (int id, BibliotecaContext db) =>
        {
            if (await db.Editoras.FindAsync(id) is Editora editora)
            {
                db.Editoras.Remove(editora);
                await db.SaveChangesAsync();
                return Results.NoContent();
            }
            return Results.NotFound();
        });

    }

}