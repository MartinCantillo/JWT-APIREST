var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Agregar CORS
builder.Services.AddCors(options =>
{
    // Definir una política CORS llamada "AllowAnyOrigin"
    options.AddPolicy("AllowAnyOrigin", builder => 
    {
        // Permitir solicitudes desde cualquier origen
        builder.AllowAnyOrigin()
               // Permitir cualquier método HTTP
               .AllowAnyMethod()
               // Permitir cualquier encabezado HTTP
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configurar el pipeline de solicitudes HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

// Aplicar la política CORS "AllowAnyOrigin" a todas las solicitudes entrantes
app.UseCors("AllowAnyOrigin");

app.UseAuthorization();

app.MapControllers();

app.Run();
