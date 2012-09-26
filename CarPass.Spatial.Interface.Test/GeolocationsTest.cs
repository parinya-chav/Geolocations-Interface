using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace CarPass.Spatial.Interface.Test
{
    using MbUnit.Framework;
    using CarPass.Spatial.Services;

    public class GeolocationsTest
    {
        private IGeolocations mongoGeolocations;

        [SetUp]
        public void SetUp()
        {
            mongoGeolocations = new GeolocationsMongoDB("appsit01");

        }

        [Test]
        public void TestGetLocationsByDeviceSN()
        {
            
        }

        [Test]
        public void TestGetLocationsByImei()
        {

        }
    }
}
