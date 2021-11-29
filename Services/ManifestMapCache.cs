using Microsoft.Extensions.Caching.Memory;

using Web.Extensions.ParameterValidation;

namespace Web.Services;

public sealed class ManifestMapCache
{
    private const string CacheKey = "manifestData";

    private readonly string _fileName; 
    private readonly IWebHostEnvironment _hostingEnvironment;
    private readonly IMemoryCache _cache;

    public ManifestMapCache(IMemoryCache cache, IWebHostEnvironment hostingEnvironment, ManifestMapOptions options)
    {
        _cache = cache.NotNull();
        _hostingEnvironment = hostingEnvironment.NotNull();
        _fileName = options.NotNull().FileName.NotNull();
    }

    public ManifestMap GetManifestMap()
    {
        if (_cache.TryGetValue(CacheKey, out ManifestMap map)) return map;
        
        var cacheEntryOptions = new MemoryCacheEntryOptions();
        cacheEntryOptions.AddExpirationToken(_hostingEnvironment.WebRootFileProvider.Watch(_fileName));
        map = ReadManifestData();
        return _cache.Set(CacheKey, map, cacheEntryOptions);
    }

    private ManifestMap ReadManifestData()
    {
        var fileInfo = _hostingEnvironment.WebRootFileProvider.GetFileInfo(_fileName);
        if (!fileInfo.Exists) return ManifestMap.Empty;
        var content = File.ReadAllText(fileInfo.PhysicalPath);
        var map = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(content);
        return map is null ? ManifestMap.Empty : new ManifestMap(map);
    }
}
