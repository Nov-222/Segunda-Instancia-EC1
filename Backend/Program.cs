using Backend.Repositorios;
using Backend.Servicios;
using MySqlConnector;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy.WithOrigins("http://127.0.0.1:5500", "http://localhost:5500", "https://segunda-instancia-ec1.onrender.com") // El puerto de Live Server
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

builder.Services.AddControllers();
builder.Services.AddScoped<IReservarEstadia, ReservarEstadia>();
builder.Services.AddScoped<IReservarServicio, ReservarServicio>();
builder.Services.AddScoped<IConsultaReservas, ConsultaReservas>();
builder.Services.AddScoped<IConsultaServicio, ConsultaServicio>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseCors("AllowFrontend");
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
    string connectionString = config.GetConnectionString("DefaultConnection");
    const string Separador = "-------------------------------------------------";
    const string MensajeExito = "¡CONEXIÓN EXITOSA A LA BASE DE DATOS REC!";
    try
    {
        using (var connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            Console.WriteLine(Separador);
            Console.WriteLine(MensajeExito);
            Console.WriteLine(Separador);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(Separador);
        Console.WriteLine($"ERROR DE CONEXIÓN: {ex.Message}");
        Console.WriteLine(Separador);
    }
}

app.Run();