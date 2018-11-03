namespace DarkSky.Application.Domain.Model
{
    public partial class Language
    {
        public string LanguageTag { get; set; }

        public string Name { get; set; }

        public Languages ShortCode { get; set; }
    }
}