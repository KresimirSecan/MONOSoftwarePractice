using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Npgsql;
using WebApplication2.Model;

namespace WebApplication2.WebAPI.Controllers
{
    public class ManufacturerController : ApiController
    {


        // GET api/<controller>
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // GET api/<controller>/5
        public HttpResponseMessage Get(Guid id)
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }


        [System.Web.Http.Route("api/Manufacturer/GetMachine")]
        public HttpResponseMessage GetMachine()
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }



        // POST api/<controller>
        public HttpResponseMessage Post([FromBody] Manufacturer value)
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // PUT api/<controller>/5
        public HttpResponseMessage Put(Guid id, [FromBody] Manufacturer value)
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // DELETE api/<controller>/5
        public HttpResponseMessage Delete(Guid id)
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
