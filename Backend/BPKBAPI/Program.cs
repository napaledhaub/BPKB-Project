using BPKBAPI.Data;
using BPKBAPI.Models;
using BPKBAPI.Repository;
using BPKBAPI.Repository.Interface;
using BPKBAPI.Service;
using BPKBAPI.Service.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var conString = builder.Configuration.GetConnectionString("DatabaseConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(conString));

builder.Services.Configure<DogApiSettings>(builder.Configuration.GetSection("DogApiSettings"));

builder.Services.AddScoped<IStorageLocationRepository, StorageLocationRepository>();
builder.Services.AddScoped<IStorageLocationService, StorageLocationService>();
builder.Services.AddScoped<IBPKBRepository, BPKBRepository>();
builder.Services.AddScoped<IBPKBService, BPKBService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddHttpClient<IDogBreedsRepository, DogBreedsRepository>();
builder.Services.AddScoped<IDogBreedsRepository, DogBreedsRepository>();
builder.Services.AddScoped<IDogBreedsService, DogBreedsService>();

var app = builder.Build();

if (!app.Environment.IsProduction())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
