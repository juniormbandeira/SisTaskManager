using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models; // Adicione este using
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using UserManagement.API.Data;
using UserManagement.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Configuração dos serviços
builder.Services.AddControllers();

// Configuração do Swagger mais robusta
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "UserManagement API", Version = "v1" });

    // Adicione esta configuração para resolver o conflito de nomes:
    c.CustomSchemaIds(type => type.FullName?.Replace("+", "."));

    // Opcional: Organize os controllers por tags
    c.TagActionsBy(api => new[] { api.GroupName ?? api.ActionDescriptor.RouteValues["controller"] });
});

// Configuração do MySQL/MariaDB
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MariaDbServerVersion(new Version(10, 4, 32)),
        mysqlOptions =>
        {
            mysqlOptions.SchemaBehavior(MySqlSchemaBehavior.Ignore);
            mysqlOptions.EnableRetryOnFailure(
               maxRetryCount: 5,
               maxRetryDelay: TimeSpan.FromSeconds(30),
               errorNumbersToAdd: null); // Adicione explicitamente 'null'
        }
    )
);

// Injeção de dependência
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<PerfilService>();

var app = builder.Build();

// Pipeline de requisições HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "User Management API v1");
        c.RoutePrefix = "swagger"; // Define a rota como /swagger
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();