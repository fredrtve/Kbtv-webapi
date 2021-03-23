using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BjBygg.WebApi.Dto
{
    public class UploadMissionImageFormDto
    {
        [FromForm]
        public string Id { get; set; }
        [FromForm]
        public IFormFile File { get; set; }
        [FromForm]
        public string MissionId { get; set; }
    }
}
