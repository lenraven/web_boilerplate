using Web.Extensions.ParameterValidation;

namespace Web.Services;

public class ManifestRevFileVersionProvider
{
    private readonly ManifestMapCache _cache;

    public ManifestRevFileVersionProvider(ManifestMapCache cache)
    {
        _cache = cache.NotNull();
    }

    public string GetVersionedFile(string filePath)
    {
        filePath.NotNull();
        if (IsAbsolutePath(filePath)) return filePath;
        return _cache.GetManifestMap()[filePath];
    }

    private bool IsAbsolutePath(string filePath)
    {
        return Uri.TryCreate(filePath, UriKind.Absolute, out Uri? uri) && !uri.IsFile;
    }


}
