namespace OmniTracker.Extensions;

public static class StringExtension
{
    public static bool IsNullOrEmpty(this string value) => string.IsNullOrEmpty(value);
    public static bool IsNotNullOrEmpty(this string value) => !string.IsNullOrEmpty(value);
}
