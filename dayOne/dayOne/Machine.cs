using System;
using System.Globalization;

namespace ConsoleApp1{
    
    class Machine{
            public String name;
            public int price;
            public int maxWeight;
            public Weight typeOfWeight;

        public Machine()
        {   this.name = "";
            this.price=0;
            this.maxWeight = 0;
            this.typeOfWeight =new Weight(20, "plate") ;
        }
        public Machine(string name,int price, int maxWeight, Weight typeOfWeight)
        {
            this.name = name;
            this.price=price;
            this.maxWeight = maxWeight;
            this.typeOfWeight =typeOfWeight ;
        }

    }
}