using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Entities.Core;
using System;
using System.Collections.Generic;
using System.Text;
using VirtaMed.Data.Entity;

namespace VirtaMed.Data.Entity
{
    public class SimulatorFamily : MetaEntityBase
    {
        public List<Simulator> Simulators { get; set; } = new List<Simulator>();
    }

    public class Simulator : EntityBase
    {
        public string Version { get; set; }

        public List<SceneSection> SceneSections { get; set; } = new List<SceneSection>();
    }

    public class SceneSection : EntityBase
    {
        public List<ObjectId> SceneIdentifiers { get; set; } = new List<ObjectId>();
    }

    public class Scene : MetaEntityBase
    {
        public List<ReportSection> ReportSections { get; set; } = new List<ReportSection>();

        public Guid SimulatorIdentifier { get; set; }

        public string SimulatorVersion { get; set; }
    }

    public class ReportSection : EntityBase
    {
        public List<ReportItem> ReportItems { get; set; } = new List<ReportItem>();
    }

    public class ReportItem : EntityBase
    {
        public int MaxScore { get; set; }
    }
}
