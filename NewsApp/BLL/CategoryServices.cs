using NewsApp.Common;
using NewsApp.Data;
using NewsApp.Network;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace NewsApp.BLL
{
    public class CategoryServices
    {
        private readonly ClientSocket _clientSocket;

        public bool IsConnected { get { return _clientSocket != null && _clientSocket.IsConnected; } }

        public event Action<List<Category>>? GetCategoriesResult;
        public event Action<string>? ErrorData;

        public CategoryServices()
        {
            _clientSocket = new ClientSocket();
            _clientSocket.Start();
            _clientSocket.DataReceive += HandleDataReceive;
        }

        private void HandleDataReceive(Packet? packet)
        {
            if (packet == null) return;

            string command = packet.Command;
            string payload = packet.Payload;

            switch (command)
            {
                case MessageProtocol.ResponseCommand.GET_CATEGORIES_SUCCESS:
                    {
                        List<Category>? categories = JsonSerializer.Deserialize<List<Category>>(payload);
                        if (categories != null)
                        {
                            GetCategoriesResult?.Invoke(categories);
                        }
                    }
                    break;
                case MessageProtocol.ResponseCommand.GET_CATEGORIES_FAIL:
                    ErrorData?.Invoke("Lỗi lấy danh sách danh mục");
                    break;
            }
        }

        public void GetCategories()
        {
            if (IsConnected)
            {
                Packet requestPacket = new(MessageProtocol.RequestCommand.GET_CATEGORIES, null);
                _clientSocket.SendRequest(requestPacket);
            }
        }
    }
}
