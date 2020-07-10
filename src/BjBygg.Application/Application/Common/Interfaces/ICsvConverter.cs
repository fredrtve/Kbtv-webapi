using System.Collections.Generic;
using System.Text.Json;

namespace BjBygg.Application.Application.Common.Interfaces
{
    public interface ICsvConverter
    {
        string ConvertJsonListToCsv(List<JsonElement> jsonObjects, Dictionary<string, string> propertyMap, string delimiter = ",");
    }
}
