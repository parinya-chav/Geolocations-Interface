using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarPass.Spatial.Services
{
    using CarPass.Spatial.Interface;
    using CarPass.Spatial.Interface.Dto;
    using MongoDB.Driver;
    using CarPass.Spatial.Services.Models;
    using MongoDB.Driver.Builders;

    public class GeolocationsMongoDB : IGeolocations
    {
        public string Server { get; internal set; }
        public int Port { get; internal set; }

        MongoServer mMongo;
        string mDatabase = "spatial";
        string mGeolocations = "spatial";

        public GeolocationsMongoDB()
        {
            mMongo = MongoServer.Create("mongodb://appsit01");

        }

        public GeolocationsMongoDB(string server = "localhost", int port = 27017)
        {
            Server = server;
            Port = port;
        }

        public IList<GeoPointDto> GetLocationsByDeviceSN(string deviceSN, DateTime fromTime, DateTime toTime)
        {
            throw new NotImplementedException();
        }

        public IList<GeoPointDto> GetLocationsByImei(string imei, DateTime fromTime, DateTime toTime)
        {
            IList<GeoPointDto> result = null;
            var database = mMongo.GetDatabase(mDatabase);
            using (mMongo.RequestStart(database))
            {
                var geolocations = database.GetCollection<Geolocation>(mGeolocations);

                var query = Query.And(
                        Query.EQ("Imei", imei),
                        Query.GTE("CreateDate", fromTime),
                        Query.LTE("CreateDate", toTime));

                var geolocationList = geolocations.Find(query).ToList();

                result = geolocationList.ToGeoPointDto();
            }
            return result;
        }
    }
}
