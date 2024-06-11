# api-biblioteca
 

Node.js

npm install

npm start


Dotnet

dotnet ef migrations (se já tem)

dotnet ef database update (gerar banco de dados) -  Se der erro configurar string de conexão exemplo: 

BibliotecaContext.cs

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseMySQL("server=localhost;port=3306;database=biblioteca;user=root;password=1234");
    }

dotnet run
