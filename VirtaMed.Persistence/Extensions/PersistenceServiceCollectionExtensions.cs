using System;
using System.Collections.Generic;
using System.Text;
using VirtaMed.Persistence.Repository;
using VirtaMed.Persistence;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class PersistenceServiceCollectionExtensions
    {
        public static void AddMongoDBPersistence(this IServiceCollection services)
        {
            services.AddSingleton<MongoClientWrapper>();
            services.AddScoped<SimulatorFamilyRepository>();
            services.AddScoped<SceneRepository>();
            services.AddScoped<SimulatorLocalizationRepository>();
            services.AddScoped<CourseRepository>();
            services.AddSingleton<DataInitializer>();
        }
    }
}
