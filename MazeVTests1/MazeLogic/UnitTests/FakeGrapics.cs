using MazeV.MazeLogic.Drawing;
using System.Drawing;

namespace MazeVTests1.MazeLogic
{
    public class FakeGrapics : IMazeGraphics
    {
        public string MethodCalled { get; private set; }

        public void Clear(Color color)
        {
            MethodCalled = nameof(FakeGrapics.Clear);
        }

        public void DrawLine(Pen pen, Point pt1, Point pt2)
        {
            MethodCalled = nameof(FakeGrapics.DrawLine);
        }

        public void FillEllipse(Brush brush, Rectangle rectangle)
        {
            MethodCalled = nameof(FakeGrapics.FillEllipse);
        }

        public void FillRectangle(Brush brush, Rectangle rectangle)
        {
            MethodCalled = nameof(FakeGrapics.FillRectangle);
        }
    }
}