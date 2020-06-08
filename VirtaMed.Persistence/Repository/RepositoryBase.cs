using MongoDB.Driver;

namespace VirtaMed.Persistence.Repository
{
    public abstract class RepositoryBase<TEntity>
    {
        protected abstract string CollectionName { get; }

        public RepositoryBase(MongoClientWrapper clientWrapper)
        {
            this.Collection = clientWrapper.Database.GetCollection<TEntity>(this.CollectionName);
        }

        protected IMongoCollection<TEntity> Collection { get; }
    }


}
