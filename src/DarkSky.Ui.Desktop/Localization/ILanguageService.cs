namespace DarkSky.Ui.Desktop.Localization
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using DarkSky.Application.Domain.Model;

    public interface ILanguageService
    {
        void ChangeLanguage(Language language);

        Task<List<Language>> GetSupportedLanguagesAsync(CancellationToken cancellationToken);
    }
}