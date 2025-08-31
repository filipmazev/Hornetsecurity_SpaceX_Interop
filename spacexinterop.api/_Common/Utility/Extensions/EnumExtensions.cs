using System.Text.Json.Serialization;
using System.Reflection;

namespace spacexinterop.api._Common.Utility.Extensions;

public static class EnumExtensions
{
    public static string GetJsonPropertyName(this Enum value)
    {
        MemberInfo? member = value.GetType().GetMember(value.ToString()).FirstOrDefault();
        
        if (member is null) return value.ToString();
        
        return member.GetCustomAttributes(typeof(JsonPropertyNameAttribute), false)
            .FirstOrDefault() is JsonPropertyNameAttribute attr ? attr.Name : value.ToString(); // fallback
    }
}
