using System;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading;
using NewsApp.BLL;
using NewsApp.Common;

namespace NewsApp.BLL.Network
{
    public class ServerSocket
    {
        private readonly TcpListener _listener;
        private Thread _serverThread;
        private bool _isRunning = false;
        public event Action<string>? OnLog;
        private const int PORT = 8080;

        public ServerSocket()
        {
            _listener = new TcpListener(IPAddress.Any, PORT);
        }
        public void Start()
        {
            try
            {
                _listener.Start();
                _isRunning = true;
                OnLog?.Invoke($"[Server - INFO] Server đã khởi động tại cổng {PORT}");
                _serverThread = new(ListenForClients)
                {
                    IsBackground = true
                };
                _serverThread.Start();
                OnLog?.Invoke($"[Server - INFO] Đang lắng nghe tại cổng {PORT}");
            }
            catch (Exception ex)
            {
                Stop();
                OnLog?.Invoke($"[Server - ERROR] {ex.Message}");
            }
        }

        private void ListenForClients()
        {
            try
            {
                while (_isRunning)
                {
                    if (!_listener.Pending())
                    {
                        Thread.Sleep(100);
                        continue;
                    }
                    TcpClient client = _listener.AcceptTcpClient();
                    HandlerClientRequest handlerClientRequest = new(client, this);
                    OnLog?.Invoke("[Server - INFO] Có client mới kết nối!");
                    Thread clientThread = new(handlerClientRequest.HandleRequest)
                    {
                        IsBackground = true
                    };
                    clientThread.Start();
                }
            }
            catch (Exception ex)
            {
                OnLog?.Invoke($"[Server - ERROR] {ex.Message}");
                Stop();
                OnLog?.Invoke("[Server - INFO] Đã dừng server.");
            }
        }

        private readonly System.Collections.Concurrent.ConcurrentDictionary<string, TcpClient> _connectedUsers = new();

        public bool IsUserOnline(string username)
        {
            return _connectedUsers.ContainsKey(username);
        }

        public void AddUser(string username, TcpClient client)
        {
            _connectedUsers.TryAdd(username, client);
            OnLog?.Invoke($"[Server - INFO] User '{username}' đã đăng nhập.");
        }

        public void RemoveUser(string username)
        {
            if (_connectedUsers.TryRemove(username, out _))
            {
                OnLog?.Invoke($"[Server - INFO] User '{username}' đã đăng xuất.");
            }
        }

        public void Stop()
        {
            _isRunning = false;
            _listener?.Stop();
            _connectedUsers.Clear();
            OnLog?.Invoke("[Server - INFO] Đã dừng server.");
        }
        public void Log(string message)
        {
            OnLog?.Invoke(message);
        }
    }
}