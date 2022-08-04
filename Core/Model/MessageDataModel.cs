using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    public class MessageData
    {
        public ClientData Client { get; set; }=new ClientData();
        public string Message { get; set; }

    }
}
