using GeekGarden.API.Data;
using GeekGarden.API.Services;
using GeekGarden.API.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ----------------------------------------------------
// 1️⃣ Services registration
// ----------------------------------------------------
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database Context
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Email & Utility Services
builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));
builder.Services.AddScoped<EmailService>();
builder.Services.AddHttpContextAccessor();

// Optional: Allow CORS (frontend bisa akses API)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// ----------------------------------------------------
// 2️⃣ Middleware pipeline
// ----------------------------------------------------

// Always enable Swagger (even in Production)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "GeekGarden API v1");
    c.RoutePrefix = "swagger"; // accessible via /swagger
});

// Optional: Global error handler (bisa ditambah logging)
app.UseExceptionHandler("/error");

// Redirect HTTP -> HTTPS (optional)
app.UseHttpsRedirection();

// Enable CORS
app.UseCors("AllowAll");

// Authorization
app.UseAuthorization();

// Map controllers
app.MapControllers();

// ----------------------------------------------------
// 3️⃣ Run the app
// ----------------------------------------------------
app.Run();
