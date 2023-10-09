using System;
using System.ComponentModel.Design;
using System.Globalization;

namespace ConsoleApp1{
    
    class Worker : Person{
            private int salary;
            private int workHours;

        public Worker (int salary =0 , int workHours = 0){
            this.salary = salary;
            this.workHours = workHours;
        }
        public int getWorkHours(){
                return workHours;
        }

    }
}