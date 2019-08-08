using MazeV.MazeLogic.Drawing;
using MazeV.MazeLogic.Units;

namespace MazeV.MazeLogic
{
    public interface IVisualizer
    {
        void Draw(IMazeGraphic mazeGraphic,IMazeViewData mazeView, IUnitList unitList);
    }
}