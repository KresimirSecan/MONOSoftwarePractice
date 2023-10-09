using System;
using System.Globalization;

namespace ConsoleApp1{
    
    abstract class Person{
            public string name;
            public string surname;
            protected  static int personalID = 0;

        
        public Person (string name = "", string surname =""){
            this.name = name;
            this.surname = surname;
            personalID++;
        }

    }
}