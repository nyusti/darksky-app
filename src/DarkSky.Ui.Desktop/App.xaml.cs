﻿using System.Windows;
using DarkSky.Client;

namespace DarkSky.Ui.Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var secretKeyCredentials = new SecretKeyCredentials("aae91878222aef9a2fbaddcbf97edc42");
            var client = new DarkSkyClient(secretKeyCredentials);

            var res = client.ForecastOperations.GetForecastAsync(40, 30).ConfigureAwait(false).GetAwaiter().GetResult();

            base.OnStartup(e);
        }
    }
}