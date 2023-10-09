using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using WebApplication2.Model;
using Npgsql;
using WebApplication2.Repository.Common;

namespace Repository
{
    public class MachineRepository :IMachineRepository
    {
        internal BaseConnection baseconnection;

        public MachineRepository() { baseconnection = new BaseConnection(); }

        public static Machine ReadMachine(NpgsqlDataReader reader)
        {
            Guid? id = reader["id"] as Guid?;
            string name = reader["name"] as string;
            int? price = reader["price"] as int?;
            int? maxWeight = reader["maxWeight"] as int?;
            string typeOfWeight = reader["typeOfWeight"] as string;

            Machine mach = new Machine
            {
                Id = id.Value,
                Name = name,
                Price = price.Value,
                MaxWeight = maxWeight.Value,
                TypeOfWeight = typeOfWeight
            };
            return mach;
        }

        public async Task<List<Machine>> GetFromBaseAsync(string tablename)
        {
            baseconnection.OpenConnection();
            List<Machine> machineList = new List<Machine>();
            string commandText = $"SELECT * FROM {tablename}";
            using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, baseconnection.connection))
            {
                using (NpgsqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        Machine mach =  ReadMachine(reader);
                        machineList.Add(mach);
                    }
                    return machineList;
                }
            }
            return machineList;
        }

        public async Task<Machine> GetFromBaseIdAsync(string tablename, Guid id)
        {
            baseconnection.OpenConnection();
            Machine mach = new Machine();
            string commandText = $"SELECT * FROM {tablename} WHERE id = @id";
            using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, baseconnection.connection))
            {
                cmd.Parameters.AddWithValue("@id", id);
                using (NpgsqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        mach = ReadMachine(reader);
                        return mach;
                    }
                }
            }
            return mach;
        }

        public async Task<bool> SetOnBaseAsync(string tablename, Machine value)
        {
            baseconnection.OpenConnection();
            string commandText = $"INSERT INTO {tablename} (id, name, price, maxWeight, typeOfWeight) " +
                $"VALUES (@id, @name, @price, @maxWeight, @typeOfWeight)";

            using (var cmd = new NpgsqlCommand(commandText, baseconnection.connection))
            {
                Guid randid = System.Guid.NewGuid();
                cmd.Parameters.AddWithValue("@id", randid);
                cmd.Parameters.AddWithValue("@name", value.Name);
                cmd.Parameters.AddWithValue("@price", value.Price);
                cmd.Parameters.AddWithValue("@maxWeight", value.MaxWeight);
                cmd.Parameters.AddWithValue("@typeOfWeight", value.TypeOfWeight);

                await cmd.ExecuteNonQueryAsync();
                return true;
            }
        }

        public async Task<int> UpdateOnBaseAsync(string tablename, Guid id, Machine value)
        {
            baseconnection.OpenConnection();
            int rowsAffected = 0;
            string commandText = $"UPDATE {tablename} SET name = @name, price = @price, maxWeight = @maxWeight, typeOfWeight = @typeOfWeight WHERE id = @id";

            using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, baseconnection.connection))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@name", value.Name);
                cmd.Parameters.AddWithValue("@price", value.Price);
                cmd.Parameters.AddWithValue("@maxWeight", value.MaxWeight);
                cmd.Parameters.AddWithValue("@typeOfWeight", value.TypeOfWeight);

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
