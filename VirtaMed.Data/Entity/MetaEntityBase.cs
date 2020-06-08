using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Entities.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace VirtaMed.Data.Entity
{
    public abstract class MetaEntityBase : EntityBase
    {
        //[BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        [BsonId(IdGenerator = typeof(ObjectIdGenerator))]
        public ObjectId Id { get; set; }

    }

    public abstract class EntityBase
    {
        public string NameLanguageKey { get; set; }

        public Guid Identifier { get; set; }
    }
}
