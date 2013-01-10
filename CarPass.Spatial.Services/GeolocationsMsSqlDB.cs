// -----------------------------------------------------------------------
// <copyright file="GeolocationsMsSqlDB.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace CarPass.Spatial.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using CarPass.Spatial.Interface;
    using CarPass.Spatial.Interface.Dto;
    using System.Data.SqlClient;
    using System.Data;
    using CarPass.Spatial.Services.Models;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class GeolocationsMsSqlDB : IGeolocations
    {
        string _dbServer = string.Empty;
        string _strConnString = string.Empty;

        public GeolocationsMsSqlDB(string dbServer, string username, string password)
        {
            _strConnString = string.Format(
                "Data Source={0};Initial Catalog={1};Integrated Security=False;User ID={2};Password={3};",
                dbServer, "CommunicationDatabase",
                username, password);
        }

        public IList<GeoPointDto> GetLocationsByDeviceSN(string deviceSN, DateTime fromTime, DateTime toTime)
        {
            IList<GeoPointDto> results = new List<GeoPointDto>();
            using (SqlConnection objConn = new SqlConnection(_strConnString))
            {
                objConn.Open();

                using (SqlCommand cmd = objConn.CreateCommand())
                {

                    string query = @"SELECT * FROM GeoPointHarversines
                WHERE Message = 'GeoPoint' ";
                    query += "AND DeviceSN = @deviceSN ";
                    query += "AND UtcTime BETWEEN @fromTime AND @toTime";

                    cmd.CommandText = query;

                    DateTime utcFromTime = fromTime.ToUniversalTime();
                    DateTime utcToTime = toTime.ToUniversalTime();

                    cmd.Parameters.AddWithValue("deviceSN", deviceSN);
                    cmd.Parameters.AddWithValue("fromTime", utcFromTime);
                    cmd.Parameters.AddWithValue("toTime", utcToTime);

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        IList<Geolocation> geolocationList = new List<Geolocation>();
                        while (rdr.Read())
                        {
                            var geoPointDto = rdr.ToGeoPointDto();
                            results.Add(geoPointDto);
                        }
                    }
                }
            }
            return results;
        }

        public IList<GeoPointDto> GetLocationsByImei(string imei, DateTime fromTime, DateTime toTime)
        {
            IList<GeoPointDto> results = new List<GeoPointDto>();
            using (SqlConnection objConn = new SqlConnection(_strConnString))
            {
                objConn.Open();
                using (SqlCommand cmd = objConn.CreateCommand())
                {
                    string query = @"SELECT * FROM GeoPointHarversines
                WHERE Message = 'GeoPoint' ";
                    query += "AND Imei = @imei ";
                    query += "AND UtcTime BETWEEN @fromTime AND @toTime";

                    cmd.CommandText = query;

                    DateTime utcFromTime = fromTime.ToUniversalTime();
                    DateTime utcToTime = toTime.ToUniversalTime();

                    cmd.Parameters.AddWithValue("imei", imei);
                    cmd.Parameters.AddWithValue("fromTime", utcFromTime);
                    cmd.Parameters.AddWithValue("toTime", utcToTime);

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        IList<Geolocation> geolocationList = new List<Geolocation>();
                        while (rdr.Read())
                        {
                            var geoPointDto = rdr.ToGeoPointDto();
                            results.Add(geoPointDto);
                        }
                    }
                }
            }
            return results;
        }
    }
}
