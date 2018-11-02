namespace DarkSky.Application.Mapping
{
    using System;
    using AutoMapper;

    public class ModelMapper
    {
        public ModelMapper()
        {
            this.ForecastDetailsMapper = new Lazy<IMapper>(() =>
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Client.Models.Forecast, Domain.Model.Forecast>();
                });

                return config.CreateMapper();
            });
        }

        public Lazy<IMapper> ForecastDetailsMapper { get; private set; }
    }
}