using KnyguNuoma.Contracts;
using KnyguNuoma.Repositories;
using KnyguNuoma.Services;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// MongoDB setup
builder.Services.AddSingleton<IMongoClient>(new MongoClient("mongodb+srv://rsendzikas:MongoDBvaliklis1?@cluster0.00as9.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0"));

// Register services
builder.Services.AddScoped<IKnyguRepozitorija, KnyguRepozitorija>();
builder.Services.AddScoped<IKnyguServisas, KnyguServisas>();
builder.Services.AddScoped<INuomosRepozitorija, NuomosRepozitorija>();
builder.Services.AddScoped<INuomosServisas, NuomosServisas>();
builder.Services.AddScoped<INaudotojuRepozitorija, NaudotojuRepozitorija>();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
