using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication2.Model;

namespace WebApplication2.Service.Common
{
    public interface IManufacturerService
    {
        Task<List<Manufacturer>> GetAsync();
        Task<Manufacturer> GetByIdAsync(Guid id);
        Task<bool> SetAsync(Manufacturer value);
        Task<bool> UpdateAsync(Guid id, Manufacturer value);
        Task<bool> DeleteAsync(Guid id);
    }
}
