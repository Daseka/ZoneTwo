using MazeV.MazeLogic.Drawing;
using MazeV.MazeLogic.MazeViews;
using MazeV.MazeLogic.Units;

namespace MazeV.MazeLogic.Visualizer
{
    public interface IVisualizer
    {
        void Draw(IMazeGraphic mazeGraphic, IMazeViewData mazeView, IUnitList unitList);
    }
}