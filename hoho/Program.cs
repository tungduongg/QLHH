using hoho.Data;
using hoho.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<QLHHDatacontext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("QLHH"));
});

builder.Services.AddScoped<IOUOMRepository, OUOMRepository>();
builder.Services.AddScoped<IUGP1Repository, UGP1Repository>();
builder.Services.AddScoped<IOUGPRepository, OUGPRepository>();
builder.Services.AddScoped<IOITMRepository, OITMRepository>();
builder.Services.AddScoped<IOITWRepository, OITWRepository>();
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IUGP1Repository, UGP1Repository>();
builder.Services.AddScoped<IOUGPRepository, OUGPRepository>();
builder.Services.AddScoped<IOITMRepository, OITMRepository>();
builder.Services.AddScoped<IOITWRepository, OITWRepository>();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
