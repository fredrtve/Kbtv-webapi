using BjBygg.Application.Common.Interfaces;
using BjBygg.Core.Entities;
using BjBygg.Infrastructure.Api.GoogleGeocode;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace BjBygg.Infrastructure.Api
{
    public class GoogleGeocodeService : IGeocodeService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;

        public GoogleGeocodeService(
            IHttpClientFactory clientFactory,
            IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
        }

        public async Task<Position> GetPositionAsync(string address)
        {
            var client = _clientFactory.CreateClient();

            var response = await client.GetAsync(BuildUri(address));

            if (!response.IsSuccessStatusCode) return null;
 
            var geocodeResponse = await ReadGeocodeResponseAsync(response);

            if (geocodeResponse.status != "OK" || geocodeResponse.results.Length == 0) return null;

            var result = geocodeResponse.results[0];

            var position = new Position()
            {
                Latitude = Convert.ToDouble(result.geometry.location.lat),
                Longitude = Convert.ToDouble(result.geometry.location.lng),
                IsExact = result.types.Length != 0 && result.types[0] == "street_address"
            };

            return position;           
       }

        private async Task<GoogleGeoCodeResponse> ReadGeocodeResponseAsync(HttpResponseMessage response)
        {
            var contentStream = await response.Content.ReadAsStreamAsync();
            using var streamReader = new StreamReader(contentStream);
            using var jsonReader = new JsonTextReader(streamReader);
            return new JsonSerializer().Deserialize<GoogleGeoCodeResponse>(jsonReader);
        }

        private Uri BuildUri(string address)
        {
            var builder = new UriBuilder("https://maps.googleapis.com/maps/api/geocode/json");
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["key"] = _configuration.GetValue<string>("GoogleMapsApiKey");
            query["address"] = address;
            query["region"] = "no";
            builder.Query = query.ToString();
            return builder.Uri;
        }
    }
}
