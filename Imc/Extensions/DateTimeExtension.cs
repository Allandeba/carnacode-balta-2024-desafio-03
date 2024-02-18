namespace Imc.Extensions;

public static class DateTimeExtension
{
    public static string ToPtBR(this DateTime dateTime)
    {
        return dateTime.ToString("dd/MM/yyyy");
    }
}