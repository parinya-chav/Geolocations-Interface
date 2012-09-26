using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CarPass.Spatial.Interface.Test
{
    using NMock2;
    using CarPass.Spatial.Interface.Dto;

    [TestClass]
    public class MockeryGeolocationsTest
    {
        private Mockery mocks;
        private IGeolocations mocksGeolocations;

        [TestInitialize]
        public void SetUp()
        {
            mocks = new Mockery();
            mocksGeolocations = mocks.NewMock<IGeolocations>();

            var geipoints = new List<GeoPointDto>();
            geipoints.Add(new GeoPointDto
                {
                    DeviceSn = "000010274",
                    Imei = "352848024123388",
                    Latitude = 0.1M,
                    Longitude = 0.2M,
                });

            Expect.Once.On(mocksGeolocations)
                .Method("GetLocationsByImei")
                .With(Is.Anything, Is.Anything, Is.Anything)
                .Will(Return.Value(geipoints));
        }

        [TestMethod]
        public void TestWithMockery()
        {
            var geopointList = mocksGeolocations.GetLocationsByImei("", DateTime.Now, DateTime.Now);
            geopointList.ToList().ForEach(g =>
            {
                Console.WriteLine("{0}|{1}|{2}", g.Imei, g.Latitude, g.Longitude);
            });
        }

    }
}
