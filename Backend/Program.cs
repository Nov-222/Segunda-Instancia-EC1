using Backend.Repositorios; 
using Backend.Servicios;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy.WithOrigins("http://127.0.0.1:5500", "http://localhost:5500") // El puerto de Live Server
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<IReservarEstadia, ReservarEstadia>();

builder.Services.AddScoped<IReservarServicio, ReservarServicio>();

builder.Services.AddScoped<IConsultaReservas, ConsultaReservas>();

builder.Services.AddScoped<IConsultaServicio, ConsultaServicio>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();



using (var scope = app.Services.CreateScope())
{
    var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
    string connectionString = config.GetConnectionString("DefaultConnection");

    try
    {
        using (var connection = new Microsoft.Data.SqlClient.SqlConnection(connectionString))
        {
            connection.Open();
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("¡CONEXIÓN EXITOSA A LA BASE DE DATOS REC!");
            Console.WriteLine("-------------------------------------------------");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("-------------------------------------------------");
        Console.WriteLine($"ERROR DE CONEXIÓN: {ex.Message}");
        Console.WriteLine("-------------------------------------------------");
    }
}


app.Run();
