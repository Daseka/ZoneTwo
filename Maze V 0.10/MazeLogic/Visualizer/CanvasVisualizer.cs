using MazeV.MazeLogic.Drawing;
using MazeV.MazeLogic.MazeNodes;
using MazeV.MazeLogic.MazeViews;
using MazeV.MazeLogic.Settings;
using MazeV.MazeLogic.Units;
using System;
using System.Drawing;

namespace MazeV.MazeLogic.Visualizer
{
    public class CanvasVisualizer : IVisualizer
    {
        private readonly DefaultSettings _defaultSettings;

        public CanvasVisualizer(DefaultSettings defaultSettings)
        {
            _defaultSettings = defaultSettings;
        }

        public void Draw(IMazeGraphic mazeGraphic, IMazeViewData mazeView, IUnitList unitList)
        {
            ValidateGrapic(mazeGraphic);

            mazeGraphic.Clear(Color.White);
            int index = 0;

            for (int y = mazeView.ViewStart; y <= mazeView.ViewEnd; y++)
            {
                for (int x = mazeView.ViewStart; x <= mazeView.ViewEnd; x++)
                {
                    INode node = mazeView.MazeNodes[index];
                    DrawNode(x, y, node, mazeGraphic, mazeView, unitList);
                    index++;
                }
            }
        }

        private static void ValidateGrapic(IMazeGraphic mazeGraphic)
        {
            if (mazeGraphic is null)
            {
                throw new ArgumentException("No Graphic found cant draw");
            }
        }

        private void DrawCollectable(INode node, IMazeGraphic graphic, int indeX, int indeY)
        {
            int x = indeX - _defaultSettings.HalfOfCollectableSize;
            int y = indeY - _defaultSettings.HalfOfCollectableSize;
            int width = _defaultSettings.CollectableSize;
            int height = _defaultSettings.CollectableSize;
            var rectangle = new Rectangle(x, y, width, height);

            node.CollectablePoint.Draw(graphic, rectangle);
        }

        private void DrawNode(INode node, IMazeGraphic graphic, IMazeViewData mazeView, int indeX, int indeY)
        {
            int HalfOfNodeSize = _defaultSettings.HalfOfNodeSize;
            var topLeft = new Point(indeX - HalfOfNodeSize, indeY - HalfOfNodeSize);
            var topRight = new Point(indeX + HalfOfNodeSize, indeY - HalfOfNodeSize);
            var bottomLeft = new Point(indeX - HalfOfNodeSize, indeY + HalfOfNodeSize);
            var bottomRight = new Point(indeX + HalfOfNodeSize, indeY + HalfOfNodeSize);

            node.Draw(node, graphic, mazeView, topLeft, topRight, bottomLeft, bottomRight);
        }

        private void DrawPlayer(INode node, IMazeGraphic graphic, IUnitList unitList, int indeX, int indeY)
        {
            IUnit player = unitList.GetPlayer();
            var playerRect = new Rectangle(
                                    indeX - _defaultSettings.HalfOfUnitSize,
                                    indeY - _defaultSettings.HalfOfUnitSize,
                                    _defaultSettings.UnitSize,
                                    _defaultSettings.UnitSize);

            player.Draw(graphic, playerRect, node);
        }

        private void DrawNode(int x, int y, INode node, IMazeGraphic graphic, IMazeViewData mazeView, IUnitList unitList)
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