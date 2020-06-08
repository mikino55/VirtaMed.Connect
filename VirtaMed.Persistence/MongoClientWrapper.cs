using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace VirtaMed.Persistence
{
    public class MongoClientWrapper
    {
        #region Private Fields

        public IMongoDatabase Database { get; }

        #endregion Private Fields

        #region Public Constructors
        public MongoClientWrapper(string connectionString)
        {
            MongoDefaults.GuidRepresentation = GuidRepresentation.Standard;
            this.Client = new MongoClient(connectionString);
            this.Database = Client.GetDatabase("VirtaMed");
        }

        public MongoClientWrapper(IConfiguration configuration)
            : this(configuration.GetConnectionString("VirtaMed.AtlasDB"))
        {
            //string connectionString = configuration.GetConnectionString("VirtaMed.AtlasDB");
            //MongoDefaults.GuidRepresentation = GuidRepresentation.Standard;
            //this.Client = new MongoClient(connectionString);
            //this.database = this.Client.GetDatabase("VirtaMed");
        }

        #endregion Public Constructors

        #region Public Properties

        public MongoClient Client { get; }

        #endregion Public Properties

       
    }
}
