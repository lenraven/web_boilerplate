using Microsoft.AspNetCore.Mvc;

using Web.Services;

namespace Web.Extensions;

public static class UrlHelperExtensions
{
    public static string StaticContentVersioned(this IUrlHelper urlHelper, string contentPath)
    {
        var manifestRevFileVersionProvider = (ManifestRevFileVersionProvider)urlHelper.ActionContext.HttpContext.RequestServices.GetService(typeof(ManifestRevFileVersionProvider))!;
        return manifestRevFileVersionProvider.GetVersionedFile(contentPath);
    }
}
