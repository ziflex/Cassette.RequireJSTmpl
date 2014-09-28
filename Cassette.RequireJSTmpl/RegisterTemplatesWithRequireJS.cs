using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cassette.BundleProcessing;

namespace Cassette.HtmlTemplates
{
    public class RegisterTemplatesWithRequireJS : AddTransformerToAssets<HtmlTemplateBundle>
    {
        readonly RegisterTemplateWithRequireJS.Factory createAssetTransformer;
        readonly IJsonSerializer serializer;
        readonly RequireJSTmplSettings settings;

        public RegisterTemplatesWithRequireJS(RegisterTemplateWithRequireJS.Factory createAssetTransformer, IJsonSerializer serializer, RequireJSTmplSettings settings)
        {
            this.createAssetTransformer = createAssetTransformer;
            this.serializer = serializer;
            this.settings = settings;
        }

        protected override IAssetTransformer CreateAssetTransformer(HtmlTemplateBundle bundle)
        {
            return createAssetTransformer(bundle, this.serializer, this.settings);
        }
    }
}
