using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Repository;
using WebApplication2.Model;

namespace WebApplication2.Service
{
    public class MachineService
    {
        public bool Get() {

            MachineRepository mach = new MachineRepository();

            return (mach.GetFromBase("Machine").Count() == 0);
        }
        public bool GetById(Guid id)
        {

            MachineRepository mach = new MachineRepository();

            return (mach.GetFromBaseId("Machine",id) != null);
        }



        public bool Set(Machine value) {
            MachineRepository mach = new MachineRepository();
            return (mach.SetOnBase("Machine",value));
        }
        public bool Update(Guid id, Machine value) {
            MachineRepository mach = new MachineRepository();
            return (mach.UpdateOnBase("Machine",id,value) != 0);
        }
        public bool Delete(Guid id) {
            MachineRepository mach = new MachineRepository();
            return (mach.DeleteFromBase("Machine",id) != 0);
        }

        

    }
}
