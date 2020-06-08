using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VirtaMed.Data.Entity;

namespace VirtaMed.Persistence.Repository
{
    public class CourseRepository : RepositoryBase<CourseEntity>
    {
        public CourseRepository(MongoClientWrapper clientWrapper) : base(clientWrapper)
        {
        }

        protected override string CollectionName => "Course";

        public Task InsertOneAsync(CourseEntity course)
        {
            return this.Collection.InsertOneAsync(course);
        }
    }
}
