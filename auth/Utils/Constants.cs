using System;
using System.Collections.Generic;
using System.Text;

namespace auth.Utils
{
    class Constants
    {
        public static string BASE_URL = "http://" + Environment.GetEnvironmentVariable("MACHINE_LOCAL_IPV4");
        public static string CONSUL_PORT = "8500";
        public static string GET_AGENT_BY_EMAIL = "/agents/query?email=";
    }
}
