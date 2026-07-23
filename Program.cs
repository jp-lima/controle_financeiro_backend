using Swashbuckle.AspNetCore; 
using Microsoft.EntityFrameworkCore; 
using ControleFinanceiro.Data;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter());
    });


builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseNpgsql(
      builder.Configuration.GetConnectionString("AppDbConnectionString"))); 

builder.Services.AddSwaggerGen(); 
builder.Services.AddControllers(); 

var app = builder.Build();
app.MapControllers();

app.UseSwagger(); 
app.UseSwaggerUI(); 

app.UseHttpsRedirection();
app.Run();

