using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using NewsApp.Common;

namespace NewsApp.Network
{
    public class ClientSocket
    {

        private TcpClient _client;
        private StreamReader? _reader;
        private StreamWriter? _writer;
        private const string IP_ADDRESS = "127.0.0.1";
        private const int PORT = 8080;

        private bool isConnected = false;

        public bool IsConnected { get { return isConnected; } }

        public event Action<string>? ConnectionStatusChanged;
        public event Action<Packet?>? DataReceive;

        public event Action<string>? ErrorData;

        public ClientSocket()
        {
            _client = new TcpClient();
        }


        public void Start()
        {
            try
            {
                _client.Connect(IP_ADDRESS, PORT);
                NetworkStream stream = _client.GetStream();
                _reader = new(stream, Encoding.UTF8);
                _writer = new(stream, Encoding.UTF8) { AutoFlush = true };
                isConnected = true;

                Thread receiveThread = new(ListenForServer)
                {
                    IsBackground = true
                };
                receiveThread.Start();
                ConnectionStatusChanged?.Invoke("Đã kết nối đến server");
            }
            catch (Exception e)
            {
                isConnected = false;
                ConnectionStatusChanged?.Invoke("Kết nối thất bại: " + e.Message);
            }
        }

        private void ListenForServer()
        {
            try
            {
                while (isConnected && _client.Connected)
                {
                    string? jsonResponse = _reader?.ReadLine();
                    if (jsonResponse == null)
                    {
                        ConnectionStatusChanged?.Invoke("Mất kết nối với server");
                        Disconnect();
                        break;

                    }
                    Packet? responsePacket = JsonSerializer.Deserialize<Packet>(jsonResponse);
                    if (responsePacket == null)
                    {
                        DataReceive?.Invoke(null);
                        continue;
                    }
                    DataReceive?.Invoke(responsePacket);
                }
            }
            catch (Exception e)
            {
                if (isConnected)
                {

                    Disconnect();
                    DataReceive?.Invoke(null);
                }

            }
        }

        public void SendRequest(Packet requestPacket)
        {
            try
            {
                if (isConnected && _client.Connected)
                {
                    string jsonRequest = JsonSerializer.Serialize(requestPacket);
                    _writer?.WriteLine(jsonRequest);
                }
            }
            catch (Exception e)
            {
                ErrorData?.Invoke("Dữ liệu không hợp lệ" + e.Message);
            }
        }
        public void Disconnect()
        {
            isConnected = false;
            _reader?.Close();
            _writer?.Close();
            _client?.Close();
        }
    }
}