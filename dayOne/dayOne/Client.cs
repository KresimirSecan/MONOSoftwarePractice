using System;
using System.ComponentModel.Design;
using System.Globalization;

namespace ConsoleApp1{
    
    class Client : Person{
            private bool isTheMembershipActive;

        public Client (bool active = true){
            this.isTheMembershipActive = active;
        }

        public bool getMembership(){
            return isTheMembershipActive;
        }

    }
}