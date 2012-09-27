// -----------------------------------------------------------------------
// <copyright file="MongoDBContext.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace CarPass.Spatial.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using MongoDB.Driver;

    /// <summary>
    /// MongoDB Environment.
    /// </summary>
    public abstract class MongoDBContext
    {
        public string Server { get; internal set; }
        public int Port { get; internal set; }

        public MongoServer Mongo { get; internal set; }

        private string mDatabase = "spatial";
        protected string mGeolocations = "geolocations";

        public MongoDBContext()
            : this("localhost")
        {
            
        }

        public MongoDBContext(string server)
            : this(server, 27017)
        {
            
        }

        public MongoDBContext(string server, int port)
        {
            Server = server;
            Port = port;
            Mongo = MongoServer.Create("mongodb://" + Server);
        }

        public MongoDatabase CreateSpatialDatabase()
        {
            var database = Mongo.GetDatabase(mDatabase);
            return database;
        }
    }
}
