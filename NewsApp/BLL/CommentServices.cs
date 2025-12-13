using NewsApp.Common;
using NewsApp.Network;
using NewsApp.Data;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace NewsApp.BLL
{
    public class CommentServices
    {
        private readonly ClientSocket _clientSocket;

        public bool IsConnected { get { return _clientSocket != null && _clientSocket.IsConnected; } }

        public event Action<bool, string>? DataChanged;
        public event Action<List<Comment>>? GetCommentsResult;
        public event Action<string>? ResponseError;
        public event Action<string>? ConnectionStatusChanged;
        public event Action<string>? ErrorData;

        public CommentServices()
        {
            _clientSocket = new ClientSocket();
            _clientSocket.Start();

            _clientSocket.DataReceive += HandleDataReceive;
            _clientSocket.ConnectionStatusChanged += HandleConnectionStatusChanged;
            _clientSocket.ErrorData += HandleErrorData;
        }

        private void HandleDataReceive(Packet? packet)
        {
            if (packet == null)
            {
                ResponseError?.Invoke("Không nhận được phản hồi từ server");
                return;
            }

            string command = packet.Command;
            string payload = packet.Payload;

            switch (command)
            {
                case MessageProtocol.ResponseCommand.POST_COMMENT_SUCCESS:
                    DataChanged?.Invoke(true, "Bình luận thành công");
                    break;
                case MessageProtocol.ResponseCommand.POST_COMMENT_FAIL:
                    string errorMessage = "Bình luận thất bại";
                    try
                    {
                        errorMessage = JsonSerializer.Deserialize<string>(payload) ?? "Bình luận thất bại";
                    }
                    catch { }
                    DataChanged?.Invoke(false, errorMessage);
                    break;
                case MessageProtocol.ResponseCommand.GET_COMMENTS_SUCCESS:
                    try
                    {
                        var comments = JsonSerializer.Deserialize<List<Comment>>(payload);
                        GetCommentsResult?.Invoke(comments ?? []);
                    }
                    catch
                    {
                        GetCommentsResult?.Invoke([]);
                    }
                    break;
                case MessageProtocol.ResponseCommand.GET_COMMENTS_FAIL:
                    GetCommentsResult?.Invoke([]);
                    break;
                default:
                    break;
            }
        }

        public void PostComment(int articleId, int userId, string content)
        {
            if (IsConnected)
            {
                var commentData = new { ArticleID = articleId, UserID = userId, Content = content };
                Packet requestPacket = new(MessageProtocol.RequestCommand.POST_COMMENT, commentData);
                _clientSocket.SendRequest(requestPacket);
            }
        }

        public void GetComments(int articleId)
        {
            if (IsConnected)
            {
                var data = new { ArticleID = articleId };
                Packet requestPacket = new(MessageProtocol.RequestCommand.GET_COMMENTS, data);
                _clientSocket.SendRequest(requestPacket);
            }
        }

        private void HandleConnectionStatusChanged(string message)
        {
            ConnectionStatusChanged?.Invoke(message);
        }

        private void HandleErrorData(string message)
        {
            ErrorData?.Invoke(message);
        }
    }
}
