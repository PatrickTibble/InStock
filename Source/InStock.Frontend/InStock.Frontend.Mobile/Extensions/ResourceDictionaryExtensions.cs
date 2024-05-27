namespace InStock.Frontend.Mobile.Extensions;

public static class ResourceDictionaryExtensions
{
    public static T GetValueOrDefault<T>(this ResourceDictionary? dictionary, string key, T defaultValue)
    {
        if (dictionary?.TryGetValue(key, out var value) ?? false)
        {
            return (T)value;
        }

        return defaultValue;
    }
}
