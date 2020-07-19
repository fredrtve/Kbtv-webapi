using BjBygg.Application.Application.Common.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace BjBygg.Application.Application
{
    public class CsvConverter : ICsvConverter
    {
        public string ConvertJsonListToCsv(List<JsonElement> jsonObjects, Dictionary<string, string> propertyMap, string delimiter = ",")
        {
            StringBuilder csv = new StringBuilder();

            string header = "";
            for (var i = 0; i < propertyMap.Count; i++)
            {
                var propertyValue = propertyMap.ElementAt(i).Value;
                if (i == 0) header = propertyValue; //Initial value no delimiter
                else header = string.Concat(header, delimiter, propertyValue);
            }
            csv.AppendLine(header);

            foreach (var json in jsonObjects)
            {
                var line = "";
                for (var i = 0; i < propertyMap.Count; i++)
                {
                    var propertyKey = propertyMap.ElementAt(i).Key;
                    JsonElement property;
                    if (json.ValueKind == JsonValueKind.Undefined || !json.TryGetProperty(propertyKey, out property)) continue;
                    if (i == 0) line = property.GetRawText(); //Initial value no delimiter
                    else line = string.Concat(line, delimiter, property.GetRawText());
                };
                csv.AppendLine(line);
            }

            return csv.ToString();
        }

        //public string ConvertObjectsToCsv<T>(List<T> objects)
        //{
        //    var sb = new StringBuilder();
        //    var header = "";
        //    var info = typeof(T).GetProperties();

        //    foreach (var prop in typeof(T).GetProperties())
        //    {
        //        header += prop.Name + "; ";
        //    }

        //    header = header.Substring(0, header.Length - 2);
        //    sb.AppendLine(header);

        //    foreach (var obj in objects)
        //    {
        //        sb = new StringBuilder();
        //        var line = "";
        //        foreach (var prop in info)
        //        {
        //            line += prop.GetValue(obj, null) + "; ";
        //        }
        //        line = line.Substring(0, line.Length - 2);
        //        sb.AppendLine(line);
        //    }
        //    return sb.ToString();
        //}
    }
}
