using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Manufacturer
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String Address { get; set; }

        public Guid MachineId { get; set; }

    }
}