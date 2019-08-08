namespace MazeV.MazeLogic
{
    public interface IMazeNodeDataBuilderSettings
    {
        int GridSize { get; }
        int MinimumPathsToANode { get; }
    }
}