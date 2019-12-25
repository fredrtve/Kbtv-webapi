using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace BjBygg.Application.Commands.UserCommands.Create
{
    public class CreateUserResponse
    {
        public CreateUserResponse() {}

        public CreateUserResponse(bool succeeded, string error)
        {
            Succeeded = succeeded;
            Errors = new List<string>();
            Errors.Add(error);
        }

        public CreateUserResponse(bool succeeded, List<string> errors)
        {
            Succeeded = succeeded;
            Errors = errors;
        }
        public bool Succeeded { get; set; }
        public List<string> Errors { get; set; }
    }
}
