using Microsoft.EntityFrameworkCore;

namespace IcecreamShop.Db;

public record Icecream {
    public int Id { get; set; }
    public string? Name { get; set; }
}

public class IcecreamDb : DbContext {
    public IcecreamDb(DbContextOptions<IcecreamDb> options) : base(options) {}

    public DbSet<Icecream>? Icecreams { get; set; } = null!;
}
