// -----------------------------------------------------------------------
// <copyright file="Geolocation.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace CarPass.Spatial.Services.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using MongoDB.Bson.Serialization.Attributes;
    using MongoDB.Bson.Serialization;
    using CarPass.Spatial.Interface.Dto;
    using Newtonsoft.Json;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Geolocation 
    {
        [BsonId]
        public string Id { get; set; }
        public short Altitude { get; set; }

        /// <summary>
        /// GPS Time From Geopoint
        /// </summary>
        public DateTime UtcTime { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreateDate { get; set; }

        public string PacketId { get; set; }
        public string Imei { get; set; }
        public ushort GroundSpeed { get; set; }
        public decimal Hdop { get; set; }
        public ushort Heading { get; set; }
        public int JourneyId { get; set; }

        public int Seq { get; set; }
        public string Message { get; set; }
        public string FromState { get; set; }
        public int MapId { get; set; }

        /// <summary>
        /// From Message Header
        /// </summary>
        public DateTime HeaderTime { get; set; }
        public byte NumberOfSatellitesUsed { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime LocalUtcTime { get; set; }

        public string MessageJson { get; set; }
        public string DeviceSN { get; set; }

        [BsonElement("loc")]
        public string Location { get; internal set; }
    }

    public static class GeolocationExtension
    {
        public static IList<GeoPointDto> ToGeoPointDto(this IList<Geolocation> geolocationList)
        {
            IList<GeoPointDto> result = new List<GeoPointDto>();

            geolocationList.ToList().ForEach(geoPointMsg =>
            {

                dynamic loc = JsonConvert.DeserializeObject(geoPointMsg.Location);

                var geoPointDto = new GeoPointDto
                {
                    DeviceSn = geoPointMsg.DeviceSN,
                    UniqueJournyId = (ushort)geoPointMsg.JourneyId,
                    Seq = geoPointMsg.Seq,

                    Latitude = loc.lat,
                    Longitude = loc.lon,

                    Altitude = geoPointMsg.Altitude,
                    Groundspeed = geoPointMsg.GroundSpeed,
                    NumberOfSatellitesUsed = geoPointMsg.NumberOfSatellitesUsed,
                    Heading = geoPointMsg.Heading,
                    CreateDate = geoPointMsg.CreateDate,

                    UtcTime = geoPointMsg.UtcTime,
                    FromDate = geoPointMsg.UtcTime.ToLocalTime(),
                    ToDate = geoPointMsg.UtcTime.ToLocalTime(),

                    FromMessage = geoPointMsg.Message,
                    FromMessageJson = geoPointMsg.MessageJson,

                    MapId = geoPointMsg.MapId,

                };

                result.Add(geoPointDto);
            });
            return result;
        }

    }
}
