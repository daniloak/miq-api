namespace Omini.Miq.Shared.Formatters;

public static class Formatters
{
    public static string GetDigits(this string value)
    {
        return string.Concat(value.Where(Char.IsDigit));
    }
}