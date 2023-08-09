using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;

namespace ZekLIB
{
    internal class LoginInformation
    {
        private static LoginInformation instance;

        private static LoginInformation username;
        private static LoginInformation password;
        private static LoginInformation type;
        private static LoginInformation isloggin;
        private static LoginInformation machineID;

        public bool IsLoggin { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Type { get; set; }
        public string MachineID { get; set; }


        private LoginInformation()
        {

        }
        public static LoginInformation GetInstance()
        {
            if (instance == null)
            {
                instance = new LoginInformation();
            }
            return instance;
        }
    }
}
