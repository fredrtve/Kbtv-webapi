using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace CleanArchitecture.IntegrationTests
{
    public static class ContentHelper
    {
        public static StringContent GetStringContent(object obj)
            => new StringContent(JsonConvert.SerializeObject(obj), Encoding.Default, "application/json");

        public static T GetObjectFromStringContent<T>(string obj)
            => JsonConvert.DeserializeObject<T>(obj);
    }
}
