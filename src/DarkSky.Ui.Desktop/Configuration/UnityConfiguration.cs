﻿namespace DarkSky.Ui.Desktop.Configuration
{
    using System;
    using System.Linq;
    using System.Threading;
    using CommonServiceLocator;
    using DarkSky.Application.Domain.Services;
    using DarkSky.Application.Injection;
    using DarkSky.Client;
    using DarkSky.Ui.Desktop.ServiceClient;
    using GalaSoft.MvvmLight;
    using Microsoft.Practices.Unity.Configuration;
    using Unity;
    using Unity.Injection;
    using Unity.Lifetime;
    using Unity.RegistrationByConvention;
    using Unity.ServiceLocation;

    /// <summary>
    /// Unity contaienr configuration
    /// </summary>
    public static class UnityConfiguration
    {
        private static readonly Lazy<IUnityContainer> containerFactory = new Lazy<IUnityContainer>(GetConfiguredContainer);

        /// <summary>
        /// Gets the configured container
        /// </summary>
        public static IUnityContainer Container => containerFactory.Value;

        private static IUnityContainer GetConfiguredContainer()
        {
            // configure singleton container
            var container = new UnityContainer();
            DesignTimeConfiguration(container);
            RuntimeConfiguration(container);

            // configure service locator
            var unityServiceLocator = new UnityServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => unityServiceLocator);

            return container;
        }

        private static void RuntimeConfiguration(IUnityContainer container)
        {
            if (UnityConfigurationSection.CurrentSection != null)
            {
                container.LoadConfiguration(UnityConfigurationSection.CurrentSection);
            }
        }

        private static void DesignTimeConfiguration(IUnityContainer container)
        {
            // register services
            container
                .RegisterType<IDependencyResolver, UnityHierarchicalDependencyResolver>(new ContainerControlledLifetimeManager())
                .RegisterType<IDarkSkyClient, ConfiguredDarkSkyClient>(new HierarchicalLifetimeManager())
                .RegisterType<ILocationService, OfflineLocationService>(new HierarchicalLifetimeManager())
                .RegisterType<IForecastService, DarkSkyForecastService>(new HierarchicalLifetimeManager())
                .RegisterType<CancellationTokenSource>(new HierarchicalLifetimeManager(), new InjectionFactory(c => CancellationTokenSource.CreateLinkedTokenSource(ApplicationContext.MainCancellationTokenSource.Token)));

            // register all viewmodels as hierarchical
            container.RegisterTypes(
                AllClasses.FromLoadedAssemblies().Where(p => p.IsSubclassOf(typeof(ViewModelBase))),
                WithMappings.None,
                WithName.Default,
                WithLifetime.Hierarchical);
        }
    }
}