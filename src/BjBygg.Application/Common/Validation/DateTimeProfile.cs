using AutoMapper;
using System;

namespace BjBygg.Application.Common
{
    public class DateTimeProfile : Profile
    {
        public DateTimeProfile()
        {
            CreateMap<DateTime, long>().ConvertUsing(new DateTimeToEpochMillisecondsFormatter());

            CreateMap<long, DateTime>().ConvertUsing(new EpochMillisecondsToDateTimeFormatter());
        }
    }
}
