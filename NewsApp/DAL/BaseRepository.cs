using Microsoft.Data.SqlClient;

namespace NewsApp.DAL
{
    public abstract class BaseRepository<T> where T : class
    {
        protected string connectionString;
        public string? LastError { get; protected set; }

        public BaseRepository()
        {
            connectionString = DatabaseHepper.ConnectionString;
        }

        protected SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
        public abstract IList<T> GetAll();

        public abstract T? GetByID(int id);
        public abstract bool Save(T obj);
        public abstract bool Update(T obj);
        public abstract bool Delete(T obj);
    }
}
