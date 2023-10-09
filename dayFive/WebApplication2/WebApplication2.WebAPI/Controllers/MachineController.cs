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

namespace WebApplication2.WebAPI.Controllers
{
    public class MachineController : ApiController
    {


        // GET api/<controller>
        [HttpGet]
        public HttpResponseMessage Get()
        {
            MachineService mach = new MachineService();
            if (mach.Get())
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else 
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // GET api/<controller>/5
        public HttpResponseMessage Get(Guid id)
        {
            MachineService mach = new MachineService();
            if (mach.GetById(id))
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody] Machine value)
        {
            MachineService mach = new MachineService();
            if (mach.Set(value))
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Posted");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // PUT api/<controller>/5
        public HttpResponseMessage Put(Guid id, [FromBody] Machine value)
        {
            MachineService mach = new MachineService();
            if (mach.Update(id,value))
            {
                return Request.CreateResponse(HttpStatusCode.OK,"Updated");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/<controller>/5
        public HttpResponseMessage Delete(Guid id)
        {

            MachineService mach = new MachineService();
            if (mach.Delete(id))
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Deleted");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
