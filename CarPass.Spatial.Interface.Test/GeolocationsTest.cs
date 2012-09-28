using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace CarPass.Spatial.Interface.Test
{
    using MbUnit.Framework;
    using CarPass.Spatial.Services;

    using Should.Fluent;
    public class GeolocationsTest
    {
        private IGeolocations mongoGeolocations;
        string server = "localhost";

        [SetUp]
        public void SetUp()
        {
            mongoGeolocations = new GeolocationsMongoDB(server);
        }

        [Test]
        public void CheckCount_GetLocationsByDeviceSN()
        {
            var locations = mongoGeolocations.GetLocationsByDeviceSN("000010052", new DateTime(2012, 9, 10, 0, 0, 1), new DateTime(2012, 9, 10, 23, 59, 59));
            var count = locations.ToList().Count;

            count.Should().Not.Equal(0);
        }

        [Test]
        public void CheckCount_GetLocationsByImei()
        {
            var locations = mongoGeolocations.GetLocationsByImei("13845257385757011", new DateTime(2012, 9, 10, 0, 0, 1), new DateTime(2012, 9, 10, 23, 59, 59));
            var count = locations.ToList().Count;

            count.Should().Not.Equal(0);
        }

        [Test]
        public void FixFormatGeolocation_GetLocationsByDeviceSN()
        {
            var locations = mongoGeolocations.GetLocationsByDeviceSN("000010274",
                new DateTime(2012, 9, 19, 0, 0, 1), new DateTime(2012, 9, 19, 23, 59, 59));
            var count = locations.ToList().Count;
            count.Should().Not.Equal(0);

            locations.ToList().ForEach(l =>
            {
                l.HavDistanceMeters.Should().Be.InRange(0.0, double.MaxValue);
                l.Latitude.Should().Be.AssignableFrom(typeof(double));
                l.Longitude.Should().Be.AssignableFrom(typeof(double));
            });

            for (int i = 1; i < locations.Count; i++)
            {
                var l = locations[i - 1];
                var r = locations[i];
                var diff = r.HeaderTime - l.HeaderTime;
                diff.Ticks.Should().Be.InRange(default(long), long.MaxValue);
            }
        }
    }
}
