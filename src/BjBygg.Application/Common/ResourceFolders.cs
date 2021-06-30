namespace BjBygg.Application.Common
{
    public class ResourceFolders
    {
        public string BaseUrl { get; set; }
        public string MissionImage { get; set; }
        public string OriginalMissionImage { get; set; }
        public string Document { get; set; }
        public string MissionHeader { get; set; }

        public string GetUrl(string folder, string fileName)
        {
            return $"{BaseUrl}/{folder}/{fileName}";
        }
    }
}
