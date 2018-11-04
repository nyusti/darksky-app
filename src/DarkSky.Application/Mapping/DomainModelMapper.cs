namespace DarkSky.Application.Mapping
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;

    /// <summary>
    /// Domain model mapper
    /// </summary>
    /// <seealso cref="DarkSky.Application.Mapping.IDomainModelMapper"/>
    public class DomainModelMapper : IDomainModelMapper
    {
        private readonly Lazy<IMapper> internalMapperFactory;

        public DomainModelMapper()
        {
            this.internalMapperFactory = new Lazy<IMapper>(MapperFactory);
        }

        /// <inheritdoc/>
        public TDestination Map<TDestination>(object source)
        {
            if (source is Client.Models.Forecast forecast
                && typeof(TDestination) == typeof(Domain.Model.CurrentWeather))
            {
                var mapped = this.internalMapperFactory.Value.Map<TDestination>(forecast.Currently);
                if (mapped is Domain.Model.CurrentWeather domain)
                {
                    domain.DailySummary = forecast.Daily.Summary;
                    domain.Daily = this.internalMapperFactory.Value.Map<List<Domain.Model.Forecast>>(forecast.Daily.Data);
                }

                return mapped;
            }

            return this.internalMapperFactory.Value.Map<TDestination>(source);
        }

        private IMapper MapperFactory()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Client.Models.DataPoint, Domain.Model.Forecast>();

                cfg.CreateMap<Client.Models.DataPoint, Domain.Model.CurrentWeather>()
                    .ForMember(p => p.Daily, c =>
                    {
                        c.Ignore();
                    });
            });

            return config.CreateMapper();
        }
    }
}