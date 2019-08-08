namespace MazeV.MazeLogic.MazeNodes.Settings
{
    public interface IMazeNodeDataBuilderSettings
    {
        int GridSize { get; }
        int MinimumPathsToANode { get; }
    }
}