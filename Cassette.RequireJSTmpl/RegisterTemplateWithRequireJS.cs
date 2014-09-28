using System;
using System.IO;
using Cassette.Utilities;

namespace Cassette.HtmlTemplates
{
    public class RegisterTemplateWithRequireJS : IAssetTransformer
    {
        public delegate RegisterTemplateWithRequireJS Factory(HtmlTemplateBundle bundle, IJsonSerializer serializer, RequireJSTmplSettings settings);

        readonly HtmlTemplateBundle bundle;

        readonly IJsonSerializer serializer;

        readonly RequireJSTmplSettings settings;

        public RegisterTemplateWithRequireJS(HtmlTemplateBundle bundle, IJsonSerializer serializer, RequireJSTmplSettings settings)
        {
            this.bundle = bundle;
            this.serializer = serializer;
            this.settings = settings;
        }

        public Func<Stream> Transform(Func<Stream> openSourceStream, IAsset asset)
        {
            return () =>
            {
                var source = openSourceStream().ReadToEnd();
                var output = Module(asset.Path, source);
                return output.AsStream();
            };
        }

        string Module(string path, string source)
        {
            return string.Format(
                "define('{0}', [], function(){{ return {1}; }});",
                NormalizePath(path),
                serializer.Serialize(source)
            );
        }

        string NormalizePath(string path)
        {
            var result = path.Replace("~", string.Empty);

            if (result.StartsWith("/"))
            {
                result = result.Remove(0, 1);
            }

            return string.Format("{0}{1}", this.settings.PluginPrefix, result);
        }
    }
}