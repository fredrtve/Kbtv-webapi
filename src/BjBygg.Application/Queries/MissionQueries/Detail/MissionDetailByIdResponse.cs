using BjBygg.Application.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Queries.MissionQueries.Detail
{
    public class MissionDetailByIdResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public List<MissionImageDto> MissionImages { get; set; }
        public List<MissionNoteDto> MissionNotes { get; set; }
        public MissionEmployerDto Employer { get; set; }
        public string MissionTypeName { get; set; }
    }


}
