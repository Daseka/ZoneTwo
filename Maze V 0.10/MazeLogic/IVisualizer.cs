using MazeV.MazeLogic.Units;

namespace MazeV.MazeLogic
{
    public interface IVisualizer
    {
        void Draw(IMazeViewData mazeView, IUnitList unitList);
    }
}