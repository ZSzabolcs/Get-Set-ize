using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using proba1.Data;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string MyAllowSpecificorigins = "_myAllowSpecificOrigin";

builder.Services.AddCors(options =>{
    options.AddPolicy(MyAllowSpecificorigins,
        policy =>{
            policy.WithOrigins("*")
            .AllowAnyHeader()
            .AllowAnyMethod();
        }
    )
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition =
            System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString,ServerVersion.AutoDetect(connectionString)
    )
);
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MyAllowSpecificorigins);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

