using Microsoft.OpenApi.Models;
using IcecreamShop.Db;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<IcecreamDb>(options => options.UseInMemoryDatabase("IcecreamDb"));

builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "IcecreamShop API", Description = "Your favourite dessert", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "IcecreamShop API V1"));
}

app.MapGet("/", () => "Icecream Shop API");

app.MapGet("/icecreams", async (IcecreamDb db) => await db.Icecreams.ToListAsync());
app.MapGet("/icecream/{id}", async (int id, IcecreamDb db) => await db.Icecreams.FindAsync(id));

app.MapPost("/icecream", async (Icecream icecream, IcecreamDb db) => {
    await db.Icecreams.AddAsync(icecream);
    await db.SaveChangesAsync();
    return Results.Created($"/icecream/{icecream.Id}", icecream);
});

app.MapPut("/icecream/{id}", async (IcecreamDb db, Icecream updatedIcecream, int id) => {
    var icecream = await db.Icecreams.FindAsync(id);
    if (icecream is null) {
        return Results.NotFound();
    }
    icecream.Name = updatedIcecream.Name;
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/icecream/{id}", async (IcecreamDb db, int id) => {
    var icecream = await db.Icecreams.FindAsync(id);
    if (icecream is null) {
        return Results.NotFound();
    }
    db.Icecreams.Remove(icecream);
    await db.SaveChangesAsync();
    return Results.Ok();
});

app.Run();
