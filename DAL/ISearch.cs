

namespace NewsApp.DAL
{
    internal interface ISearch<T> where T : class
    {
        public IList<T> SearchByTitle(string title);
    }

}
