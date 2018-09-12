using System;
using System.Drawing;

namespace MazeV.Maze_Logic
{
    public class CanvasVisualizer : IVisualizer
    {
        private readonly Graphics fGraphic;

        public CanvasVisualizer(Graphics graphic)
        {
            fGraphic = graphic ?? throw new ArgumentException("No Graphic found cant draw");
        }

        public void Draw(IMazeViewData mazeView, UnitList unitList)
        {
            fGraphic.Clear(Color.White);
            int index = 0;

            for (int y = mazeView.ViewStart; y <= mazeView.ViewEnd; y++)
            {
                for (int x = mazeView.ViewStart; x <= mazeView.ViewEnd; x++)
                {
                    INode node = mazeView.MazeNodes[index];
                    DrawNode(x, y, node, fGraphic, mazeView, unitList);
                    index++;
                }
            }
        }

        private static void DrawCollectable(INode node, Graphics grapic, int indeX, int indeY)
        {
            Rectangle rectangle = new Rectangle(indeX - DefaultSettings.HalfOfCollectableSize, indeY - DefaultSettings.HalfOfCollectableSize, DefaultSettings.CollectableSize, DefaultSettings.CollectableSize);
            node.CollectablePoint.Draw(grapic, rectangle);
        }

        private static void DrawNode(INode node, Graphics grapic, IMazeViewData mazeView, int indeX, int indeY)
        {
            int HalfOfNodeSize = DefaultSettings.HalfOfNodeSize;
            Point topLeft = new Point(indeX - HalfOfNodeSize, indeY - HalfOfNodeSize);
            Point topRight = new Point(indeX + HalfOfNodeSize, indeY - HalfOfNodeSize);
            Point bottomLeft = new Point(indeX - HalfOfNodeSize, indeY + HalfOfNodeSize);
            Point bottomRight = new Point(indeX + HalfOfNodeSize, indeY + HalfOfNodeSize);
            node.Draw(node, grapic, mazeView, topLeft, topRight, bottomLeft, bottomRight);
        }

        private static void DrawPlayer(INode node, Graphics grapic, UnitList unitList, int indeX, int indeY)
        {
            IUnit player = unitList.GetPlayer();
            Rectangle playerRect = new Rectangle(indeX - DefaultSettings.HalfOfUnitSize, indeY - DefaultSettings.HalfOfUnitSize, DefaultSettings.UnitSize, DefaultSettings.UnitSize);
            player.Draw(grapic, playerRect, node);
        }

        private void DrawNode(int x, int y, INode node, Graphics grapic, IMazeViewData mazeView, UnitList unitList)
        {
            int indeX = GetNodePoint(x, node.SquareSize, mazeView.ViewStart);
            int indeY = GetNodePoint(y, node.SquareSize, mazeView.ViewStart);

            DrawNode(node, grapic, mazeView, indeX, indeY);
            DrawCollectable(node, grapic, indeX, indeY);
            DrawPlayer(node, grapic, unitList, indeX, indeY);
        }

        private int GetNodePoint(int xOrY, int nodeSquareSize, int mazeViewStart)
        {
            int graphicOffset = 1 - mazeViewStart;
            return (xOrY + graphicOffset) * nodeSquareSize;
        }
    }
}