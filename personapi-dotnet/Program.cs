using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Models.Entities;
using Microsoft.OpenApi.Models;
using personapi_dotnet.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Registrar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Configurar DbContext con retry logic
builder.Services.AddDbContext<PersonaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
    sqlServerOptionsAction: sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5, // Número máximo de reintentos
            maxRetryDelay: TimeSpan.FromSeconds(10), // Tiempo de espera entre reintentos
            errorNumbersToAdd: null); // Puedes especificar errores adicionales para manejar.
    }));

// Registrar el repositorio y la interfaz
builder.Services.AddScoped<ITelefonoRepository, TelefonoRepository>();

// Agregar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Persona API", Version = "v1" });
});

var app = builder.Build();

// Habilitar Swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Persona API V1");
    c.RoutePrefix = string.Empty; // Serve Swagger UI at the app's root
});

// Aplicar CORS
app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Escuchar en el puerto 80 y todas las interfaces
app.Urls.Add("http://0.0.0.0:80");

app.Run();
