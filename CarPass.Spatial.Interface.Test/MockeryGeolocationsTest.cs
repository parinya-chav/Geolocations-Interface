﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CarPass.Spatial.Interface.Test
{
    using NMock2;
    using CarPass.Spatial.Interface.Dto;
    using CarPass.Spatial.Services.Models;
    using MongoDB.Driver;
    using MongoDB.Driver.Builders;
    using MongoDB.Bson.Serialization;
    using Newtonsoft.Json;
    using CarPass.Spatial.Services;

    using Should.Fluent;

    [TestClass]
    public class MockeryGeolocationsTest
    {
        private Mockery mocks;
        private IGeolocations mocksGeolocations;
        string server = "appsit01";

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
                    Latitude = 0.1,
                    Longitude = 0.2,
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

        [TestMethod]
        public void TestMongoServer()
        {
            //BsonClassMap.RegisterClassMap<Geolocation>();

           // places.EnsureIndex(IndexKeys.GeoSpatial("loca"));
            var mongo = MongoServer.Create("mongodb://" + server);
            var database = mongo.GetDatabase("spatial");

            using (mongo.RequestStart(database))
            {
                var geolocations = database.GetCollection <Geolocation>("geolocations");

                var query = Query.And(
                        Query.EQ("Imei", "13845257385757011"),
                        Query.GTE("CreateDate", new DateTime(2012, 9, 10, 0, 0, 1)),
                        Query.LTE("CreateDate", new DateTime(2012, 9, 10, 23, 59, 59)));

                var c = geolocations.Find(query).ToList();
            }

        }

        [TestMethod]
        public void CheckCount_GetLocationsByImei()
        {
            var geolocationsMongoDB = new GeolocationsMongoDB(server);
            var locations = geolocationsMongoDB.GetLocationsByImei("13845257385757011", new DateTime(2012, 9, 10, 0, 0, 1), new DateTime(2012, 9, 10, 23, 59, 59));
            var count = locations.ToList().Count;

            count.Should().Not.Equal(0);
        }

        [TestMethod]
        public void CheckCount_GetLocationsByDeviceSN()
        {
            var geolocationsMongoDB = new GeolocationsMongoDB(server);
            var locations = geolocationsMongoDB.GetLocationsByImei("352848024123388", new DateTime(2012, 10, 1, 0, 0, 1), new DateTime(2012, 10, 1, 23, 59, 59));
            var totalCount = locations.ToList().Count;
            var onlyGeoPointCount = locations.Where( g => g.FromMessage == "GeoPoint").ToList().Count;

            totalCount.Should().Not.Equal(0);
            (totalCount >= onlyGeoPointCount).Should().Equal(true);
        }

        [TestMethod]
        public void FixFormatGeolocation_GetLocationsByDeviceSN()
        {
            var geolocationsMongoDB = new GeolocationsMongoDB(server);
            var locations = geolocationsMongoDB.GetLocationsByDeviceSN("000010274",
                new DateTime(2010, 7, 1, 0, 0, 1), new DateTime(2012, 9, 28, 23, 59, 59));
            var count = locations.ToList().Count;
            count.Should().Not.Equal(0);
            Console.WriteLine("Count: {0}", count);

            double distance = 0;
            locations.ToList().ForEach(l =>
            {
                l.HavDistanceMeters.Should().Be.InRange(0.0, double.MaxValue);
                l.Latitude.Should().Be.AssignableFrom(typeof(double));
                l.Longitude.Should().Be.AssignableFrom(typeof(double));

                distance += l.HavDistanceMeters;
            });

            for (int i = 1; i < locations.Count; i++)
            {
                var l = locations[i - 1];
                var r = locations[i];
                var diff = r.HeaderTime - l.HeaderTime;
                diff.Ticks.Should().Be.InRange(default(long), long.MaxValue);
            }

            Console.WriteLine("HavDistanceMeters: {0}", distance);
        }

    }
}
