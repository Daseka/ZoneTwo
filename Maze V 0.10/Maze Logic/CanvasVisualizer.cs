using System;
using System.Drawing;

namespace MazeV.Maze_Logic
{
    public class CanvasVisualizer : IVisualizer
    {
        private readonly int fCollectableSize = 6;
        private readonly Graphics fGraphic;
        private readonly int fHalfOfCollectableSize = 6 / 2;
        private readonly int fHalfOfNodeSize = 50 / 2;
        private readonly int fHalfOfUnitSize = 10 / 2;
        private readonly int fNodeSize = 50;
        private readonly int fUnitSize = 10;

        public CanvasVisualizer(Graphics graphic)
        {
            fGraphic = graphic;
        }

        public void Draw(MazeViewData mazeView, UnitList unitList)
        {
            if (fGraphic == null)
                throw new ArgumentException("No Graphic found cant draw");

            fGraphic.Clear(Color.White);

            int index = 0;

            for (int y = mazeView.ViewStart; y <= mazeView.ViewEnd; y++)
            {
                for (int x = mazeView.ViewStart; x <= mazeView.ViewEnd; x++)
                {
                    Node node = mazeView[index];
                    DrawNode(x, y, node, fGraphic, mazeView, unitList);
                    index++;
                }
            }
        }

        private void DrawNode(int x, int y, Node node, Graphics grapic, MazeViewData mazeView, UnitList unitList)
        {
            Pen pen = new Pen(Color.Blue);

            int graphicOffset = 1 - mazeView.ViewStart;

            int indeX = (x + graphicOffset) * fNodeSize;
            int indeY = (y + graphicOffset) * fNodeSize;

            if (node.CollectablePoint > 0)
            {
                Rectangle rect = new Rectangle(indeX - fHalfOfCollectableSize, indeY - fHalfOfCollectableSize, fCollectableSize, fCollectableSize);
                grapic.FillEllipse(Brushes.Gray, rect);
            }

            Player player = unitList.GetPlayer();

            if (node.Location == player.CurrentLocation)
            {
                Rectangle rect = new Rectangle(indeX - fHalfOfUnitSize, indeY - fHalfOfUnitSize, fUnitSize, fUnitSize);
                grapic.FillRectangle(Brushes.Red, rect);
            }

            Point topLeft = new Point(indeX - fHalfOfNodeSize, indeY - fHalfOfNodeSize);
            Point topRight = new Point(indeX + fHalfOfNodeSize, indeY - fHalfOfNodeSize);
            Point bottomLeft = new Point(indeX - fHalfOfNodeSize, indeY + fHalfOfNodeSize);
            Point bottomRight = new Point(indeX + fHalfOfNodeSize, indeY + fHalfOfNodeSize);

            Node leftNode = node.GetNeigbour(mazeView, Direction.Left);
            Node bottomNode = node.GetNeigbour(mazeView, Direction.Down);
            Node rightNode = node.GetNeigbour(mazeView, Direction.Right);
            Node topNode = node.GetNeigbour(mazeView, Direction.Up);

            if (!Validator.DoesPathToNodeExist(node, topNode))
                grapic.DrawLine(pen, topLeft, topRight);

            if (!Validator.DoesPathToNodeExist(node, rightNode))
                grapic.DrawLine(pen, topRight, bottomRight);

            if (!Validator.DoesPathToNodeExist(node, bottomNode))
                grapic.DrawLine(pen, bottomRight, bottomLeft);

            if (!Validator.DoesPathToNodeExist(node, leftNode))
                grapic.DrawLine(pen, bottomLeft, topLeft);
        }
    }
}