using AutoMapper;
using CleanArchitecture.Core;
using System;

namespace BjBygg.Application.Common
{
    public class EpochDateTimeFormatter : ITypeConverter<DateTime, long>
    {
        public long Convert(DateTime source, long destination, ResolutionContext context)
        {
            return DateTimeHelper.ConvertDateToEpoch(source); 
        }
    }

}
