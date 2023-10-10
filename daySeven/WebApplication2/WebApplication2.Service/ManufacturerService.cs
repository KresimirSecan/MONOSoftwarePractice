using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication2.Model;
using WebApplication2.Repository.Common;
using WebApplication2.Service.Common;

namespace WebApplication2.Service
{
    public class ManufacturerService:IManufacturerService
    {
        private IManufacturerRepository _manufacturerRepository;

        public ManufacturerService(IManufacturerRepository ms)
        {
            _manufacturerRepository = ms;
        }


        public async Task<List<Manufacturer>> GetAsync()
        {
            var manufacturer = await _manufacturerRepository.GetFromBaseAsync("Manufacturer");
            return manufacturer;
        }

        public async Task<Manufacturer> GetByIdAsync(Guid id)
        {
            var manufacturer = await _manufacturerRepository.GetFromBaseIdAsync("Manufacturer", id);
            return manufacturer ;
        }

        public async Task<bool> SetAsync(Manufacturer value)
        {
            return await _manufacturerRepository.SetOnBaseAsync("Manufacturer", value);
        }

        public async Task<bool> UpdateAsync(Guid id, Manufacturer value)
        {
            var rowsAffected = await _manufacturerRepository.UpdateOnBaseAsync("Manufacturer", id, value);
            return rowsAffected != 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var rowsAffected = await _manufacturerRepository.DeleteFromBaseAsync("Manufacturer", id);
            return rowsAffected != 0;
        }
    }
}
