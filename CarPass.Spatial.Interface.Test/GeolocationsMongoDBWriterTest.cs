using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarPass.Spatial.Services;
using CarPass.Spatial.Services.Models;
using CarPass.Spatial.Interface.Dto;

namespace CarPass.Spatial.Interface.Test
{
    using Should.Fluent;

    [TestClass]
    public class GeolocationsMongoDBWriterTest
    {
        IGeolocationsWriter writer;
        IGeolocations reader;
        string server = "localhost";

        [TestInitialize]
        public void SetUp()
        {
            writer = new GeolocationsMongoDBWriter(server);
            reader = new GeolocationsMongoDB(server);
        }

        [TestMethod]
        public void TestSaveDevice_IMEI_13845257385757011()
        {
            var newGeoPointDto = new GeoPointDto
            {
                Id = Guid.NewGuid().ToString(),
                Imei = "13845257385757011",
                DeviceSn = "000010052",
                Latitude = 13.7253520,
                Longitude = 100.5794770,
                HeaderTime = DateTime.UtcNow,
                CreateDate = DateTime.Now,
            };

            var locations = reader.GetLocationsByImei("13845257385757011", new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 1), new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59));
            var count1 = locations.ToList().Count;
            writer.SaveGeoPointDto(newGeoPointDto);
            locations = reader.GetLocationsByImei("13845257385757011", new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 1), new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59));
            var count2 = locations.ToList().Count;

            var ge = count2 > count1;

            ge.Should().Equals(true);
        }
    }
}
