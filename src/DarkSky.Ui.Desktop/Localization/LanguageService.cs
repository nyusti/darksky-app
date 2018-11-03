namespace DarkSky.Ui.Desktop.Localization
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using DarkSky.Application.Domain.Model;
    using GalaSoft.MvvmLight;
    using Gu.Localization;

    public class LanguageService : ViewModelBase, ILanguageService
    {
        private static readonly List<Language> supportedLanguages = new List<Language>
        {
            new Language { Name = "English", LanguageTag ="en-US", ShortCode = Languages.En },
            new Language { Name = "Magyar", LanguageTag = "hu-HU", ShortCode = Languages.Hu }
        };

        public void ChangeLanguage(Language language)
        {
            ApplicationContext.UserContext.SelectedLanguage = language.ShortCode;

            if (Translator.Cultures.Any(p => string.Equals(p.Name, language.LanguageTag, System.StringComparison.OrdinalIgnoreCase)))
            {
                Translator.Culture = new System.Globalization.CultureInfo(language.LanguageTag);
            }
            else
            {
                Translator.Culture = null;
            }
        }

        public Task<List<Language>> GetSupportedLanguagesAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(supportedLanguages);
        }
    }
}