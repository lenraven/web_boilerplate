using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;

using Web.Extensions.ParameterValidation;
using Web.Services;

namespace Web.TagHelpers;
[HtmlTargetElement("script", Attributes = SrcName)]
public class ScriptFileVersionTagHelper : TagHelper
{
    private const string SrcName = "versioned-src";

    private readonly ManifestRevFileVersionProvider _provider;

    public ScriptFileVersionTagHelper(ManifestRevFileVersionProvider provider)
    {
        _provider = provider.NotNull();
    }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        base.Process(context, output);
        if(!context.AllAttributes.TryGetAttribute(SrcName, out var src)) return;
        var link = (HtmlString)src.Value;
        output.Attributes.Remove(src);
        output.Attributes.Add(new TagHelperAttribute("src", _provider.GetVersionedFile(link.Value!)));
    }
}
