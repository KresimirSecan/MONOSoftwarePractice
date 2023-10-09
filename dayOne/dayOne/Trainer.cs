using System;
using System.ComponentModel.Design;
using System.Globalization;
using System.Linq.Expressions;
using System.Runtime.InteropServices;

namespace ConsoleApp1{

    class Trainer : Person{ 
            private  IDictionary<int, Tuple<Client,DateTime>> clientAppoitments = new Dictionary<int, Tuple<Client,DateTime>>() ; 
    
        public Trainer (IDictionary<int, Tuple<Client,DateTime>> clientapp){
            this.clientAppoitments = clientapp;
        }


        public void ChangeAppoitment(int app , DateTime newapp) {
            clientAppoitments[app] =new Tuple<Client, DateTime> (clientAppoitments[app].Item1,newapp) ;
            Console.WriteLine ("|----Appoitment changed----|");
            return;
        }

        public void DeleteAppoitment(int app){
            clientAppoitments.Remove(app);
            Console.WriteLine ("|----Appoitment deleted----|");
            return;
        }
        public void AddAppoitmnet(int app,Client c , DateTime date){
            clientAppoitments.Add(app,(new Tuple<Client, DateTime> (c,date)));
            Console.WriteLine ("|----Appoitment added----|");
            return;   
        }


        public void PrintTermins(){
            foreach(var item in clientAppoitments){
                 Console.WriteLine (String.Format("Client: {0} | Appoitment: {1}" ,item.Value.Item1,item.Value.Item2));
            }
        }
        public void PrintTermins(DateTime d1){
            
            foreach(var item in clientAppoitments){
                 if (item.Value.Item2 == d1){
                    Console.WriteLine (String.Format("Client: {0} | Appoitment: {1}" ,item.Value.Item1,item.Value.Item2));
                 }
            }
        }
        public void PrintTermins(DateTime d1 ,DateTime d2){
             foreach(var item in clientAppoitments){
                 if (item.Value.Item2  >= d1 && item.Value.Item2  <= d2 ){
                    Console.WriteLine (String.Format("Client: {0} | Appoitment: {1}" ,item.Value.Item1,item.Value.Item2));
                 }
            }
        }
    }
}