using System.Collections.Generic;
using Cassette.BundleProcessing;
using Cassette.TinyIoC;

namespace Cassette.HtmlTemplates
{
    public class RequireJSTmplPipeline : BundlePipeline<HtmlTemplateBundle>
    {
        public RequireJSTmplPipeline(TinyIoCContainer container)
            : base(container)
        {
            var renderer = container.Resolve<RemoteHtmlTemplateBundleRenderer>();
            AddRange(new IBundleProcessor<HtmlTemplateBundle>[]
            {
                container.Resolve<AssignHtmlTemplateRenderer.Factory>()(renderer),
                new AssignContentType("text/javascript"),
                new ParseHtmlTemplateReferences(),
                container.Resolve<RegisterTemplatesWithRequireJS>(),
                new AssignHash(),
                new ConcatenateAssets()
            });
        }
    }
}
