using System.Collections.Generic;
using System.Text.Json;

namespace CleanArchitecture.Core.Interfaces.Services
{
    public interface ICsvConverter
    {
        string ConvertJsonListToCsv(List<JsonElement> jsonObjects, Dictionary<string, string> propertyMap, string delimiter = ",");
    }
}
