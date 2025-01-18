using Microsoft.OpenApi.Models;
using IcecreamShop.DB;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "IcecreamShop API", Description = "Your favourite dessert", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "IcecreamShop API V1"));
}

app.MapGet("/", () => "Icecream Shop API");
app.MapGet("/icecreams/{id}", (int id) => IcecreamDB.GetIcecream(id));
app.MapGet("/icecreams", () => IcecreamDB.GetIcecreams());
app.MapPost("/icecreams", (Icecream icecream) => IcecreamDB.CreateIcecream(icecream));
app.MapPut("/icecreams", (Icecream icecream) => IcecreamDB.UpdateIcecream(icecream));
app.MapDelete("/icecreams/{id}", (int id) => IcecreamDB.DeleteIcecream(id));

app.Run();
