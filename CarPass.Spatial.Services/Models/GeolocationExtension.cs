// -----------------------------------------------------------------------
// <copyright file="GeolocationExtension.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace CarPass.Spatial.Services.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using CarPass.Spatial.Interface.Dto;
    using Newtonsoft.Json;
    using Microsoft.SqlServer.Types;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class GeolocationExtension
    {
        public static IList<GeoPointDto> ToGeoPointDto(this IList<Geolocation> geolocationList)
        {
            List<GeoPointDto> result = new List<GeoPointDto>();

            geolocationList.ToList().ForEach(geoPointMsg =>
            {
                var loc = geoPointMsg.LocationJson;
                var locDoc = loc.Replace("0E-19", "0");
                dynamic locJson = JsonConvert.DeserializeObject(locDoc);
                double lat = locJson.lat;
                double lon = locJson.lon;

                var geoPointDto = new GeoPointDto
                {
                    DeviceSn = geoPointMsg.DeviceSN,
                    UniqueJournyId = (ushort)geoPointMsg.JourneyId,
                    Seq = geoPointMsg.Seq,

                    Latitude = lat,
                    Longitude = lon,

                    Altitude = geoPointMsg.Altitude,
                    Groundspeed = geoPointMsg.Groundspeed,
                    NumberOfSatellitesUsed = (byte)geoPointMsg.NumberOfSatellitesUsed,
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

            for (int i = 1; i < result.Count; i++)
            {
                var g1 = result[i - 1];
                var g2 = result[i];

                Vector2D l = new Vector2D
                {
                    X = (double)g1.Latitude,
                    Y = (double)g1.Longitude,
                };

                Vector2D r = new Vector2D
                {
                    X = (double)g2.Latitude,
                    Y = (double)g2.Longitude,
                };

                double distance = 0.0;
                distance = HarversineHelper.Distance(l, r);

                g2.HavDistanceMeters = distance;
            }

            return result;
        }

    }
}
