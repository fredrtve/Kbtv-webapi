using System;
using System.Collections.Generic;
using System.Text;

namespace Application.IntegrationTests
{
    public class TestSeederConfig
    {
        public TestSeederConfig(){}
        public TestSeederConfig(
            int _missions, 
            int _missionTypes, 
            int _missionNotes, 
            int _missionDocuments, 
            int _missionImages, 
            int _employers, 
            int _timesheets, 
            int _documenttypes)
        {
            Missions = _missions;
            MissionTypes = _missionTypes;
            MissionNotes = _missionNotes;
            MissionDocuments = _missionDocuments;
            MissionImages = _missionImages;
            Employers = _employers;
            Timesheets = _timesheets;
            Documenttypes = _documenttypes;
        }

        public int Missions { get; set; } = 5;
        public int MissionTypes { get; set; } = 5;
        public int MissionNotes { get; set; } = 5;
        public int MissionDocuments { get; set; } = 5;
        public int MissionImages { get; set; } = 5;
        public int Employers { get; set; } = 5;
        public int Timesheets { get; set; } = 5;
        public int Documenttypes { get; } = 5;
        public int DocumentTypes { get; set; } = 5;

    }
}
