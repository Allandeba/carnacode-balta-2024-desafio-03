using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Imc.Extensions;

public static class EnumExtension
{
    public static string GetDisplayName(this Enum enumValue)
    {
        var displayName = enumValue.ToString();
        var displayAttributeName = enumValue.GetType()
            .GetMember(displayName)
            .FirstOrDefault()?
            .GetCustomAttribute<DisplayAttribute>()?
            .GetName();

        return displayAttributeName ?? displayName;
    }
}