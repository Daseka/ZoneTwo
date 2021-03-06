﻿namespace MazeV.MazeLogic
{
    public class MazeNodeDataBuilderSettings : IMazeNodeDataBuilderSettings
    {
        public MazeNodeDataBuilderSettings()
        {
            GridSize = 11;
            MinimumPathsToANode = 3;
        }

        public int GridSize { get; }
        public int MinimumPathsToANode { get; }
    }
}