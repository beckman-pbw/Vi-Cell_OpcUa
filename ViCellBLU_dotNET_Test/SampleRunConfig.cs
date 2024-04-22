using System;
using System.Runtime.Serialization;

namespace ViCellBLU_dotNET_Test
{
    // ***************************************************************************
    // ConnectionCfg
    // ***************************************************************************
    [Serializable()]
    // ***************************************************************************
    public partial class ConnectionCfg
    {
        public ConnectionCfg()
        {
            IPAddr = "127.0.0.1";
            Port = 62641;
            Username = "";
            Password = "";
        }

        [DataMember()]
        public String IPAddr { get; set; }

        [DataMember()]
        public UInt32 Port { get; set; }

        [DataMember()]
        public String Username { get; set; }

        [DataMember()]
        public String Password { get; set; }


        // ************************************
        public override string ToString()
        {
            string st = "{" + Username + "}";
            return st;
        }

    }


}
