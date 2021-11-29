using Microsoft.Extensions.DependencyInjection.Extensions;

using Web.Extensions.ParameterValidation;
using Web.Services;

namespace Web.DependencyInjection;

public static class Manifest
{
    public static void AddAssetManifest(this IServiceCollection collection, string fileName)
    {
        collection.TryAddSingleton(new ManifestMapOptions()
        {
            FileName = fileName.NotNull()
        });
        collection.TryAddSingleton<ManifestMapCache>();
        collection.TryAddSingleton<ManifestRevFileVersionProvider>();
    }
}
