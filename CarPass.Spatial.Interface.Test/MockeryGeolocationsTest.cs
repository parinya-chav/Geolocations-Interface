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

        [TestMethod]
        public void TestMongoServer()
        {
            //BsonClassMap.RegisterClassMap<Geolocation>();

           // places.EnsureIndex(IndexKeys.GeoSpatial("loca"));
            var mongo = MongoServer.Create("mongodb://localhost");
            var database = mongo.GetDatabase("spatial");

            using (mongo.RequestStart(database))
            {
                var geolocations = database.GetCollection <Geolocation>("geolocations");

                var query = Query.And(
                        Query.EQ("Imei", "13845257385757011"),
                        Query.GTE("CreateDate", new DateTime(2012, 9, 10, 0, 0, 1)),
                        Query.LTE("CreateDate", new DateTime(2012, 9, 10, 23, 59, 59)));

                var c = geolocations.Find(query).ToList();

                c.ForEach(d =>
                {
                    dynamic loc = JsonConvert.DeserializeObject(d.Location);
                    Console.WriteLine("{0}", loc.lat);
                });
            }

        }

        [TestMethod]
        public void TestGeolocationsMongoDB()
        {
            var geolocationsMongoDB = new GeolocationsMongoDB("localhost");
            var locations = geolocationsMongoDB.GetLocationsByImei("13845257385757011", new DateTime(2012, 9, 10, 0, 0, 1), new DateTime(2012, 9, 10, 23, 59, 59));
            var count = locations.ToList().Count;
            Assert.AreNotEqual(0, count);
        }
    }
}
