﻿using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication2.Model;
using WebApplication2.Service.Common;

namespace WebApplication2.Service
{
    public class ManufacturerService:IManufacturerService
    {
        private ManufacturerRepository manufacturerRepository;

        public ManufacturerService()
        {
            manufacturerRepository = new ManufacturerRepository();
        }

        public async Task<List<Manufacturer>> GetAsync()
        {
            var manufacturer = await manufacturerRepository.GetFromBaseAsync("Manufacturer");
            return manufacturer;
        }

        public async Task<Manufacturer> GetByIdAsync(Guid id)
        {
            var manufacturer = await manufacturerRepository.GetFromBaseIdAsync("Manufacturer", id);
            return manufacturer ;
        }

        public async Task<bool> SetAsync(Manufacturer value)
        {
            return await manufacturerRepository.SetOnBaseAsync("Manufacturer", value);
        }

        public async Task<bool> UpdateAsync(Guid id, Manufacturer value)
        {
            var rowsAffected = await manufacturerRepository.UpdateOnBaseAsync("Manufacturer", id, value);
            return rowsAffected != 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var rowsAffected = await manufacturerRepository.DeleteFromBaseAsync("Manufacturer", id);
            return rowsAffected != 0;
        }
    }
}
