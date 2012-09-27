// -----------------------------------------------------------------------
// <copyright file="IGeolocationsWriter.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace CarPass.Spatial.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
using CarPass.Spatial.Interface.Dto;

    /// <summary>
    /// For Writer only
    /// </summary>
    public interface IGeolocationsWriter
    {
        int SaveGeoPointDto(GeoPointDto geopoint);
    }
}
