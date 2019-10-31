using System;

namespace Movies.Core.Util
{
    public static class Rounder
    {
        public static double Round(double value)
        {
            // Taken from https://stackoverflow.com/questions/1329426/how-do-i-round-to-the-nearest-0-5
            return Math.Round(value * 2, MidpointRounding.AwayFromZero) / 2;
        }
    }
}