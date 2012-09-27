using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace CarPass.Spatial.Interface
{
    using CarPass.Spatial.Interface.Dto;

    /// <summary>
    /// Spatial Data for Get Geolocations Service Contract
    /// </summary>
    [ServiceContract]
    public interface IGeolocations
    {
        /// <summary>
        /// Get Geolocations by Device SN
        /// </summary>
        /// <param name="deviceSN">Device Serial Number(SN)</param>
        /// <param name="fromTime">From Date Local Time</param>
        /// <param name="toTime">To Date Local Time</param>
        /// <returns>List of Geolocations</returns>
        [OperationContract]
        IList<GeoPointDto> GetLocationsByDeviceSN(string deviceSN, DateTime fromTime, DateTime toTime);
        
        /// <summary>
        /// Get Geolocations by IMEI
        /// </summary>
        /// <param name="imei">IMEI</param>
        /// <param name="fromTime">From Date Local Time</param>
        /// <param name="toTime">To Date Local Time</param>
        /// <returns>List of Geolocations</returns>
        [OperationContract]
        IList<GeoPointDto> GetLocationsByImei(string imei, DateTime fromTime, DateTime toTime);

    }
}
