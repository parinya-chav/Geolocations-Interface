// -----------------------------------------------------------------------
// <copyright file="HarversineHelper.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace CarPass.Spatial.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class HarversineHelper
    {
        public static double R_TH_BKK = 6376989.215;//Meters

        public static double Distance(IList<Vector2D> vector2DList)
        {
            double totalDistance = 0.0;
            for (int i = vector2DList.Count - 1; i > 0; i--)
            {
                Vector2D coordinate2 = vector2DList[i];
                Vector2D coordinate1 = vector2DList[i - 1];
                var d = Distance(coordinate1, coordinate2);
                totalDistance += d;
            }
            return totalDistance;
        }

        public static double Distance(Vector2D coordinate1, Vector2D coordinate2)
        {
            double distance = 0.0;
            var distanceCord = coordinate2 - coordinate1;
            var a = Math.Pow(Math.Sin(ToRadian(distanceCord.X) / 2), 2)
                + Math.Cos(ToRadian(coordinate1.X)) * Math.Cos(ToRadian(coordinate2.X)) * Math.Pow(Math.Sin(ToRadian(distanceCord.Y) / 2), 2);
            var c = 2 * Math.Atan2(Math.Pow(a, 0.5), Math.Pow(1 - a, 0.5));
            var d = R_TH_BKK * c;
            distance = d;
            return distance;
        }

        public static double ToRadian(double val)
        {
            return (Math.PI / 180) * val;
        }
    }
}
