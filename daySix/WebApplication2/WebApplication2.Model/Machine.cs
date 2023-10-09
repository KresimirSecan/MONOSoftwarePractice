using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication2.Model.Common;

namespace WebApplication2.Model
{
    public class Machine 
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public int Price { get; set; }
        public int MaxWeight { get; set; }
        public String TypeOfWeight { get; set; }
    }
}
