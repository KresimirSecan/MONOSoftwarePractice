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
    public class MachineRepository 
    {
        internal BaseConnection baseconnection;

        public MachineRepository() { baseconnection = new BaseConnection();}

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


        public List<Machine> GetFromBase(string tablename)
        {
            baseconnection.OpenConnection();
            List<Machine> machineList = new List<Machine>();
            string commandText = $"SELECT * FROM {tablename}";
            using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, baseconnection.connection))
            {
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        Machine mach = ReadMachine(reader);
                        machineList.Add(mach);
                    }
                }
            }
            return machineList ;
        }


        public Machine GetFromBaseId(string tablename,Guid id)
        {
            baseconnection.OpenConnection();
            Machine mach = new Machine();
            string commandText = $"SELECT * FROM {tablename} where id = @id";
            using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, baseconnection.connection))
            {
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        mach = ReadMachine(reader);
                        return mach;
                    }
                }
            }
            return mach ;
        }

        public bool SetOnBase(string tablename,Machine value) 
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

                cmd.ExecuteNonQuery();
                return true;
            }

            return false;
            
        }

        public int UpdateOnBase(string tablename, Guid id, Machine value)
        {
            baseconnection.OpenConnection();
            int rowsAffected = 0;

            string selectQuery = $"SELECT * FROM {tablename} WHERE id = @id";

            using (NpgsqlCommand selectCmd = new NpgsqlCommand(selectQuery, baseconnection.connection))
            {
                selectCmd.Parameters.AddWithValue("@id", id);

                using (NpgsqlDataReader reader = selectCmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        bool needsUpdate = false;

                        if (!string.Equals(reader["name"].ToString(), value.Name))
                        {
                            needsUpdate = true;
                        }

                        if (!decimal.Equals(reader["price"], value.Price))
                        {
                            needsUpdate = true;
                        }

                        if (!decimal.Equals(reader["maxWeight"], value.MaxWeight))
                        {
                            needsUpdate = true;
                        }

                        if (!string.Equals(reader["typeOfWeight"].ToString(), value.TypeOfWeight))
                        {
                            needsUpdate = true;
                        }

                        if (needsUpdate)
                        {
                            string updateQuery = $"UPDATE {tablename} SET " +
                                $"name = @name, price = @price, maxWeight = @maxWeight, typeOfWeight = @typeOfWeight " +
                                $"WHERE id = @id";

                            using (NpgsqlCommand updateCmd = new NpgsqlCommand(updateQuery, baseconnection.connection))
                            {
                                updateCmd.Parameters.AddWithValue("@id", id);
                                updateCmd.Parameters.AddWithValue("@name", value.Name);
                                updateCmd.Parameters.AddWithValue("@price", value.Price);
                                updateCmd.Parameters.AddWithValue("@maxWeight", value.MaxWeight);
                                updateCmd.Parameters.AddWithValue("@typeOfWeight", value.TypeOfWeight);

                                rowsAffected = updateCmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }

            return rowsAffected;
        }

 

        public int DeleteFromBase(string tablename, Guid id) {
            baseconnection.OpenConnection(); ;
            int rowsAffected = 0;
            string commandText = $"DELETE FROM {tablename} WHERE id = @id";

            using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, baseconnection.connection))
            {
                cmd.Parameters.AddWithValue("@id", id);

                rowsAffected = cmd.ExecuteNonQuery();
                
            }

            return rowsAffected;
        }

    }
}
