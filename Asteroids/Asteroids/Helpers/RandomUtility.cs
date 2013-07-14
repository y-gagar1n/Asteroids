using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroids.Helpers
{
    static class RandomUtility
    {
        static Random rnd;

        static RandomUtility()
        {
            rnd = new Random();
        }

        public static bool IsOneOf(int value)
        {
            return rnd.Next(value) == 0;
        }
    }
}
