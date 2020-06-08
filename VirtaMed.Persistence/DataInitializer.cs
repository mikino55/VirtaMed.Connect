using System;
using System.Collections.Generic;
using System.Text;
using VirtaMed.Data.Entity;

namespace VirtaMed.Persistence
{
    public class DataInitializer
    {
        private readonly MongoClientWrapper clientWrapper;

        public DataInitializer(MongoClientWrapper clientWrapper)
        {
            this.clientWrapper = clientWrapper;
        }

        public void Initialize()
        {
            var collection = this.clientWrapper.Database.GetCollection<CourseEntity>("Course");
            if (collection == null)
            {
                this.clientWrapper.Database.CreateCollection("Course");
            }
        }

        public void SeedData()
        { 
        
        }
    }
}
