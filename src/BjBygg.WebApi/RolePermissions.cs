using BjBygg.Application.Common;

namespace BjBygg.WebApi
{
    public static class RolePermissions
    {
        public static class EmployerActions
        {
            public const string Create = Roles.Admin + "," + Roles.Leader + "," + Roles.Management;
            public const string Delete = Roles.Admin + "," + Roles.Leader;
            public const string Update = Roles.Admin + "," + Roles.Leader;
        }
        public static class ExportJsonToCsvActions
        {
            public const string Export = Roles.Admin + "," + Roles.Leader;
        }
        public static class InboundEmailPasswordActions
        {
            public const string Create = Roles.Admin + "," + Roles.Leader;
            public const string Delete = Roles.Admin + "," + Roles.Leader;
            public const string Read = Roles.Admin + "," + Roles.Leader;
        }
        public static class MissionDocumentActions
        {
            public const string Create = Roles.Admin + "," + Roles.Leader;
            public const string Delete = Roles.Admin + "," + Roles.Leader;
            public const string SendEmail = Roles.Admin + "," + Roles.Leader;
        }
        public static class MissionImageActions
        {
            public const string Create = Roles.Admin + "," + Roles.Leader + "," + Roles.Management + "," + Roles.Employee;
            public const string Delete = Roles.Admin + "," + Roles.Leader;
            public const string SendEmail = Roles.Admin + "," + Roles.Leader;
        }
        public static class MissionNoteActions
        {
            public const string Create = Roles.Admin + "," + Roles.Leader + "," + Roles.Management + "," + Roles.Employee;
            public const string Delete = Roles.Admin + "," + Roles.Leader;
            public const string Update = Roles.Admin + "," + Roles.Leader;
        }
        public static class MissionActions
        {
            public const string Create = Roles.Admin + "," + Roles.Leader + "," + Roles.Management;
            public const string CreateFromPdf = Roles.Admin + "," + Roles.Leader;
            public const string Delete = Roles.Admin + "," + Roles.Leader;
            public const string Update = Roles.Admin + "," + Roles.Leader;
            public const string UpdateHeaderImage = Roles.Admin + "," + Roles.Leader;
        }
        public static class MissionTypeActions
        {
            public const string Create = Roles.Admin + "," + Roles.Leader;
            public const string Delete = Roles.Admin + "," + Roles.Leader;
            public const string Update = Roles.Admin + "," + Roles.Leader;
        }
        public static class TimesheetActions
        {
            public const string ReadTimesheets = Roles.Admin + "," + Roles.Leader;
            public const string ReadUserTimesheets = Roles.Admin + "," + Roles.Leader + "," + Roles.Management + "," + Roles.Employee;
            public const string UpdateStatus = Roles.Admin + "," + Roles.Leader;
            public const string Create = Roles.Admin + "," + Roles.Leader + "," + Roles.Management + "," + Roles.Employee;
            public const string Delete = Roles.Admin + "," + Roles.Leader + "," + Roles.Management + "," + Roles.Employee;
            public const string Update = Roles.Admin + "," + Roles.Leader + "," + Roles.Management + "," + Roles.Employee;
        }
        public static class UserActions
        {
            public const string Create = Roles.Admin + "," + Roles.Leader;
            public const string Delete = Roles.Admin + "," + Roles.Leader;
            public const string Update = Roles.Admin + "," + Roles.Leader;
            public const string Read = Roles.Admin + "," + Roles.Leader;
        }
        public static class LeaderSettingActions
        {
            public const string Update = Roles.Admin + "," + Roles.Leader;
        }
    }
}

