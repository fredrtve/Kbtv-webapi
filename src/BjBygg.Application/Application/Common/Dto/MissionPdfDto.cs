using System.IO;

namespace BjBygg.Application.Application.Common.Dto
{
    public class MissionPdfDto
    {
        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public Stream Image { get; set; }

        public string DocumentName { get; set; }
    }
}
