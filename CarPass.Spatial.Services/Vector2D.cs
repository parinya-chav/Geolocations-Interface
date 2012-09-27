// -----------------------------------------------------------------------
// <copyright file="Vector2D.cs" company="">
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
    public class Vector2D
    {
        public double X, Y;

        public Vector2D() { X = 0; Y = 0; }
        public Vector2D(double x, double y) { X = x; Y = y; }

        //length property        
        public double Length
        {
            get
            {
                return (double)Math.Sqrt((double)(X * X + Y * Y));
            }
        }


        //addition
        public static Vector2D operator +(Vector2D L, Vector2D R)
        {
            return new Vector2D(L.X + R.X, L.Y + R.Y);
        }

        //subtraction
        public static Vector2D operator -(Vector2D L, Vector2D R)
        {
            return new Vector2D(L.X - R.X, L.Y - R.Y);
        }

        //negative
        public static Vector2D operator -(Vector2D R)
        {
            Vector2D temp = new Vector2D(-R.X, -R.Y);
            return temp;
        }

        //scalar multiply
        public static Vector2D operator *(Vector2D L, double R)
        {
            return new Vector2D(L.X * R, L.Y * R);
        }

        //divide multiply
        public static Vector2D operator /(Vector2D L, double R)
        {
            return new Vector2D(L.X / R, L.Y / R);
        }

        //dot product
        public static double operator *(Vector2D L, Vector2D R)
        {
            return (L.X * R.X + L.Y * R.Y);
        }

        //cross product, in 2d this is a scalar since we know it points in the Z direction
        public static double operator %(Vector2D L, Vector2D R)
        {
            return (L.X * R.Y - L.Y * R.X);
        }

        //normalize the vector
        public void normalize()
        {
            double mag = Length;

            X /= mag;
            Y /= mag;
        }

        //project this vector on to v
        public Vector2D Project(Vector2D v)
        {
            double thisDotV = this * v;
            return v * thisDotV;
        }

        //project this vector on to v, return signed magnatude
        public Vector2D Project(Vector2D v, out double mag)
        {
            double thisDotV = this * v;
            mag = thisDotV;
            return v * thisDotV;
        }

        public override string ToString()
        {
            return string.Format("({0}, {1})", X, Y);
        }
    }
}
