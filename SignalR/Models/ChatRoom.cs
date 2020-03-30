using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalR.Models
{
    public class ChatRoom
    {
        public string RoomName { get; set; }
        public List<string> ConnectionIds { get; set; }
    }
}
