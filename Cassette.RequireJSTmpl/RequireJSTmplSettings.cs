using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cassette.HtmlTemplates
{
    public class RequireJSTmplSettings
    {
        public string PluginPrefix { get; set; }

        public RequireJSTmplSettings(IEnumerable<IConfiguration<RequireJSTmplSettings>> configurations)
        {
            this.PluginPrefix = string.Empty;
            ApplyConfigurations(configurations);
        }

        void ApplyConfigurations(IEnumerable<IConfiguration<RequireJSTmplSettings>> configurations)
        {
            configurations.OrderByConfigurationOrderAttribute().Configure(this);
        }
    }
}
