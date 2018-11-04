namespace DarkSky.Ui.Desktop.Localization
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using DarkSky.Application.Domain.Model;

    /// <summary>
    /// Language service interface
    /// </summary>
    public interface ILanguageService
    {
        /// <summary>
        /// Changes the language.
        /// </summary>
        /// <param name="language">The language.</param>
        void ChangeLanguage(Language language);

        /// <summary>
        /// Gets the supported languages asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The supported language list.</returns>
        Task<List<Language>> GetSupportedLanguagesAsync(CancellationToken cancellationToken);
    }
}