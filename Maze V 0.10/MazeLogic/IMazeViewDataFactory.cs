namespace MazeV.MazeLogic
{
    public interface IMazeViewDataFactory
    {
        IMazeViewData CreateMazeViewData(int start, int end, int size, IMazeNodeData nodeData, IAxisFactory axisFactory);
    }
}