using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication2.Model;


namespace WebApplication2.Repository.Common
{
     public interface IMachineRepository
     {
            Task<List<Machine>> GetFromBaseAsync(string tablename);
            Task<Machine> GetFromBaseIdAsync(string tablename, Guid id);
            Task<bool> SetOnBaseAsync(string tablename, Machine value);
            Task<int> UpdateOnBaseAsync(string tablename, Guid id, Machine value);
            Task<int> DeleteFromBaseAsync(string tablename, Guid id);
     }
}
