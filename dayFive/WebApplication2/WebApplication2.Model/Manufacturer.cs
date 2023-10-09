using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication2.Model.Common;

namespace WebApplication2.Model
{
    public class Manufacturer
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String Address { get; set; }
        public Guid MachineId { get; set; }
    }
}
