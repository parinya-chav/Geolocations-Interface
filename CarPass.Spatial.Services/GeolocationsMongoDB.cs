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

    public class GeolocationsMongoDB :MongoDBContext, IGeolocations
    {

        public GeolocationsMongoDB()
            : base("localhost")
        {
            
        }

        public GeolocationsMongoDB(string server)
            : base(server, 27017)
        {
            
        }

        public GeolocationsMongoDB(string server, int port)
            : base(server, port)
        {
            
        }

        public IList<GeoPointDto> GetLocationsByDeviceSN(string deviceSN, DateTime fromTime, DateTime toTime)
        {
            IList<GeoPointDto> result = null;
            var database = mMongo.GetDatabase(mDatabase);
            using (mMongo.RequestStart(database))
            {
                var geolocations = database.GetCollection<Geolocation>(mGeolocations);

                var query = Query.And(
                        Query.EQ("DeviceSN", deviceSN),
                        Query.GTE("CreateDate", fromTime),
                        Query.LTE("CreateDate", toTime));

                var geolocationList = geolocations.Find(query).ToList();

                result = geolocationList.ToGeoPointDto();
            }
            return result;
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
