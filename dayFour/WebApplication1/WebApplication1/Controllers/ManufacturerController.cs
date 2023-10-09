using Npgsql;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using WebApplication1.Models;


namespace WebApplication1.Controllers
{
    public class ManufacturerController : ApiController
    {
        private NpgsqlConnection connection;

        public ManufacturerController()
        {
            connection = new NpgsqlConnection(CONNECTION_STRING);
        }

        private const string CONNECTION_STRING = "Host=localhost;" +
            "Port=5432;" +
            "Username=postgres;" +
            "Password=nk26cdJx;" +
            "Database=postgres";
        private const string TABLE_NAME = "Manufacturer";

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
            


        // GET api/<controller>
        public HttpResponseMessage Get()
        {
            try
            {
                connection.Open();
                List<Manufacturer> manfList = new List<Manufacturer>();
                string commandText = $"SELECT * FROM {TABLE_NAME} INNER JOIN MACHINE ON MANUFACTURER.machineID = MACHINE.ID";
                using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, connection))
                {
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            Manufacturer mach = ReadManufacturer(reader);
                            manfList.Add(mach);
                        }
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, manfList);
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
                            Manufacturer mach = ReadManufacturer(reader);
                            return Request.CreateResponse(HttpStatusCode.OK, mach);
                        }
                    }
                }
                return Request.CreateResponse(HttpStatusCode.NotFound, "Manufacturer not found");
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

        
        [System.Web.Http.Route("api/Manufacturer/GetMachine")]
        public HttpResponseMessage GetMachine()
        {
            try
            {
                connection.Open();
                List<Machine> manfList = new List<Machine>();
                string commandText = $"SELECT * FROM MACHINE  INNER JOIN {TABLE_NAME} ON MANUFACTURER.machineID = MACHINE.ID ";
                using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, connection))
                {
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Machine mach = MachineController.ReadMachine(reader);
                            manfList.Add(mach);
                        }
                    }
                }
                return Request.CreateResponse(HttpStatusCode.OK, manfList);
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
        public HttpResponseMessage Post([FromBody] Manufacturer value)
        {
            try
            {
                if (value == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Manufacturer object is null");
                }

                connection.Open();
                string commandText = $"INSERT INTO {TABLE_NAME} (id, name, address,MachineId) VALUES (@id, @name, @address,@machineId)";

                using (var cmd = new NpgsqlCommand(commandText, connection))
                {
                    Guid randid = System.Guid.NewGuid();
                    cmd.Parameters.AddWithValue("@id", randid);
                    cmd.Parameters.AddWithValue("@name", value.Name);
                    cmd.Parameters.AddWithValue("@address", value.Address);
                    cmd.Parameters.AddWithValue("@machineID", value.MachineId);

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
        public HttpResponseMessage Put(Guid id, [FromBody] Manufacturer value)
        {
            try
            {
                connection.Open();
                string commandText = $"UPDATE {TABLE_NAME} SET name = @name, address = @address WHERE id = @id";

                using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@name", value.Name);
                    cmd.Parameters.AddWithValue("@address", value.Address);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Manufacturer updated successfully");
                    }
                }

                return Request.CreateResponse(HttpStatusCode.NotFound, "Manufacturer not found");
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
                        return Request.CreateResponse(HttpStatusCode.OK, "Manufacturer deleted successfully");
                    }
                }

                return Request.CreateResponse(HttpStatusCode.NotFound, "Manufacturer not found");
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
