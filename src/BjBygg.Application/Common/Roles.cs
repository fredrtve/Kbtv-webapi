using System.Collections.Generic;

namespace BjBygg.Application.Common
{
    public static class Roles
    {
        public const string Leader = "leder";
        public const string Management = "mellomleder";
        public const string Employee = "ansatt";
        public const string Employer = "oppdragsgiver";

        public static readonly List<string> All = new List<string>() { Leader, Management, Employee, Employer };
    }
}
