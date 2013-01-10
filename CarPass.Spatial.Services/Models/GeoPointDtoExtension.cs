// -----------------------------------------------------------------------
// <copyright file="GeoPointDtoExtension.cs" company="">
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
    using System.Data.SqlClient;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class GeoPointDtoExtension
    {
        public static GeoPointDto ToGeoPointDto(this SqlDataReader rdr)
        {
            var geoPointDto = new GeoPointDto();

            geoPointDto.Id = rdr.GetString(0);
            if (rdr["HeaderTime"] != DBNull.Value)
            {
                geoPointDto.HeaderTime = Convert.ToDateTime(rdr["HeaderTime"]);
            }
            if (rdr["DeviceSn"] != DBNull.Value)
            {
                geoPointDto.DeviceSn = Convert.ToString(rdr["DeviceSn"]);
            }
            if (rdr["JourneyId"] != DBNull.Value)
            {
                geoPointDto.UniqueJournyId = Convert.ToUInt16(rdr["JourneyId"]);
            }
            if (rdr["Seq"] != DBNull.Value)
            {
                geoPointDto.Seq = Convert.ToInt32(rdr["Seq"]);
            }

            if (rdr["Latitude"] != DBNull.Value)
            {
                geoPointDto.Latitude = Convert.ToDouble(rdr["Latitude"]);
            }
            if (rdr["Longitude"] != DBNull.Value)
            {
                geoPointDto.Longitude = Convert.ToDouble(rdr["Longitude"]);
            }

            if (rdr["Altitude"] != DBNull.Value)
            {
                geoPointDto.Altitude = Convert.ToInt32(rdr["Altitude"]);
            }
            if (rdr["Groundspeed"] != DBNull.Value)
            {
                geoPointDto.Groundspeed = Convert.ToInt32(rdr["Groundspeed"]);
            }
            if (rdr["NumberOfSatellitesUsed"] != DBNull.Value)
            {
                geoPointDto.NumberOfSatellitesUsed = Convert.ToByte(rdr["NumberOfSatellitesUsed"]);
            }
            if (rdr["Heading"] != DBNull.Value)
            {
                geoPointDto.Heading = Convert.ToInt32(rdr["Heading"]);
            }
            if (rdr["CreateDate"] != DBNull.Value)
            {
                geoPointDto.CreateDate = Convert.ToDateTime(rdr["CreateDate"]);
            }

            if (rdr["UtcTime"] != DBNull.Value)
            {
                geoPointDto.UtcTime = Convert.ToDateTime(rdr["UtcTime"]);
            }

            geoPointDto.FromDate = geoPointDto.UtcTime.ToLocalTime();
            geoPointDto.ToDate = geoPointDto.UtcTime.ToLocalTime();

            if (rdr["Message"] != DBNull.Value)
            {
                geoPointDto.FromMessage = Convert.ToString(rdr["Message"]);
            }
            if (rdr["MessageJson"] != DBNull.Value)
            {
                geoPointDto.FromMessageJson = Convert.ToString(rdr["MessageJson"]);
            }
            if (rdr["PacketId"] != DBNull.Value)
            {
                geoPointDto.PacketId = Convert.ToString(rdr["PacketId"]);
            }

            if (rdr["MapId"] != DBNull.Value)
            {
                geoPointDto.MapId = Convert.ToInt32(rdr["MapId"]);
            }

            if (rdr["Imei"] != DBNull.Value)
            {
                geoPointDto.Imei = Convert.ToString(rdr["Imei"]);
            }

            return geoPointDto;
        }
    }
}
