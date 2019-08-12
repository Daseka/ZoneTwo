using MazeV.MazeLogic.Drawing;
using MazeV.MazeLogic.MazeNodes;
using MazeV.MazeLogic.MazeViews;
using MazeV.MazeLogic.Movement.Directions;
using MazeV.MazeLogic.Settings;
using MazeV.MazeLogic.Units;
using System;
using System.Drawing;

namespace MazeV.MazeLogic.Visualizer
{
    public class CanvasVisualizer : IVisualizer
    {
        private readonly DefaultSettings _defaultSettings;

        private readonly DirectionRetriever _directionRetriever;

        public CanvasVisualizer(DefaultSettings defaultSettings, DirectionRetriever directionRetriever)
        {
            _defaultSettings = defaultSettings;
            _directionRetriever = directionRetriever;
        }

        public void Draw(IMazeGraphic mazeGraphic, IMazeViewData mazeView, IUnitList unitList)
        {
            ValidateGrapic(mazeGraphic);

            mazeGraphic.Clear(Color.White);
            int index = 0;

            for (int yIndex = mazeView.ViewStart; yIndex <= mazeView.ViewEnd; yIndex++)
            {
                for (int xIndex = mazeView.ViewStart; xIndex <= mazeView.ViewEnd; xIndex++)
                {
                    INode node = mazeView.MazeNodes[index];

                    var info = new Info
                    {
                        Graphic = mazeGraphic,
                        MazeView = mazeView,
                        Node = node,
                        UnitList = unitList,
                        XIndex = xIndex,
                        YIndex = yIndex,
                    };
                    DrawNode(info);
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

        private void DrawCollectable(Info info)
        {
            int x = info.XIndex - _defaultSettings.HalfOfCollectableSize;
            int y = info.YIndex - _defaultSettings.HalfOfCollectableSize;
            int width = _defaultSettings.CollectableSize;
            int height = _defaultSettings.CollectableSize;
            var rectangle = new Rectangle(x, y, width, height);

            info.Node.CollectablePoint.Draw(info.Graphic, rectangle);
        }

        private void DrawNode(Info info)
        {
            info.XIndex = OffsetIndex(info.XIndex, info.Node.SquareSize, info.MazeView.ViewStart);
            info.YIndex = OffsetIndex(info.YIndex, info.Node.SquareSize, info.MazeView.ViewStart);

            DrawNodeEdges(info);
            DrawCollectable(info);
            DrawPlayer(info);
        }

        private void DrawNodeEdges(Info info)
        {
            int HalfOfNodeSize = _defaultSettings.HalfOfNodeSize;
            var nodeVisualizerInfo = new NodeVisualizerInfo(_directionRetriever)
            {
                TopLeft = new Point(info.XIndex - HalfOfNodeSize, info.YIndex - HalfOfNodeSize),
                TopRight = new Point(info.XIndex + HalfOfNodeSize, info.YIndex - HalfOfNodeSize),
                BottomLeft = new Point(info.XIndex - HalfOfNodeSize, info.YIndex + HalfOfNodeSize),
                BottomRight = new Point(info.XIndex + HalfOfNodeSize, info.YIndex + HalfOfNodeSize),
                Node = info.Node,
                MazeView = info.MazeView,
                Graphic = info.Graphic,
            };

            info.Node.Draw(nodeVisualizerInfo);
        }

        private void DrawPlayer(Info info)
        {
            IUnit player = info.UnitList.GetPlayer();
            var playerRect = new Rectangle(
                info.XIndex - _defaultSettings.HalfOfUnitSize,
                info.YIndex - _defaultSettings.HalfOfUnitSize,
                _defaultSettings.UnitSize,
                _defaultSettings.UnitSize);

            player.Draw(info.Graphic, playerRect, info.Node);
        }

        private int OffsetIndex(int xOrY, int nodeSquareSize, int mazeViewStart)
        {
            int graphicOffset = 1 - mazeViewStart;
            return (xOrY + graphicOffset) * nodeSquareSize;
        }

        private class Info
        {
            public IMazeGraphic Graphic { get; set; }
            public IMazeViewData MazeView { get; set; }
            public INode Node { get; set; }
            public IUnitList UnitList { get; set; }
            public int XIndex { get; set; }
            public int YIndex { get; set; }
        }
    }
}