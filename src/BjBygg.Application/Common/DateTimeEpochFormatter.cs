using AutoMapper;
using CleanArchitecture.Core;
using System;

namespace BjBygg.Application.Common
{
    public class DateTimeEpochFormatter : ITypeConverter<long, DateTime>
    {
        public DateTime Convert(long source, DateTime destination, ResolutionContext context)
            => DateTimeHelper.ConvertEpochToDate(source);
    }
}
