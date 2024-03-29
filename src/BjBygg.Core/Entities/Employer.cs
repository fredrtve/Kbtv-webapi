using BjBygg.SharedKernel;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace BjBygg.Core.Entities
{
    public class Employer : BaseEntity, IContactable, IAddress, IName
    {
        public Employer() { }

        public string Name { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }

        public string? Address
        {
            get { return this._address; }
            set
            {
                if (!String.IsNullOrEmpty(value))
                    _address = new CultureInfo("nb-NO", false).TextInfo.ToTitleCase(value);
            }
        }

        private string? _address;
        public List<Mission> Missions { get; set; }
        public List<EmployerUser> EmployerUsers { get; set; }
    }
}
