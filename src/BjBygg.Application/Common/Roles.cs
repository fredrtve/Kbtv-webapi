using System.Collections.Generic;

namespace BjBygg.Application.Common
{
    public static class Roles
    {
        public const string Leader = "Leder";
        public const string Management = "Mellomleder";
        public const string Employee = "Ansatt";
        public const string Employer = "Oppdragsgiver";
        public const string Admin = "Admin";

        public static readonly List<string> All = new List<string>() { Leader, Management, Employee, Employer, Admin };
    }
}
