using System.Collections.Immutable;

using Web.Extensions.ParameterValidation;

namespace Web.Services;

public sealed class ManifestMap
{
    public static ManifestMap Empty { get; } = new(new Dictionary<string, string>());

    private readonly IDictionary<string, string> _map;

    public ManifestMap(IDictionary<string, string> map)
    {
        _map = map.NotNull().ToImmutableDictionary();
    }

    public string this[string manifestName] => GetValue(manifestName);

    private string GetValue(string manifestName)
    {
        var normalizedManifestName= NormalizeKey(manifestName);
        return _map.TryGetValue(normalizedManifestName, out var versionedName) ? versionedName : NormalizeManifestPath(manifestName);
    }

    private string NormalizeKey(string manifestName)
    {
        var lastFragment = manifestName;
        if (lastFragment.StartsWith("~"))
        {
            lastFragment = lastFragment[1..];
        }

        var lastSplashIndex = lastFragment.LastIndexOf('/');
        if (lastSplashIndex >= 0)
        {
            lastFragment = lastFragment[(lastSplashIndex + 1)..];
        }

        return lastFragment;
    }

    private string NormalizeManifestPath(string manifestName)
    {
        return manifestName.StartsWith("~") ? manifestName[1..] : manifestName;
    }
}
