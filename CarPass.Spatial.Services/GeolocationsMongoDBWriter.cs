// -----------------------------------------------------------------------
// <copyright file="GeolocationsWriter.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace CarPass.Spatial.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using CarPass.Spatial.Interface;
    using CarPass.Spatial.Interface.Dto;
    using CarPass.Spatial.Services.Models;
    using MongoDB.Driver.Builders;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class GeolocationsMongoDBWriter : MongoDBContext, IGeolocationsWriter
    {
        public GeolocationsMongoDBWriter()
            : base("localhost")
        {
            
        }

        public GeolocationsMongoDBWriter(string server)
            : base(server, 27017)
        {
            
        }

        public GeolocationsMongoDBWriter(string server, int port)
            : base(server, port)
        {
            
        }

        public int SaveGeoPointDto(GeoPointDto geopoint)
        {
            var database = CreateSpatialDatabase();
            using (Mongo.RequestStart(database))
            {
                var geolocations = database.GetCollection<Geolocation>(Geolocations);
                var geolocation = geopoint.ToGeolocation();
                geolocations.Save(geolocation);

                geolocations.EnsureIndex(IndexKeys.GeoSpatial("loc"));
            }
            return 1; 
        }
    }
}
