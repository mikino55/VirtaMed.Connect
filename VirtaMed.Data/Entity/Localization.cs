using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Text;

namespace VirtaMed.Data.Entity
{
    [BsonDiscriminator(nameof(EntityType))]
    [BsonKnownTypes(typeof(SimulatorLocalization))]
    public abstract class Localization 
    {
        [BsonId(IdGenerator = typeof(ObjectIdGenerator))]
        public ObjectId Id { get; set; }

        public EntityType EntityType { get; set; }

        public ObjectId EntityObjectId { get; set; }

        public List<LanguageDictionary> Dictionaries { get; set; } = new List<LanguageDictionary>();
    }

    public class SimulatorLocalization : Localization
    {
        public string SimulatorVersion { get; set; }

        public Guid SimulatorIdentifier { get; set; }
    }

    public class LanguageDictionary
    {
        public string CultureName { get; set; }

        public List<LanguageEntry> Entries { get; set; } = new List<LanguageEntry>();
    }

    public class LanguageEntry
    {
        public string LanguageKey { get; set; }

        public string LanguageValue { get; set; }
    }

    public enum EntityType
    { 
        Simulator = 0,
        Course = 1,
    }
}
