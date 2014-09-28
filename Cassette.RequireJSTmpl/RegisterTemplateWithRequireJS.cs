using System;
using System.IO;
using Cassette.Utilities;

namespace Cassette.HtmlTemplates
{
    public class RegisterTemplateWithRequireJS : IAssetTransformer
    {
        public delegate RegisterTemplateWithRequireJS Factory(HtmlTemplateBundle bundle, IJsonSerializer serializer);

        readonly HtmlTemplateBundle bundle;

        readonly IJsonSerializer serializer;

        public RegisterTemplateWithRequireJS(HtmlTemplateBundle bundle, IJsonSerializer serializer)
        {
            this.bundle = bundle;
            this.serializer = serializer;
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
                path,
                serializer.Serialize(source)
            );
        }
    }
}