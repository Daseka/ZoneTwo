using System.Drawing;

namespace MazeV.MazeLogic.Drawing
{
    public interface IMazeGraphic
    {
        void Clear(Color color);

        void DrawLine(Pen pen, Point pt1, Point pt2);

        void FillEllipse(Brush brush, Rectangle rectangle);

        void FillRectangle(Brush brush, Rectangle rectangle);
    }
}