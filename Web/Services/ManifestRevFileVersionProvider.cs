using Microsoft.Extensions.Caching.Memory;

namespace Web.Services;

public class ManifestRevFileVersionProvider
{
    private readonly IWebHostEnvironment _hostingEnvironment;
    private readonly IMemoryCache _cache;

    public ManifestRevFileVersionProvider(
        IWebHostEnvironment hostingEnvironment,
        IMemoryCache cache)
    {
        _hostingEnvironment = hostingEnvironment ?? throw new ArgumentNullException(nameof(hostingEnvironment));
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
    }

    public string GetVersionedFile(string filePath)
    {
        if (filePath == null)
        {
            throw new ArgumentNullException(nameof(filePath));
        }

        if (Uri.TryCreate(filePath, UriKind.Absolute, out Uri? uri) && !uri.IsFile)
        {
            // Don't append version if the path is absolute.
            return filePath;
        }

        var lastFragment = filePath;
        if (lastFragment.StartsWith("~"))
        {
            lastFragment = lastFragment[1..];
        }

        if (lastFragment.LastIndexOf('/') > -1)
        {
            lastFragment = lastFragment[(lastFragment.LastIndexOf('/') + 1)..];
        }
        string? mappedFilePath = GetMapping(lastFragment);
        if (mappedFilePath != null)
        {
            filePath = mappedFilePath;
        }
        else
        {
            if (filePath.StartsWith("~"))
            {
                filePath = filePath[1..];
            }
        }

        return filePath;
    }

    private string? GetMapping(string mapping)
    {
        const string manifestRevPath = "manifest-rev.json";
        const string cacheKey = "manifestRev";
        if (_cache.TryGetValue(cacheKey, out IConfiguration mappings)) return mappings[mapping];

        var cacheEntryOptions = new MemoryCacheEntryOptions();
        cacheEntryOptions.AddExpirationToken(_hostingEnvironment.WebRootFileProvider.Watch(manifestRevPath));
        var fileInfo = _hostingEnvironment.WebRootFileProvider.GetFileInfo(manifestRevPath);

        if (fileInfo.Exists)
        {
            cacheEntryOptions.AddExpirationToken(_hostingEnvironment.WebRootFileProvider.Watch(manifestRevPath));
            var builder = new ConfigurationBuilder()
                .AddJsonFile(_hostingEnvironment.WebRootFileProvider, manifestRevPath, false, false);

            mappings = builder.Build();
        }
        else
        {
            mappings = new ConfigurationBuilder().Build();
        }

        mappings = _cache.Set(cacheKey, mappings, cacheEntryOptions);

        return mappings[mapping];
    }
}
