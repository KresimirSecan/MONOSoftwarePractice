using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebApplication2.Model;
using WebApplication2.Repository.Common;

namespace Repository
{
    public class ManufacturerRepository  : IManufacturerRepository
    {
        internal BaseConnection baseconnection;

        public ManufacturerRepository() { baseconnection = new BaseConnection(); }

        private static Manufacturer ReadManufacturer(NpgsqlDataReader reader)
        {
            Guid? id = reader["id"] as Guid?;
            string name = reader["name"] as string;
            string address = reader["address"] as string;
            Guid? machineId = reader["machineID"] as Guid?;

            Manufacturer mach = new Manufacturer
            {
                Id = id.Value,
                Name = name,
                Address = address,
                MachineId = machineId.Value
            };
            return mach;
        }

        public async Task<List<Manufacturer>> GetFromBaseAsync(string tablename)
        {
            baseconnection.OpenConnection();
            List<Manufacturer> manfList = new List<Manufacturer>();
            string commandText = $"SELECT * FROM {tablename} INNER JOIN MACHINE ON MANUFACTURER.machineID = MACHINE.ID";
            using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, baseconnection.connection))
            {
                using (NpgsqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        Manufacturer mach = ReadManufacturer(reader);
                        manfList.Add(mach);
                    }
                }
            }
            return manfList;
        }

        public async Task<Manufacturer> GetFromBaseIdAsync(string tablename,Guid id)
        {
            baseconnection.OpenConnection();
            Manufacturer mach = new Manufacturer();
            string commandText = $"SELECT * FROM {tablename} where id = @id";
            using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, baseconnection.connection))
            {
                cmd.Parameters.AddWithValue("@id", id);
                using (NpgsqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        mach = ReadManufacturer(reader);
                        return mach;
                    }
                }
            }
            return mach;
        }

        public async Task<bool> SetOnBaseAsync(string tablename, Manufacturer value)
        {
            if (value == null)
            {
                return false;
            }

            baseconnection.OpenConnection();
            string commandText = $"INSERT INTO {tablename} (id, name, address, MachineId) VALUES (@id, @name, @address, @machineId)";

            using (var cmd = new NpgsqlCommand(commandText, baseconnection.connection))
            {
                Guid randid = System.Guid.NewGuid();
                cmd.Parameters.AddWithValue("@id", randid);
                cmd.Parameters.AddWithValue("@name", value.Name);
                cmd.Parameters.AddWithValue("@address", value.Address);
                cmd.Parameters.AddWithValue("@machineId", value.MachineId);

                await cmd.ExecuteNonQueryAsync();
            }
            return true;
        }

        public async Task<int> UpdateOnBaseAsync(string tablename, Guid id, Manufacturer value)
        {
            baseconnection.OpenConnection();
            int rowsAffected = 0;
            string commandText = $"UPDATE {tablename} SET name = @name, address = @address WHERE id = @id";

            using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, baseconnection.connection))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@name", value.Name);
                cmd.Parameters.AddWithValue("@address", value.Address);

                rowsAffected = await cmd.ExecuteNonQueryAsync();
            }

            return rowsAffected;
        }

        public async Task<int> DeleteFromBaseAsync(string tablename, Guid id)
        {
            baseconnection.OpenConnection();
            int rowsAffected = 0;
            string commandText = $"DELETE FROM {tablename} WHERE id = @id";

            using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, baseconnection.connection))
            {
                cmd.Parameters.AddWithValue("@id", id);

                rowsAffected = await cmd.ExecuteNonQueryAsync();
            }
            return rowsAffected;
        }
    }
}


