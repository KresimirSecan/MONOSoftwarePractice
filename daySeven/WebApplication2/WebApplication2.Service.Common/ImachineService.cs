using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication2.Model;

namespace WebApplication2.Service.Common
{
    public interface IMachineService
    {
        Task<List<Machine>> GetAsync();
        Task<Machine> GetByIdAsync(Guid id);
        Task<bool> SetAsync(Machine value);
        Task<bool> UpdateAsync(Guid id, Machine value);
        Task<bool> DeleteAsync(Guid id);
    }
}
