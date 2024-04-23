using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Configuração Swagger no builder
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Configuração banco MySQL
builder.Services.AddDbContext<BibliotecaContext>();

var app = builder.Build();

//Configuração Swagger no app
app.UseSwagger();
app.UseSwaggerUI();

//SWAGGER BIBLIOTECA http://localhost:5073/swagger/index.html

app.MapGet("/", () => "Biblioteca API");



app.MapLivrosAPI();

app.MapUsuariosAPI();

app.MapEmpretismoAPI();

app.Run();