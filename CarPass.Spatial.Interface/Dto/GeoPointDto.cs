// -----------------------------------------------------------------------
// <copyright file="GeoPoint.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace CarPass.Spatial.Interface.Dto
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Runtime.Serialization;

    [DataContract]
    public class GeoPointDto
    {
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string Imei { get; set; }

        [DataMember]
        public string DeviceSn { get; set; }

        [DataMember]
        public int Seq { get; set; }

        [DataMember]
        public ushort UniqueJournyId { get; set; }

        [DataMember]
        public DateTime HeaderTime { get; set; }

        [DataMember]
        public DateTime UtcTime { get; set; }

        [DataMember]
        public double Latitude { get; set; }

        [DataMember]
        public double Longitude { get; set; }

        [DataMember]
        public int Altitude { get; set; }

        [DataMember]
        public int Groundspeed { get; set; }

        [DataMember]
        public byte NumberOfSatellitesUsed { get; set; }

        [DataMember]
        public int Heading { get; set; }

        [DataMember]
        public DateTime CreateDate { get; set; }

        [DataMember]
        public DateTime FromDate { get; set; }

        [DataMember]
        public DateTime ToDate { get; set; }

        [DataMember]
        public string VehicleState { get; set; }

        [DataMember]
        public string PacketId { get; set; }

        [DataMember]
        public string FromMessage { get; set; }

        [DataMember]
        public string FromMessageJson { get; set; }

        [DataMember]
        public int? MapId { get; set; }

        [DataMember]
        public double HavDistanceMeters { get; set; }
    }
}
