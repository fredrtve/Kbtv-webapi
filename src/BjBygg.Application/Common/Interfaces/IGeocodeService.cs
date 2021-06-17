using BjBygg.Core.Entities;
using System.Threading.Tasks;

namespace BjBygg.Application.Common.Interfaces
{
    public interface IGeocodeService
    {
        public Task<Position> GetPositionAsync(string address);
    }
}
