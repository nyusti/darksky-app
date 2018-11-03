namespace DarkSky.Application.Domain.Model
{
    public partial class Location
    {
        public string City { get; set; }

        public string CountryCode { get; set; }

        public double Latitude { get; set; }

        public double Longiture { get; set; }
    }
}