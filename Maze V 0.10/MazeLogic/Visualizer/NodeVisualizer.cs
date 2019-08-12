using MazeV.MazeLogic.MazeNodes;
using MazeV.MazeLogic.Movement.Directions;
using MazeV.MazeLogic.Validators;
using System.Drawing;

namespace MazeV.MazeLogic.Visualizer
{
    public class NodeVisualizer
    {
        private readonly DirectionRetriever _directionRetriever;
        private readonly Pen _pen;
        private readonly Validator _validator;

        public NodeVisualizer(Validator validator, DirectionRetriever directionRetriever)
        {
            _validator = validator;
            _pen = new Pen(Color.Blue);
            _directionRetriever = directionRetriever;
        }

        public void DrawNode(NodeVisualizerInfo nodeVisualizerInfo)
        {
            DrawLine(nodeVisualizerInfo, _directionRetriever.GetUp());
            DrawLine(nodeVisualizerInfo, _directionRetriever.GetDown());
            DrawLine(nodeVisualizerInfo, _directionRetriever.GetLeft());
            DrawLine(nodeVisualizerInfo, _directionRetriever.GetRight());
        }

        private void DrawLine(NodeVisualizerInfo nodeVisualizerInfo, IDirection direction)
        {
            INode node = nodeVisualizerInfo.Node.GetNeigbour(nodeVisualizerInfo.MazeView, direction);
            if (!_validator.DoesPathToNodeExist(nodeVisualizerInfo.Node, node))
            {
                var (pointA, pointB) = nodeVisualizerInfo.GetLine(direction);
                nodeVisualizerInfo.Graphic.DrawLine(_pen, pointA, pointB);
            }
        }
    }
}