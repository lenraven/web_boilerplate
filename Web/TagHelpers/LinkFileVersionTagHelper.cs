using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;

using Web.Extensions.ParameterValidation;
using Web.Services;

namespace Web.TagHelpers;

[HtmlTargetElement("link", Attributes = HrefName)]
public class LinkFileVersionTagHelper : TagHelper
{
    private const string HrefName = "versioned-href";

    private readonly ManifestRevFileVersionProvider _provider;

    public LinkFileVersionTagHelper(ManifestRevFileVersionProvider provider)
    {
        _provider = provider.NotNull();
    }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        base.Process(context, output);
        if(!context.AllAttributes.TryGetAttribute(HrefName, out var src)) return;
        var link = (HtmlString)src.Value;
        output.Attributes.Remove(src);
        output.Attributes.Add(new TagHelperAttribute("href", _provider.GetVersionedFile(link.Value!)));
    }
}
