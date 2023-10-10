using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using Npgsql;
using WebApplication2.Model;
using WebApplication2.Service;
using WebApplication2.Service.Common;


namespace WebApplication2.WebAPI.Controllers
{
    public class MachineController : ApiController
    {
        private  IMachineService _machineService;

        public MachineController(IMachineService ms)
        {
            _machineService = ms;
        }

        // GET api/<controller>
        public async System.Threading.Tasks.Task<HttpResponseMessage> Get()
        {
            List<Machine> machlist = new List<Machine>();
            machlist = await _machineService.GetAsync();
            if (machlist.Count !=0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, machlist);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // GET api/<controller>/5
        public async System.Threading.Tasks.Task<HttpResponseMessage> Get(Guid id)
        {
            Machine mach= await _machineService.GetByIdAsync(id);
            if (mach == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest,"wrong ID");
                
            }
            return Request.CreateResponse(HttpStatusCode.OK,mach);
    
        }

        // POST api/<controller>
        public async System.Threading.Tasks.Task<HttpResponseMessage> Post([FromBody] Machine value)
        {
            if (await _machineService.SetAsync(value))
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Posted");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // PUT api/<controller>/5
        public async System.Threading.Tasks.Task<HttpResponseMessage> Put(Guid id, [FromBody] Machine value)
        {
            if (await _machineService.UpdateAsync(id, value))
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Updated");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/<controller>/5
        public async System.Threading.Tasks.Task<HttpResponseMessage> Delete(Guid id)
        {
            if (await _machineService.DeleteAsync(id))
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Deleted");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}
