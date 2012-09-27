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
    using MongoDB.Bson.Serialization.Options;
    using MongoDB.Bson;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Geolocation 
    {
        [BsonId]
        public string Id { get; set; }
        public int Altitude { get; set; }

        /// <summary>
        /// GPS Time From Geopoint
        /// </summary>
        public DateTime UtcTime { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreateDate { get; set; }

        public string PacketId { get; set; }
        public string Imei { get; set; }

        [BsonElement("GroundSpeed")]
        public int Groundspeed { get; set; }
        public decimal Hdop { get; set; }
        public int Heading { get; set; }
        public int JourneyId { get; set; }

        public int Seq { get; set; }
        public string Message { get; set; }
        public string FromState { get; set; }
        public int MapId { get; set; }

        /// <summary>
        /// From Message Header
        /// </summary>
        public DateTime HeaderTime { get; set; }
        public int NumberOfSatellitesUsed { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime LocalUtcTime { get; set; }

        public string MessageJson { get; set; }
        public string DeviceSN { get; set; }

        [BsonElement("loc")]
        public string LocationJson { get; set; }
    }
}
