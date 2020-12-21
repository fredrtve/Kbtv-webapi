namespace CleanArchitecture.Core
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
    }
}
