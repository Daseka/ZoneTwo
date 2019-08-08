using System;

namespace MazeV.MazeLogic
{
    public class Randomizer
    {
        public Random GenerateRandom(int seed)
        {
            return new Random(seed);
        }
    }
}