using MazeV.MazeLogic.MazeNodes;
using MazeV.MazeLogic.Rotation;

namespace MazeV.MazeLogic.MazeViews
{
    public class MazeViewDataFactory : IMazeViewDataFactory
    {
        public IMazeViewData CreateMazeViewData(int start, int end, int size, IMazeNodeData nodeData, IAxisFactory axisFactory)
        {
            return new MazeViewData(start, end, size, nodeData, axisFactory);
        }
    }
}