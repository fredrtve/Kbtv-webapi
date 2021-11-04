using BjBygg.Core.Interfaces;
using BjBygg.SharedKernel;
using System.Collections.Generic;
using System.Globalization;

namespace BjBygg.Core.Entities
{
    public class Mission : BaseEntity, IFile, IContactable, IAddress
    {
        public Mission() { }

        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Description { get; set; }
        public string? FileName { get; set; }
        public bool Finished { get; set; }
        public string Address
        {
            get { return this._address; }
            set { _address = new CultureInfo("nb-NO", false).TextInfo.ToTitleCase(value); }
        }

        private string _address;

        public List<MissionImage> MissionImages { get; set; }
        public List<MissionDocument> MissionDocuments { get; set; }
        public List<MissionNote> MissionNotes { get; set; }
        public List<MissionActivity> MissionActivities { get; set; }
        public MissionType MissionType { get; set; }
        public string? MissionTypeId { get; set; }
        public Employer Employer { get; set; }
        public string? EmployerId { get; set; }

        public Position Position { get; set; }
    }
}
