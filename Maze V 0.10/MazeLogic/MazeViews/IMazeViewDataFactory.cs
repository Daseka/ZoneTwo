using MazeV.MazeLogic.MazeNodes;
using MazeV.MazeLogic.Rotation;

namespace MazeV.MazeLogic.MazeViews
{
    public interface IMazeViewDataFactory
    {
        IMazeViewData CreateMazeViewData(int start, int end, int size, IMazeNodeData nodeData, IAxisFactory axisFactory);
    }
}