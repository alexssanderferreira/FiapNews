using FiapNews.Configuracao;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiConfig(builder.Configuration);

var app = builder.Build();

app.UseConfig();

app.Run();
