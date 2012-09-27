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

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class GeolocationExtension
    {
        public static IList<GeoPointDto> ToGeoPointDto(this IList<Geolocation> geolocationList)
        {
            IList<GeoPointDto> result = new List<GeoPointDto>();

            geolocationList.ToList().ForEach(geoPointMsg =>
            {
                //dynamic loc = JsonConvert.DeserializeObject<dynamic>(geoPointMsg.Location);

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
            return result;
        }

    }
}
