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
            mongoGeolocations = new GeolocationsMongoDB("localhost");
        }

        [Test]
        public void TestGetLocationsByDeviceSN()
        {
            
        }

        [Test]
        public void TestGetLocationsByImei()
        {
            var locations = mongoGeolocations.GetLocationsByImei("13845257385757011", new DateTime(2012, 9, 10, 0, 0, 1), new DateTime(2012, 9, 10, 23, 59, 59));
            var count = locations.ToList().Count;
            Assert.AreNotEqual(0, count);
        }
    }
}
