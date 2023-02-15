using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleApp1;

public interface IJsonObject
{
    string Name { get; }
}
public class JsonObject : IJsonObject
{
    public string Name { get; set; } = default!;
    public int Code { get; set; }
}

public static class JsonSerializerExtensions
{
    public static string JsonSerialize<T>(this T obj)
    {
        if (obj == null)
        {
            return JsonSerializer.Serialize(obj, JsonSerializerOptions);
        }
        return JsonSerializer.Serialize(obj, obj.GetType(), JsonSerializerOptions);
    }
    public static string JsonSerialize(this object obj, Type type)
    {
        return JsonSerializer.Serialize(obj, type, JsonSerializerOptions);
    }
    private static JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        IncludeFields = true,
        PropertyNameCaseInsensitive = true,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };
}