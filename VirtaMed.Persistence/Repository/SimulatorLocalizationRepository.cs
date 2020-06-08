using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtaMed.Data.Entity;
using MongoDB.Driver.Linq;

namespace VirtaMed.Persistence.Repository
{
    public class SimulatorLocalizationRepository : RepositoryBase<SimulatorLocalization>
    {
        public SimulatorLocalizationRepository(MongoClientWrapper clientWrapper) : base(clientWrapper)
        {
        }

        protected override string CollectionName => "Localization";

        public async Task InsertOne(SimulatorLocalization localization, IClientSessionHandle session = null)
        {
            var dbLocalization = await this.Collection.AsQueryable()
                                                .FirstOrDefaultAsync(x => x.SimulatorIdentifier == localization.SimulatorIdentifier && x.SimulatorVersion.Equals(localization.SimulatorVersion));

            if (dbLocalization == null)
            {
                await (session == null ? this.Collection.InsertOneAsync(localization) : this.Collection.InsertOneAsync(session, localization));
            }
            else
            {
                var filter = Builders<SimulatorLocalization>.Filter.Eq("_id", dbLocalization.Id);
                var update = Builders<SimulatorLocalization>.Update.PushEach(nameof(SimulatorLocalization.Dictionaries), localization.Dictionaries);
                var updateResult = await (session == null ? this.Collection.UpdateOneAsync(filter, update) : this.Collection.UpdateOneAsync(session, filter, update));
            }
        }


        public Task<bool> Exists(Guid simulatorIdentifier, string simulatorVersion, IEnumerable<string> cultures)
        {
            return this.Collection
                .Find(x => x.SimulatorIdentifier == simulatorIdentifier
                           && x.SimulatorVersion.Equals(simulatorVersion)
                           && x.Dictionaries.Any(x => cultures.Contains(x.CultureName)))
                .AnyAsync();
        }
    }
}
