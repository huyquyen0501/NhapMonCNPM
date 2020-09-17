using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace NhapMonCNPM.Localization
{
    public static class NhapMonCNPMLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(NhapMonCNPMConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(NhapMonCNPMLocalizationConfigurer).GetAssembly(),
                        "NhapMonCNPM.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
