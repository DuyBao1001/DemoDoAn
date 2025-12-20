using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.Intrinsics.Wasm;
using System.Runtime.Intrinsics.X86;
using System.Security.Policy;
using System.Text.Json;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging.Abstractions;
using NewsApp.Common;
using NewsApp.Data;
using NewsApp.Network;

namespace NewsApp.BLL
{
    public class ArticleServices
    {
        private readonly ClientSocket _clientSocket;

        public bool IsConnected
        {
            get { return _clientSocket != null && _clientSocket.IsConnected; }
        }

        public event Action<IList<Article>?>? ListArticleChanged;

        public event Action<Article?>? ArticleDetailChanged;

        public event Action<string>? ErrorData;

        public event Action<bool, string>? DataChanged;

        public event Action<string> ResponseError;

        public event Action<string>? ConnectionStatusChanged;

        public event Action<List<Article>>? GetLatestArticlesResult;
        public event Action<List<Article>>? GetArticlesByCategoryResult;
        public event Action<List<Article>>? SearchArticlesResult;
        public event Action<string>? NotificationReceived;

        public ArticleServices()
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
                case MessageProtocol.ResponseCommand.GET_LIST_ARTICLES_SUCCESS:
                    {
                        IList<Article>? articles = JsonSerializer.Deserialize<IList<Article>>(
                            payload
                        );
                        ListArticleChanged?.Invoke(articles);
                    }
                    break;
                case MessageProtocol.ResponseCommand.GET_LIST_ARTICLES_FAIL:
                    ResponseError?.Invoke("Không thể lấy danh sách bài viết");
                    break;
                case MessageProtocol.ResponseCommand.GET_ARTICLE_DETAIL_SUCCESS:
                    {
                        Article? article = JsonSerializer.Deserialize<Article>(payload);
                        ArticleDetailChanged?.Invoke(article);
                    }
                    break;
                case MessageProtocol.ResponseCommand.GET_ARTICLE_DETAIL_FAIL:
                    ResponseError?.Invoke("Không thể lấy chi tiết bài viết");
                    break;
                case MessageProtocol.ResponseCommand.POST_ARTICLE_SUCCESS:
                    DataChanged?.Invoke(true, "Đăng bài viết đã được đăng lên hệ thống, hãy chờ duyệt");
                    break;
                case MessageProtocol.ResponseCommand.POST_ARTICLE_FAIL:
                    DataChanged?.Invoke(false, "Đăng bài viết thất bại");
                    break;
                case MessageProtocol.ResponseCommand.GET_LATEST_ARTICLES_SUCCESS:
                    {
                        var articles = JsonSerializer.Deserialize<List<Article>>(payload);
                        GetLatestArticlesResult?.Invoke(articles);
                    }
                    break;
                case MessageProtocol.ResponseCommand.GET_LATEST_ARTICLES_FAIL:
                    break;
                case MessageProtocol.ResponseCommand.GET_ARTICLES_BY_CATEGORY_SUCCESS:
                    {
                        var articles = JsonSerializer.Deserialize<List<Article>>(payload);
                        GetArticlesByCategoryResult?.Invoke(articles);
                    }
                    break;
                case MessageProtocol.ResponseCommand.GET_ARTICLES_BY_CATEGORY_FAIL:
                    break;
                case MessageProtocol.ResponseCommand.SEARCH_ARTICLES_SUCCESS:
                    {
                        var articles = JsonSerializer.Deserialize<List<Article>>(payload);
                        SearchArticlesResult?.Invoke(articles);
                    }
                    break;
                case MessageProtocol.ResponseCommand.SEARCH_ARTICLES_FAIL:

                    break;

                default:
                    break;
            }
        }

        // Methods để gửi requests
        public void GetListArticles()
        {
            if (IsConnected)
            {
                Packet requestPacket = new(
                    MessageProtocol.RequestCommand.GET_LIST_ARTICLES,
                    new { }
                );
                _clientSocket.SendRequest(requestPacket);
            }
        }

        public void GetArticleDetail(int articleId)
        {
            if (IsConnected)
            {
                Packet requestPacket = new(
                    MessageProtocol.RequestCommand.GET_ARTICLE_DETAIL,
                    new { articleId }
                );
                _clientSocket.SendRequest(requestPacket);
            }
        }

        public void GetLatestArticles()
        {
            if (IsConnected)
            {
                Packet requestPacket = new(
                    MessageProtocol.RequestCommand.GET_LATEST_ARTICLES,
                    null
                );
                _clientSocket.SendRequest(requestPacket);
            }
        }

        public void GetArticlesByCategory(int categoryId)
        {
            if (IsConnected)
            {
                Packet requestPacket = new(
                    MessageProtocol.RequestCommand.GET_ARTICLES_BY_CATEGORY,
                    categoryId
                );
                _clientSocket.SendRequest(requestPacket);
            }
        }

        public void SearchArticles(string keyword)
        {
            if (IsConnected)
            {
                Packet requestPacket = new(MessageProtocol.RequestCommand.SEARCH_ARTICLES, keyword);
                _clientSocket.SendRequest(requestPacket);
            }
        }

        public void PostArticle(
            string title,
            int categoryId,
            string categoryName,
            string content,
            byte[]? image,
            int authorId,
            string authorName
        )
        {
            var articleData = new
            {
                Title = title,
                CategoryID = categoryId,
                CategoryName = categoryName,
                Content = content,
                Image = image,
                AuthorID = authorId,
                AuthorName = authorName,
                PublishDate = DateTime.Now,
            };

            string payload = JsonSerializer.Serialize(articleData);
            Packet packet = new Packet
            {
                Command = MessageProtocol.RequestCommand.POST_ARTICLE,
                Payload = payload,
            };
            _clientSocket.SendRequest(packet); 
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
