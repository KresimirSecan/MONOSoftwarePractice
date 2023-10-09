using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication2.Model;

namespace WebApplication2.Repository.Common
{
    public interface IManufacturerRepository
    {
        Task<List<Manufacturer>> GetFromBaseAsync(string tablename);
        Task<Manufacturer> GetFromBaseIdAsync(string tablename, Guid id);
        Task<bool> SetOnBaseAsync(string tablename, Manufacturer value);
        Task<int> UpdateOnBaseAsync(string tablename, Guid id, Manufacturer value);

        Task<int> DeleteFromBaseAsync(string tablename, Guid id);
    }

}
