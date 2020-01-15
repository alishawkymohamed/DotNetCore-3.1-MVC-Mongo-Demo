using BooksApi.Models;
using MongoDB.Driver;

namespace BooksApi.Repos
{
    public class BaseRepo
    {
        private MongoClient client;
        internal IMongoDatabase database;

        public BaseRepo(IBookstoreDatabaseSettings settings)
        {
            client = new MongoClient(settings.ConnectionString);
            database = client.GetDatabase(settings.DatabaseName);
        }
    }
}
