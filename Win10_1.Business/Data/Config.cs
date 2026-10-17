using System.IO;
using System.Text.Json;

namespace Win10_1.Business.Data;

public static class Config
{
    public static string GetConnectionString()
    {
        var path = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
        var json = File.ReadAllText(path);
        var doc = JsonDocument.Parse(json);
        var root = doc.RootElement;

        var conn = root.GetProperty("ConnectionStrings");
        return conn
            .GetProperty("DefaultConnection")
            .GetString()
            ?? throw new InvalidOperationException("Connection string not found");
    }
}
