using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace ConsoleApp1{
    public class Program {
        static int numberOfclients=300;

        static int picker(int numberOfPicks){
            Console.WriteLine("Pick the number of the option:\n");
            int input =int.Parse(Console.ReadLine());
            if (numberOfPicks == 4){
                while(input !=1 && input != 2 &&  input !=3 &&  input !=4){
                Console.WriteLine("Pick the number of the option:\n");
                input =int.Parse(Console.ReadLine());
                }
                return input;
            }else if (numberOfPicks == 6){
                 while(input !=1 &&  input != 2 &&  input !=3 && input !=4 &&  input !=5 &&  input !=6){
                    Console.WriteLine("Pick the number of the option:\n");
                    input =int.Parse(Console.ReadLine());
                }
                return input;
            }else if(numberOfPicks == 1){
                 while(input !=1 &&  input != 2 ){
                    Console.WriteLine("Pick the number of the option:\n");
                    input =int.Parse(Console.ReadLine());
                }
                return input;
            }else if(numberOfPicks == 2){
                 while(input !=1 &&  input != 2 &&  input !=3 ){
                    Console.WriteLine("Pick the number of the option:\n");
                    input =int.Parse(Console.ReadLine());
                }
                
            }
            return input;
            
        }

        static int MainInterface(){
            Console.WriteLine("|==========WELCOME==========|\n");
            Console.WriteLine("1.Trainer\n");
            Console.WriteLine("2.Worker\n");
            Console.WriteLine("3.Client\n");
            Console.WriteLine("4.Exit\n");
            return (picker(4));
    
        }

        static void ClientAndWorkerInterface(int optionIndex, bool isClient){
            
            Machine m1 = new Machine("legpress",1000 , 1300, new Weight(20,"dumbell"));
            Machine m2= new Machine("cabel",2000 , 800, new Weight(30,"plate"));
            Machine m3 = new Machine("benchpress",800 , 1300, new Weight(20,"plate"));
            List<Machine>  machinelist = new List<Machine>();
            machinelist.Add(m1);
            machinelist.Add(m2);
            machinelist.Add(m3);

            Worker worker1 = new Worker(10,10);
            if (isClient){
                Client client1 = new Client ();
                if(optionIndex ==1){
                    if(client1.getMembership()){
                        Console.WriteLine("Membership is valid");
                    }else{
                        Console.WriteLine("Membership is not valid");
                    }
               }else{
                    return;
                }     
            }else{
                if(optionIndex ==1){
                    foreach (Machine m in  machinelist){  
                        Console.WriteLine(String.Format("{0} with max weight {1} using {2}",m.name,m.maxWeight,m.typeOfWeight.type));
                    }
                }else if (optionIndex==2){
                    Console.WriteLine(String.Format("You have {0}",worker1.getWorkHours())); 
                }else{
                    return;
                }     
            }
           
        }


        static void Main(string[] args){
            Client client1 = new Client();

            DateTime datenow = new DateTime (2003,12,25);
            Tuple<Client,DateTime> tupl1 = new Tuple<Client, DateTime> (client1,datenow);
            datenow = new DateTime (2009,9,13);
            Tuple<Client,DateTime> tupl2 = new Tuple<Client, DateTime> (client1,datenow);
            IDictionary<int, Tuple<Client,DateTime>> clientAppoitments = new Dictionary<int, Tuple<Client,DateTime>>();
            clientAppoitments[0] =tupl1 ;
            clientAppoitments[1]=tupl2;
            Trainer trainer1 = new Trainer(clientAppoitments);

            
            int input = MainInterface();
            

            if (input==1){
                    Console.WriteLine("|==========TRAINER==========|\n");
                    Console.WriteLine("1.Add appoitment\n");
                    Console.WriteLine("2.Delete appoitment\n");
                    Console.WriteLine("3.Change appoitment\n");
                    Console.WriteLine("4.All termins\n");
                    Console.WriteLine("5.Termins on date\n");
                    Console.WriteLine("6.Termins between dates\n");

                    input = picker(6);
                   
                    if(input ==1){    
                    //meni za odabir klijenata i nadodat unos datuma
                
                        DateTime newdate =new DateTime (1999,12,25);
                        trainer1.AddAppoitmnet(numberOfclients,client1,newdate);
                        numberOfclients++;
                        return;
                    }else if (input==2){
                        Console.WriteLine("Write and ID of persone to delete: \n");
                        input =int.Parse(Console.ReadLine());
                        trainer1.DeleteAppoitment(input);

                    }else if (input==3){
                        Console.WriteLine("Write and ID of persone to edit: \n");
                        input =int.Parse(Console.ReadLine());
                        Console.WriteLine("Write new date (FORMAT OF INPUT: YYYY/MM/DD): \n");
                        string s = Console.ReadLine();
                        //napravit funkciju za parsira string i nadodat funkciju u u opciju 5 i 6 
                        DateTime newdate =new DateTime (1999,12,25);
                        trainer1.ChangeAppoitment(input,newdate);

                    }else if (input==4){
                        trainer1.PrintTermins();
                    }else if (input==5){
                        DateTime newdate =new DateTime (1999,12,25);
                        trainer1.PrintTermins(newdate);
                    }else if (input==6){
                        DateTime newdate1 =new DateTime (1999,5,25);
                        DateTime newdate2 =new DateTime (1999,12,25);
                        Console.WriteLine("Write new date (FORMAT OF INPUT: YYYY/MM/DD): \n");
                        string s1 = Console.ReadLine(); 
                        Console.WriteLine("Write new date (FORMAT OF INPUT: YYYY/MM/DD): \n");
                        string s2 = Console.ReadLine();
                        trainer1.PrintTermins(newdate1,newdate2);
                    }else{
                        return;
                    }     

            }else if (input==2){
                    Console.WriteLine("|==========CLIENT==========|\n");
                    Console.WriteLine("1.Check membership\n");

                    input = picker(1);
                    ClientAndWorkerInterface(input,true);

            }else if (input==3){
                    Console.WriteLine("|==========WORKER==========|\n");
                    Console.WriteLine("1.Check Machine\n");
                    Console.WriteLine("2.Check work hours\n");

                    input = picker(2);
                    ClientAndWorkerInterface(input,false);

            }else{
                return;
            }
           
            


        }
    }
}
          