namespace MazeV.MazeLogic.MazeNodes.Settings
{
    public class MazeNodeDataBuilderSettings : IMazeNodeDataBuilderSettings
    {
        public int GridSize { get; }
        public int MinimumPathsToANode { get; }

        public MazeNodeDataBuilderSettings()
        {
            GridSize = 11;
            MinimumPathsToANode = 3;
        }
    }
}