using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Repository;
using WebApplication2.Model;
using WebApplication2.Service.Common;

namespace WebApplication2.Service 
{
        public class MachineService : IMachineService
        {
            private  MachineRepository machineRepository;

            public MachineService()
            {
                 machineRepository = new MachineRepository();
            }

            public async Task<List<Machine>> GetAsync()
            {
                var machines = await machineRepository.GetFromBaseAsync("MACHINE");
                return machines;
            }

            public async Task<Machine> GetByIdAsync(Guid id)
            {
                var machine = await machineRepository.GetFromBaseIdAsync("MACHINE", id);
                return machine ;
            }

            public async Task<bool> SetAsync(Machine value)
            {
                return await machineRepository.SetOnBaseAsync("MACHINE", value);
            }

            public async Task<bool> UpdateAsync(Guid id, Machine value)
            {
                var rowsAffected = await machineRepository.UpdateOnBaseAsync("MACHINE", id, value);
                return rowsAffected != 0;
            }

            public async Task<bool> DeleteAsync(Guid id)
            {
                var rowsAffected = await machineRepository.DeleteFromBaseAsync("MACHINE", id);
                return rowsAffected != 0;
            }
        }
}
