using System.Collections.Generic;

namespace BjBygg.Core
{
    public static class ValidationRules
    {
        public const int PhoneNumberMinLength = 4;
        public const int PhoneNumberMaxLength = 12;

        public const int NameMaxLength = 45;

        public const int AddressMaxLength = 100;

        public const int FileNameMaxLength = 40;

        public const int MissionDescriptionMaxLength = 400;

        public const int MissionNoteTitleMaxLength = 75;

        public const int MissionNoteContentMaxLength = 400;

        public const int TimesheetCommentMaxLength = 400;

        public const int UserPasswordMinLength = 7;
        public const int UserPasswordMaxLength = 100;

        public static readonly HashSet<string> ImageFileExtensions = new HashSet<string> {
            ".jpg", ".jpeg", ".png"
        };

        public static readonly HashSet<string> DocumentFileExtensions = new HashSet<string> {
            ".doc",".docm", ".docx", ".txt",
            ".pdf", ".dot", ".csv", ".dotm",
            ".dotx", ".xla", ".odt", ".xlam",
            ".xls", ".xlsb", ".xlsm", ".xlsx",
            ".xlt", ".xlw"
        };
    }
}
