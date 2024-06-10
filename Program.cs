using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Configuração CORS
builder.Services.AddCors();


//Configuração Swagger no builder
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Configuração banco MySQL
builder.Services.AddDbContext<BibliotecaContext>();

var app = builder.Build();

app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod()
);

//Configuração Swagger no app
app.UseSwagger();
app.UseSwaggerUI();

// http://localhost:5073/swagger/index.html

app.MapGet("/", () => "Biblioteca API");

app.MapLivrosAPI();

app.MapUsuariosAPI();

app.MapEmprestimosAPI();

app.MapEditorasAPI();

app.Run();