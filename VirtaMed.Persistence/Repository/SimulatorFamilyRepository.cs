using MongoDB.Entities;
using System;
using System.Threading.Tasks;
using VirtaMed.Data.Entity;
using MongoDB.Driver.Linq;
using MongoDB.Driver;
using System.Linq;
using System.Collections.Generic;

namespace VirtaMed.Persistence.Repository
{
    public class SimulatorFamilyRepository : RepositoryBase<SimulatorFamily>
    {
        private bool simulator;

        public SimulatorFamilyRepository(MongoClientWrapper clientWrapper) : base(clientWrapper)
        {
        }

        protected override string CollectionName => "SimulatorFamily";

        public async Task UpsertAndAddSimulator(SimulatorFamily family, Simulator simulator, IClientSessionHandle session)
        {
            var dbFamily = await this.Collection.AsQueryable()
                                                .FirstOrDefaultAsync(x => x.Identifier == family.Identifier);
            if (dbFamily == null)
            {
                family.Simulators.Add(simulator);
                await this.Collection.InsertOneAsync(session, family);
            }
            else
            {
                // TODO maybe add sanity check again
                var filter = Builders<SimulatorFamily>.Filter.Eq("_id", dbFamily.Id);
                var update = Builders<SimulatorFamily>.Update.Push(nameof(SimulatorFamily.Simulators), simulator);
                var updateResult = await this.Collection.UpdateOneAsync(session, filter, update);
            }
        }

        public Task<bool> SimulatorExists(Guid simulatorIdentifier, string simulatorVersion)
        {
            //var simulator =
            //    (from sf in this.Collection.AsQueryable()
            //     from s in sf.Simulators
            //     where s.Identifier == simulatorIdentifier
            //     select s).Any();

            return this.Collection
                .Find(sf => sf.Simulators.Any(s => s.Identifier == simulatorIdentifier && s.Version.ToLower() == simulatorVersion.ToLower()))
                .AnyAsync();
        }
    }

    public class SceneRepository : RepositoryBase<Scene>
    {
        public SceneRepository(MongoClientWrapper clientWrapper) : base(clientWrapper)
        {
        }

        protected override string CollectionName => "Scene";

        public Task InsertMany(IEnumerable<Scene> scenes, IClientSessionHandle session)
        {
            return this.Collection.InsertManyAsync(session, scenes);
        }

        public Task InsertOne(Scene scene, IClientSessionHandle session)
        {
            return this.Collection.InsertOneAsync(session, scene);
        }
    }
}
