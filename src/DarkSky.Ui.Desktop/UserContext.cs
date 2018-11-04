namespace DarkSky.Ui.Desktop
{
    using System;
    using System.Collections.Generic;
    using DarkSky.Application.Domain.Model;

    /// <summary>
    /// Users contextual informations
    /// </summary>
    public class UserContext
    {
        /// <summary>
        /// Gets the last exceptions.
        /// </summary>
        /// <value>The last exceptions.</value>
        public List<Exception> LastExceptions { get; } = new List<Exception>();

        /// <summary>
        /// Gets or sets the selected language. Defaulted to English.
        /// </summary>
        /// <value>The selected language.</value>
        public Languages SelectedLanguage { get; set; } = Languages.En;
    }
}