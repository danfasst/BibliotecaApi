using Microsoft.EntityFrameworkCore;

public static class EmprestimosAPI
{

    public static void MapEmprestimosAPI(this WebApplication app)
    {

        var group = app.MapGroup("/emprestimos");


        group.MapGet("/", async (BibliotecaContext db) =>
        
        await db.Emprestimos.ToListAsync()
        );

        group.MapGet("/{id}", async (int id, BibliotecaContext db) =>
        
        await db.Emprestimos.FindAsync(id) is Emprestimo emprestimo ? 
        Results.Ok(emprestimo) : Results.NotFound()
        );

        group.MapPost("/", async (Emprestimo emprestimo, BibliotecaContext db) =>
        {
            db.Emprestimos.Add(emprestimo);
            await db.SaveChangesAsync();
            return Results.Created($"/emprestimos/{emprestimo.Id}", emprestimo);
        });

        group.MapPut("/{id}", async (int id, Emprestimo emprestimoAlterado, BibliotecaContext db) =>
        {
            var emprestimo = await db.Emprestimos.FindAsync(id);
            if (emprestimo is null) return Results.NotFound();

            emprestimo.Id = emprestimoAlterado.Id;
            emprestimo.NomePessoa = emprestimoAlterado.NomePessoa;
            emprestimo.NomeLivro = emprestimoAlterado.NomeLivro;
            emprestimo.DataEmprestimo = emprestimoAlterado.DataEmprestimo;
            emprestimo.DataDevolucao = emprestimoAlterado.DataDevolucao;

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