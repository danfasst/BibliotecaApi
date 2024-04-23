using Microsoft.EntityFrameworkCore;

public static class UsuariosAPI
{

    public static void MapUsuariosAPI(this WebApplication app)
    {

        var group = app.MapGroup("/usuarios");


        group.MapGet("/", async (BibliotecaContext db) =>
        await db.Usuarios.ToListAsync()
        );

        group.MapGet("/{id}", async (int id, BibliotecaContext db) =>
        await db.Usuarios.FindAsync(id)
        is Usuario Usuario
        ? Results.Ok(Usuario)
        : Results.NotFound()
        );

        group.MapPost("/", async (Usuario Usuario, BibliotecaContext db) =>
        {
            db.Usuarios.Add(Usuario);
            await db.SaveChangesAsync();
            return Results.Created($"/Usuarios/{Usuario.Id}", Usuario);
        });

        group.MapPut("/{id}", async (int id, Usuario UsuarioAlterado, BibliotecaContext db) =>
        {
            var Usuario = await db.Usuarios.FindAsync(id);
            if (Usuario is null) return Results.NotFound();

            Usuario.Id = UsuarioAlterado.Id;
            Usuario.Nome = UsuarioAlterado.Nome;
            Usuario.CPF = UsuarioAlterado.CPF;
            Usuario.Email = UsuarioAlterado.Email;
            Usuario.Telefone = UsuarioAlterado.Telefone;
            Usuario.DataNascimento = UsuarioAlterado.DataNascimento;

            await db.SaveChangesAsync();
            return Results.NoContent();
        });

        group.MapDelete("/{id}", async (int id, BibliotecaContext db) =>
        {
            if (await db.Usuarios.FindAsync(id) is Usuario Usuario)
            {
                db.Usuarios.Remove(Usuario);
                await db.SaveChangesAsync();
                return Results.NoContent();
            }
            return Results.NotFound();
        });

    }

}