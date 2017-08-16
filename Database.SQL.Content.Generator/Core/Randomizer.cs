using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.SQL.Content.Generator.Core
{
    public class Randomizer
    {
        private static Randomizer _instance;

        public static Randomizer Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Randomizer();
                }

                return _instance;
            }
        }

        private readonly Random rand;
        private Randomizer()
        {
            rand = new Random((int)DateTime.UtcNow.Ticks);
        }

        public bool GetBool()
        {
            return rand.NextDouble() >= 0.5;
        }

        public int GetInt()
        {
            return GetInt(0, int.MaxValue - 1);
        }

        public int GetInt(int max)
        {
            return GetInt(0, max);
        }

        public int GetInt(int min, int max)
        {
            return rand.Next(min, max);
        }

        public int GetIntPercentage()
        {
            return GetInt(0, 100);
        }
    }
}
