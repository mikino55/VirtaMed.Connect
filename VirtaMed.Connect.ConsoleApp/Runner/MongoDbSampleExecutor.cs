using MongoDB.Driver;
using MongoDB.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VirtaMed.Data.Entity;
using VirtaMed.Persistence.Repository;
using VirtaMed.Persistence;

namespace VirtaMed.Connect.ConsoleApp.Runner
{
    class MongoDbSampleExecutor : IExecutor
    {
        public async Task Execute()
        {
            string connectionString = "mongodb+srv://mikino55:TrUrJJkqCQkCQXAt@sampleclusterfree-v2efi.azure.mongodb.net/test?retryWrites=true&w=majority";
            MongoDefaults.GuidRepresentation = MongoDB.Bson.GuidRepresentation.Standard;
            //DB db = new DB(
            //    MongoClientSettings.FromConnectionString("mongodb+srv://mikino55:TrUrJJkqCQkCQXAt@sampleclusterfree-v2efi.azure.mongodb.net/test?retryWrites=true&w=majority"),
            //    "VirtaMed");
            //var database = db.GetDatabase();
            //database.DropCollection("Person");

            MongoClientWrapper clientWrapper = new MongoClientWrapper(connectionString);
            //MongoClient client = new MongoClient(connectionString);
            //IMongoDatabase database = client.GetDatabase("VirtaMed");
            //database.CreateCollection("Person");
            //IMongoCollection<SimulatorFamily> collection = database.GetCollection<SimulatorFamily>("SimulatorFamily");
            SimulatorFamilyRepository repository = new SimulatorFamilyRepository(clientWrapper);

            //database.CreateCollection("SimulatorFamily");

            SimulatorFamily family = new SimulatorFamily
            {
                //Identifier = Guid.NewGuid(),
                Identifier = Guid.Parse("177c20f1-e443-4646-af54-5850a481a8ee"),
                NameLanguageKey = "Arthros",
                Simulators = new List<Simulator>
                {
                   new Simulator
                   {
                        Identifier = Guid.Parse("d92b4ae1-680d-48e5-96ee-6acbb91d90cf"),
                        NameLanguageKey = "Arthros",
                        Version = "1.0.0.0",
                        SceneSections = new List<SceneSection>
                        {
                            new SceneSection
                            {
                                Identifier = Guid.NewGuid(),
                                NameLanguageKey = "Section language key",
                            }
                        }
                   }
                }
            };
            //await repository.UpsertAndAddSimulator(family, null);
            //var result = family.Save();          
            //collection.InsertOne(p);
        }
    }
}
