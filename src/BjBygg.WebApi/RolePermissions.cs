using BjBygg.Application.Common;

namespace BjBygg.WebApi
{
    public static class RolePermissions
    {
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
            public const string UpdateHeaderImage = Roles.Leader;
        }
        public static class MissionTypeActions
        {
            public const string Create = Roles.Leader;
            public const string Delete = Roles.Leader;
            public const string Update = Roles.Leader;
        }
        public static class TimesheetActions
        {
            public const string ReadTimesheets = Roles.Leader;
            public const string ReadUserTimesheets = Roles.Leader + "," + Roles.Management + "," + Roles.Employee;
            public const string UpdateStatus = Roles.Leader;
            public const string Create = Roles.Leader + "," + Roles.Management + "," + Roles.Employee;
            public const string Delete = Roles.Leader + "," + Roles.Management + "," + Roles.Employee;
            public const string Update = Roles.Leader + "," + Roles.Management + "," + Roles.Employee;
        }
        public static class UserActions
        {
            public const string Create = Roles.Leader;
            public const string Delete = Roles.Leader;
            public const string Update = Roles.Leader;
            public const string Read = Roles.Leader;
        }
    }
}

