using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;
using MongoDB.Entities;
using MongoDB.Entities.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VirtaMed.Connect.ConsoleApp.Runner;
using VirtaMed.Data.Entity;
using VirtaMed.Persistence;
using VirtaMed.Persistence.Repository;

namespace VirtaMed.Connect.ConsoleApp
{
    class Program
    {

        private static List<IExecutor> executors = new List<IExecutor>
        {
            //new ClientAuthenticationExecutor(),
            new PasswordAuthenticationExecutor(),
            //new MongoDbSampleExecutor(),
        };

        static async Task Main(string[] args)
        {
            Console.Title = "Console client";
            foreach (var executor in executors)
            {
                try
                {
                    await executor.Execute();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadKey();
                }
            }           
        }
    }
}
