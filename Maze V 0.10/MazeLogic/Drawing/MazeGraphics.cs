using System.Drawing;

namespace MazeV.MazeLogic.Drawing
{
    public class MazeGraphics : IMazeGraphics
    {
        private readonly Graphics _graphic;

        public MazeGraphics(Graphics graphic)
        {
            _graphic = graphic;
        }

        public void Clear(Color color)
        {
            _graphic.Clear(color);
        }

        public void DrawLine(Pen pen, Point pt1, Point pt2)
        {
            _graphic.DrawLine(pen, pt1, pt2);
        }

        public void FillEllipse(Brush brush, Rectangle rectangle)
        {
            _graphic.FillEllipse(brush, rectangle);
        }

        public void FillRectangle(Brush brush, Rectangle rectangle)
        {
            _graphic.FillRectangle(brush, rectangle);
        }
    }
}