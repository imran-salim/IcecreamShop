namespace IcecreamShop.DB;

public record Icecream {
    public int Id { get; set; }
    public string? Name { get; set; }
}

public class IcecreamDB {
    private static List<Icecream> _icecreams = new List<Icecream> {
        new Icecream { Id = 1, Name = "Salted Caramel" },
        new Icecream { Id = 2, Name = "Cookies and Cream" },
        new Icecream { Id = 3, Name = "Chocolate" }
    };

    public static List<Icecream> GetIcecreams() {
        return _icecreams;
    }

    public static Icecream? GetIcecream(int id) {
        return _icecreams.SingleOrDefault(icecream => icecream.Id == id);
    }

    public static Icecream CreateIcecream(Icecream icecream) {
        _icecreams.Add(icecream);
        return icecream;
    }

    public static Icecream UpdateIcecream(Icecream update) {
        _icecreams = _icecreams.Select(icecream => {
            if (icecream.Id == update.Id) {
                icecream.Name = update.Name;
            }
            return icecream;
        }).ToList();
        return update;
    }

    public static void DeleteIcecream(int id) {
        _icecreams = _icecreams.FindAll(icecream => icecream.Id != id).ToList();
    }
}
