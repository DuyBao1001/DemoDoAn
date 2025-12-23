
namespace NewsApp.Common
{
    public static class MessageProtocol
    {
        public static class RequestCommand
        {
            public const string LOGIN = "LOGIN";
            public const string REGISTER = "REGISTER";
            public const string FORGOT_PASSWORD = "FORGOT_PASSWORD";
            public const string RESET_PASSWORD = "RESET_PASSWORD";
            public const string GET_LIST_ARTICLES = "GET_LIST_ARTICLES";

            public const string GET_ARTICLE_DETAIL = "GET_ARTICLE_DETAIL";

            public const string GET_PROFILE = "GET_PROFILE";
            public const string UPDATE_PROFILE = "UPDATE_PROFILE";

            public const string POST_ARTICLE = "POST_ARTICLE";

            public const string POST_COMMENT = "POST_COMMENT";
            public const string LOGOUT = "LOGOUT";
            public const string GET_CATEGORIES = "GET_CATEGORIES";
            public const string GET_LATEST_ARTICLES = "GET_LATEST_ARTICLES";
            public const string GET_ARTICLES_BY_CATEGORY = "GET_ARTICLES_BY_CATEGORY";
            public const string SEARCH_ARTICLES = "SEARCH_ARTICLES";
            public const string GET_COMMENTS = "GET_COMMENTS";
            public const string CHANGE_PASSWORD = "CHANGE_PASSWORD";

            // Admin Commands
            public const string GET_ALL_USERS = "GET_ALL_USERS";
            public const string DELETE_USER = "DELETE_USER";
            public const string DELETE_ARTICLE = "DELETE_ARTICLE";
            public const string ADD_CATEGORY = "ADD_CATEGORY";
            public const string UPDATE_CATEGORY = "UPDATE_CATEGORY";
            public const string DELETE_CATEGORY = "DELETE_CATEGORY";

            public const string GET_PENDING_ARTICLES = "GET_PENDING_ARTICLES";
            public const string APPROVE_ARTICLE = "APPROVE_ARTICLE";
        }

        public static class ResponseCommand
        {
            public const string LOGIN_SUCCESS = "LOGIN_SUCCESS";
            public const string REGISTER_SUCCESS = "REGISTER_SUCCESS";
            public const string FORGOT_PASSWORD_SUCCESS = "FORGOT_PASSWORD_SUCCESS";
            public const string FORGOT_PASSWORD_FAIL = "FORGOT_PASSWORD_FAIL";

            public const string RESET_PASSWORD_SUCCESS = "RESET_PASSWORD_SUCCESS";
            public const string RESET_PASSWORD_FAIL = "RESET_PASSWORD_FAIL";

            public const string GET_LIST_ARTICLES_SUCCESS = "GET_LIST_ARTICLES_SUCCESS";
            public const string LOGIN_FAIL = "LOGIN_FAIL";
            public const string REGISTER_FAIL = "REGISTER_FAIL";
            public const string GET_LIST_ARTICLES_FAIL = "GET_LIST_ARTICLES_FAIL";

            public const string GET_ARTICLE_DETAIL_SUCCESS = "GET_ARTICLE_DETAIL_SUCCESS";
            public const string GET_ARTICLE_DETAIL_FAIL = "GET_ARTICLE_DETAIL_FAIL";

            public const string GET_PROFILE_SUCCESS = "GET_PROFILE_SUCCESS";
            public const string GET_PROFILE_FAIL = "GET_PROFILE_FAIL";

            public const string UPDATE_PROFILE_SUCCESS = "UPDATE_PROFILE_SUCCESS";
            public const string UPDATE_PROFILE_FAIL = "UPDATE_PROFILE_FAIL";

            public const string POST_ARTICLE_SUCCESS = "POST_ARTICLE_SUCCESS";
            public const string POST_ARTICLE_FAIL = "POST_ARTICLE_FAIL";

            public const string POST_COMMENT_SUCCESS = "POST_COMMENT_SUCCESS";
            public const string POST_COMMENT_FAIL = "POST_COMMENT_FAIL";

            public const string GET_CATEGORIES_SUCCESS = "GET_CATEGORIES_SUCCESS";
            public const string GET_CATEGORIES_FAIL = "GET_CATEGORIES_FAIL";

            public const string GET_LATEST_ARTICLES_SUCCESS = "GET_LATEST_ARTICLES_SUCCESS";
            public const string GET_LATEST_ARTICLES_FAIL = "GET_LATEST_ARTICLES_FAIL";

            public const string GET_ARTICLES_BY_CATEGORY_SUCCESS = "GET_ARTICLES_BY_CATEGORY_SUCCESS";
            public const string GET_ARTICLES_BY_CATEGORY_FAIL = "GET_ARTICLES_BY_CATEGORY_FAIL";

            public const string SEARCH_ARTICLES_SUCCESS = "SEARCH_ARTICLES_SUCCESS";
            public const string SEARCH_ARTICLES_FAIL = "SEARCH_ARTICLES_FAIL";

            public const string GET_COMMENTS_SUCCESS = "GET_COMMENTS_SUCCESS";
            public const string GET_COMMENTS_FAIL = "GET_COMMENTS_FAIL";

            public const string CHANGE_PASSWORD_SUCCESS = "CHANGE_PASSWORD_SUCCESS";
            public const string CHANGE_PASSWORD_FAIL = "CHANGE_PASSWORD_FAIL";


            // Admin Response Commands
            public const string GET_ALL_USERS_SUCCESS = "GET_ALL_USERS_SUCCESS";
            public const string GET_ALL_USERS_FAIL = "GET_ALL_USERS_FAIL";

            public const string DELETE_USER_SUCCESS = "DELETE_USER_SUCCESS";
            public const string DELETE_USER_FAIL = "DELETE_USER_FAIL";

            public const string DELETE_ARTICLE_SUCCESS = "DELETE_ARTICLE_SUCCESS";
            public const string DELETE_ARTICLE_FAIL = "DELETE_ARTICLE_FAIL";

            public const string ADD_CATEGORY_SUCCESS = "ADD_CATEGORY_SUCCESS";
            public const string ADD_CATEGORY_FAIL = "ADD_CATEGORY_FAIL";

            public const string UPDATE_CATEGORY_SUCCESS = "UPDATE_CATEGORY_SUCCESS";
            public const string UPDATE_CATEGORY_FAIL = "UPDATE_CATEGORY_FAIL";

            public const string DELETE_CATEGORY_SUCCESS = "DELETE_CATEGORY_SUCCESS";
            public const string DELETE_CATEGORY_FAIL = "DELETE_CATEGORY_FAIL";

            public const string GET_PENDING_ARTICLES_SUCCESS = "GET_PENDING_ARTICLES_SUCCESS";
            public const string GET_PENDING_ARTICLES_FAIL = "GET_PENDING_ARTICLES_FAIL";

            public const string APPROVE_ARTICLE_SUCCESS = "APPROVE_ARTICLE_SUCCESS";
            public const string APPROVE_ARTICLE_FAIL = "APPROVE_ARTICLE_FAIL";




        }
    }
}