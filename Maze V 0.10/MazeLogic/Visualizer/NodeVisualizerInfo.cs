using MazeV.MazeLogic.Drawing;
using MazeV.MazeLogic.MazeNodes;
using MazeV.MazeLogic.MazeViews;
using MazeV.MazeLogic.Movement.Directions;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace MazeV.MazeLogic.Visualizer
{
    public class NodeVisualizerInfo
    {
        private readonly Dictionary<IDirection, Func<(Point, Point)>> _edges;
        public Point BottomLeft { get; set; }
        public Point BottomRight { get; set; }
        public IMazeGraphic Graphic { get; set; }
        public IMazeViewData MazeView { get; set; }
        public INode Node { get; set; }
        public Point TopLeft { get; set; }
        public Point TopRight { get; set; }

        public NodeVisualizerInfo(DirectionRetriever directionRetriever)
        {
            _edges = new Dictionary<IDirection, Func<(Point, Point)>>
            {
                [directionRetriever.GetLeft()] = GetLeftLine,
                [directionRetriever.GetRight()] = GetRightLine,
                [directionRetriever.GetUp()] = GetTopLine,
                [directionRetriever.GetDown()] = GetBottomLine
            };
        }

        public (Point pointA, Point pointB) GetLine(IDirection direction)
        {
            return _edges[direction].Invoke();
        }

        private (Point bottomRight, Point bottomLeft) GetBottomLine()
        {
            return (BottomRight, BottomLeft);
        }

        private (Point bottomLeft, Point topLeft) GetLeftLine()
        {
            return (BottomLeft, TopLeft);
        }

        private (Point topRight, Point bottomRight) GetRightLine()
        {
            return (TopRight, BottomRight);
        }

        private (Point topLeft, Point topRight) GetTopLine()
        {
            return (TopLeft, TopRight);
        }
    }
}