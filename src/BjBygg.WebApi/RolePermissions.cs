using BjBygg.Application.Common;

namespace BjBygg.WebApi
{
    public static class RolePermissions
    {
        public static class DocumentTypeActions
        {
            public const string Create = Roles.Leader;
            public const string Delete = Roles.Leader;
            public const string Update = Roles.Leader;
        }
        public static class EmployerActions
        {
            public const string Create = Roles.Leader + "," + Roles.Management;
            public const string Delete = Roles.Leader;
            public const string Update = Roles.Leader;
        }
        public static class ExportJsonToCsvActions
        {
            public const string Export = Roles.Leader;
        }
        public static class InboundEmailPasswordActions
        {
            public const string Create = Roles.Leader;
            public const string Delete = Roles.Leader;
            public const string Read = Roles.Leader;
        }
        public static class MissionDocumentActions
        {
            public const string Create = Roles.Leader;
            public const string Delete = Roles.Leader;
            public const string SendEmail = Roles.Leader;
        }
        public static class MissionImageActions
        {
            public const string Create = Roles.Leader + "," + Roles.Management + "," + Roles.Employee;
            public const string Delete = Roles.Leader;
            public const string SendEmail = Roles.Leader;
        }
        public static class MissionNoteActions
        {
            public const string Create = Roles.Leader + "," + Roles.Management + "," + Roles.Employee;
            public const string Delete = Roles.Leader;
            public const string Update = Roles.Leader;
        }
        public static class MissionActions
        {
            public const string Create = Roles.Leader + "," + Roles.Management;
            public const string CreateFromPdf = Roles.Leader;
            public const string Delete = Roles.Leader;
            public const string Update = Roles.Leader;
        }
        public static class MissionTypeActions
        {
            public const string Create = Roles.Leader;
            public const string Delete = Roles.Leader;
            public const string Update = Roles.Leader;
        }
        public static class TimesheetActions
        {
            public const string Read = Roles.Leader;
            public const string Update = Roles.Leader;
        }
        public static class UserActions
        {
            public const string Create = Roles.Leader;
            public const string Delete = Roles.Leader;
            public const string Update = Roles.Leader;
            public const string Read = Roles.Leader;
        }
        public static class UserTimesheetActions
        {
            public const string Create = Roles.Leader + "," + Roles.Management + "," + Roles.Employee;
            public const string Delete = Roles.Leader + "," + Roles.Management + "," + Roles.Employee;
            public const string Update = Roles.Leader + "," + Roles.Management + "," + Roles.Employee;
            public const string Read = Roles.Leader + "," + Roles.Management + "," + Roles.Employee;
        }
    }
}

