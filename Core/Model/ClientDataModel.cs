using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    public class ClientData
    {
        public string Name { get; set; }
        public string IP { get; set; }
        [JsonIgnore]
        public Socket Socket { get; set; }
        [JsonIgnore]
        public string FullName
        {
            get
            {
                return $"{Name}({IP})";
            }
        }

    }
}
