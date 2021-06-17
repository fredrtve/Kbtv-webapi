using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Infrastructure.Api.GoogleGeocode
{
    public class GoogleGeoCodeResponse
    {
        public string status { get; set; }
        public GoogleGeocodeResult[] results { get; set; }
    }

    public class GoogleGeocodeResult
    {
        public GoogleGeoCodeGeometry geometry { get; set; }
        public string[] types { get; set; }
    }

    public class GoogleGeoCodeGeometry
    {
        public GoogleGeoCodeLocation location { get; set; }
    }

    public class GoogleGeoCodeLocation
    {
        public string lat { get; set; }
        public string lng { get; set; }
    }
}
