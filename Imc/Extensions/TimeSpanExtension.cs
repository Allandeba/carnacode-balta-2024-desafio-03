namespace Imc.Extensions;
using static Imc.Shared.Constants.DateTimeConstants;

public static class TimeSpanExtension
{
    public static string ToPtBR(this TimeSpan timeSpan)
    {
        return timeSpan.ToString(ptBRFormat);
    }
}