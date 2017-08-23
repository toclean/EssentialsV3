using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Information.Models;
using Microsoft.Win32;
using DataAccessLayer.Models;

namespace Information
{
    public enum PacketType
    {
        Connect,
        Disconnect,
        Message,
        Status
    }

    public class PacketFactory
    {
        public Packet ConnectPacket(PacketType type, User user)
        {
            return new Packet()
            {
                Type = type.ToString(),
                Username = user.Username,
                Password = user.Password 
            };
        }

        public Packet DisconnectPacket(PacketType type, User user)
        {
            return new Packet
            {
                Type = type.ToString(),
                Username = user.Username
            };
        }
    }
}
