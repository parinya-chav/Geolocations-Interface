using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarPass.Spatial.Services
{
    using CarPass.Spatial.Interface;
    using CarPass.Spatial.Interface.Dto;

    public class GeolocationsMongoDB : IGeolocations
    {
        public string Server { get; internal set; }
        public int Port { get; internal set; }


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
            throw new NotImplementedException();
        }
    }
}
