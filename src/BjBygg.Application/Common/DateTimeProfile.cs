using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Common
{
    public class DateTimeProfile : Profile
    {
        public DateTimeProfile()
        {
            CreateMap<DateTime, long>().ConvertUsing(new EpochDateTimeFormatter());

            CreateMap<long, DateTime>().ConvertUsing(new DateTimeEpochFormatter());
        }
    }
}
