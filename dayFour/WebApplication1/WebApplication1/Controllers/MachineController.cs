using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Npgsql;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class MachineController : ApiController
    {
        private NpgsqlConnection connection;

        public MachineController()
        {
            connection = new NpgsqlConnection(CONNECTION_STRING);
        }

        private const string CONNECTION_STRING = "Host=localhost;" +
            "Port=5432;" +
            "Username=postgres;" +
            "Password=nk26cdJx;" +
            "Database=postgres";
        private const string TABLE_NAME = "Machine";

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

        // GET api/<controller>
        public HttpResponseMessage Get()
        {
            try
            {
                connection.Open();
                List<Machine> machineList = new List<Machine>();
                string commandText = $"SELECT * FROM {TABLE_NAME}";
                using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, connection))
                {
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
  
                        while (reader.Read())
                        {
                            Machine mach = ReadMachine(reader);
                            machineList.Add(mach);
                        }
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, machineList);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "An error occurred.");
            }
            finally
            {
                connection.Close();
            }
        }

        // GET api/<controller>/5
        public HttpResponseMessage Get(Guid id)
        {
            try
            {
                connection.Open();
                string commandText = $"SELECT * FROM {TABLE_NAME} WHERE ID = @id";

                using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, connection))
                {
                    cmd.Parameters.AddWithValue("id", id);

                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Machine mach = ReadMachine(reader);
                            return Request.CreateResponse(HttpStatusCode.OK, mach);
                        }
                    }
                }
                return Request.CreateResponse(HttpStatusCode.NotFound, "Machine not found");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "An error occurred.");
            }
            finally
            {
                connection.Close();
            }
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody] Machine value)
        {
            try
            {
                if (value == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Machine object is null");
                }

                connection.Open();
                string commandText = $"INSERT INTO {TABLE_NAME} (id, name, price, maxWeight, typeOfWeight) VALUES (@id, @name, @price, @maxWeight, @typeOfWeight)";

                using (var cmd = new NpgsqlCommand(commandText, connection))
                {
                    Guid randid = System.Guid.NewGuid();
                    cmd.Parameters.AddWithValue("@id", randid);
                    cmd.Parameters.AddWithValue("@name", value.Name);
                    cmd.Parameters.AddWithValue("@price", value.Price);
                    cmd.Parameters.AddWithValue("@maxWeight", value.MaxWeight);
                    cmd.Parameters.AddWithValue("@typeOfWeight", value.TypeOfWeight);

                    cmd.ExecuteNonQuery();
                }
                return Request.CreateResponse(HttpStatusCode.Created, value);

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "An error occurred.");
            }
            finally
            {
                connection.Close();
            }
        }

        // PUT api/<controller>/5
        public HttpResponseMessage Put(Guid id, [FromBody] Machine value)
        {
            try
            {
                connection.Open();
                string commandText = $"UPDATE {TABLE_NAME} SET name = @name, price = @price, maxWeight = @maxWeight, typeOfWeight = @typeOfWeight WHERE id = @id";

                using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@name", value.Name);
                    cmd.Parameters.AddWithValue("@price", value.Price);
                    cmd.Parameters.AddWithValue("@maxWeight", value.MaxWeight);
                    cmd.Parameters.AddWithValue("@typeOfWeight", value.TypeOfWeight);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Machine updated successfully");
                    }
                }

                return Request.CreateResponse(HttpStatusCode.NotFound, "Machine not found");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "An error occurred.");
            }
            finally
            {
                connection.Close();
            }
        }

        // DELETE api/<controller>/5
        public HttpResponseMessage Delete(Guid id)
        {
            try
            {
                connection.Open();
                string commandText = $"DELETE FROM {TABLE_NAME} WHERE id = @id";

                using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Machine deleted successfully");
                    }
                }

                return Request.CreateResponse(HttpStatusCode.NotFound, "Machine not found");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "An error occurred.");
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
