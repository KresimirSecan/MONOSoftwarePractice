using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Npgsql;
using WebApplication2.Model;
using WebApplication2.Service;
using WebApplication2.Service.Common;

namespace WebApplication2.WebAPI.Controllers
{
    public class ManufacturerController : ApiController
    {
        private IManufacturerService _manufacturerService;

        public ManufacturerController(IManufacturerService ms)
        {
            _manufacturerService = ms;
        }

       

        // GET api/<controller>
        [HttpGet]
        public async System.Threading.Tasks.Task<HttpResponseMessage> Get()
        {
            List<Manufacturer> machlist = new List<Manufacturer>();
            machlist = await _manufacturerService.GetAsync();
            if (machlist.Count != 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK,machlist);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // GET api/<controller>/5
        public async System.Threading.Tasks.Task<HttpResponseMessage> Get(Guid id)
        {
            Manufacturer mach = await _manufacturerService.GetByIdAsync(id);
            if (mach!=null)
            {
                return Request.CreateResponse(HttpStatusCode.OK,mach);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // POST api/<controller>
        public async System.Threading.Tasks.Task<HttpResponseMessage> Post([FromBody] Manufacturer value)
        {
            if (await _manufacturerService.SetAsync(value))
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Posted");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // PUT api/<controller>/5
        public async System.Threading.Tasks.Task<HttpResponseMessage> Put(Guid id, [FromBody] Manufacturer value)
        {
            if (await _manufacturerService.UpdateAsync(id, value))
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
            if (await _manufacturerService.DeleteAsync(id))
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
