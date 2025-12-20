using System;
using System.Text.Json;

namespace NewsApp.Common
{
    public class Packet
    {
        public string Command { get; set; }   // Loại lệnh: "LOGIN", "REGISTER",...
        public string Payload { get; set; }   // Dữ liệu đi kèm (dạng JSON string)

        public Packet()
        {
            Command = string.Empty;
            Payload = string.Empty;
        }

        public Packet(string command, string payload)
        {
            Command = command;
            Payload = payload;
        }

        public Packet(string command, object data)
        {
            Command = command;
            Payload = JsonSerializer.Serialize(data);
        }
    }
}