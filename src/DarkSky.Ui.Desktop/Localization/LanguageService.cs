namespace DarkSky.Ui.Desktop.Localization
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading;
    using System.Threading.Tasks;
    using DarkSky.Application.Domain.Model;
    using Gu.Localization;

    /// <summary>
    /// Language service
    /// </summary>
    /// <seealso cref="DarkSky.Ui.Desktop.Localization.ILanguageService"/>
    public class LanguageService : ILanguageService
    {
        private static readonly List<Language> supportedLanguages = new List<Language>
        {
            new Language { Name = "English", LanguageTag ="en-US", ShortCode = Languages.En },
            new Language { Name = "Magyar", LanguageTag = "hu-HU", ShortCode = Languages.Hu }
        };

        /// <inheritdoc/>
        public void ChangeLanguage(Language language)
        {
            if (language == null)
            {
                throw new ArgumentNullException(nameof(language));
            }

            ApplicationContext.UserContext.SelectedLanguage = language.ShortCode;
            var cultureInfo = new CultureInfo(language.LanguageTag);
            CultureInfo.CurrentUICulture = cultureInfo;
            Translator.Culture = Translator.ContainsCulture(cultureInfo) ? cultureInfo : null;
        }

        /// <inheritdoc/>
        public Task<List<Language>> GetSupportedLanguagesAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(supportedLanguages);
        }
    }
}