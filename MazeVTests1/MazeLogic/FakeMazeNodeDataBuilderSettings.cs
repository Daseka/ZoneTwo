using MazeV.MazeLogic.MazeNodes.Settings;

namespace MazeVTests1.MazeLogic
{
    public class FakeMazeNodeDataBuilderSettings : IMazeNodeDataBuilderSettings
    {
        public int GridSize { get; }
        public int MinimumPathsToANode { get; }

        public FakeMazeNodeDataBuilderSettings(int size, int paths )
        {
            GridSize = size;
            MinimumPathsToANode = paths;
        }
    }
}