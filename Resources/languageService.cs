using Microsoft.Extensions.Localization;
using System.Reflection;


namespace ClinicProject.Resources
{
    public class languageService
    {
        private readonly IStringLocalizer _stringLocalizer;
        public languageService(IStringLocalizerFactory factory)
        {
            var type = typeof(SharedResources);
            var assmblyname = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            _stringLocalizer = factory.Create("SharedResources", assmblyname.Name);

        }
        public LocalizedString GetLocalizedHTML(string key)
        {
            return _stringLocalizer[key];
        }
    }
}
