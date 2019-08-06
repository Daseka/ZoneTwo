using MazeV.MazeLogic.Drawing;
using MazeV.MazeLogic.Units;
using System;
using System.Drawing;

namespace MazeV.MazeLogic
{
    public class CanvasVisualizer : IVisualizer
    {
        private readonly IMazeGraphics _mazeGraphic;

        public CanvasVisualizer(Graphics graphic)
        {
            _mazeGraphic = graphic is null
                ? throw new ArgumentException("No Graphic found cant draw")
                : new MazeGraphics(graphic);
        }

        public void Draw(IMazeViewData mazeView, IUnitList unitList)
        {
            _mazeGraphic.Clear(Color.White);
            int index = 0;

            for (int y = mazeView.ViewStart; y <= mazeView.ViewEnd; y++)
            {
                for (int x = mazeView.ViewStart; x <= mazeView.ViewEnd; x++)
                {
                    INode node = mazeView.MazeNodes[index];
                    DrawNode(x, y, node, _mazeGraphic, mazeView, unitList);
                    index++;
                }
            }
        }

        private static void DrawCollectable(INode node, IMazeGraphics graphic, int indeX, int indeY)
        {
            int x = indeX - DefaultSettings.HalfOfCollectableSize;
            int y = indeY - DefaultSettings.HalfOfCollectableSize;
            int width = DefaultSettings.CollectableSize;
            int height = DefaultSettings.CollectableSize;
            var rectangle = new Rectangle(x, y, width, height);

            node.CollectablePoint.Draw(graphic, rectangle);
        }

        private static void DrawNode(INode node, IMazeGraphics graphic, IMazeViewData mazeView, int indeX, int indeY)
        {
            int HalfOfNodeSize = DefaultSettings.HalfOfNodeSize;
            var topLeft = new Point(indeX - HalfOfNodeSize, indeY - HalfOfNodeSize);
            var topRight = new Point(indeX + HalfOfNodeSize, indeY - HalfOfNodeSize);
            var bottomLeft = new Point(indeX - HalfOfNodeSize, indeY + HalfOfNodeSize);
            var bottomRight = new Point(indeX + HalfOfNodeSize, indeY + HalfOfNodeSize);

            node.Draw(node, graphic, mazeView, topLeft, topRight, bottomLeft, bottomRight);
        }

        private static void DrawPlayer(INode node, IMazeGraphics graphic, IUnitList unitList, int indeX, int indeY)
        {
            IUnit player = unitList.GetPlayer();
            var playerRect = new Rectangle(
                                    indeX - DefaultSettings.HalfOfUnitSize,
                                    indeY - DefaultSettings.HalfOfUnitSize,
                                    DefaultSettings.UnitSize,
                                    DefaultSettings.UnitSize);

            player.Draw(graphic, playerRect, node);
        }

        private void DrawNode(int x, int y, INode node, IMazeGraphics graphic, IMazeViewData mazeView, IUnitList unitList)
        {
            int indeX = GetNodePoint(x, node.SquareSize, mazeView.ViewStart);
            int indeY = GetNodePoint(y, node.SquareSize, mazeView.ViewStart);

            DrawNode(node, graphic, mazeView, indeX, indeY);
            DrawCollectable(node, graphic, indeX, indeY);
            DrawPlayer(node, graphic, unitList, indeX, indeY);
        }

        private int GetNodePoint(int xOrY, int nodeSquareSize, int mazeViewStart)
        {
            int graphicOffset = 1 - mazeViewStart;
            return (xOrY + graphicOffset) * nodeSquareSize;
        }
    }
}