using MazeV.MazeLogic.CollectableItems;
using MazeV.MazeLogic.Settings;
using MazeV.MazeLogic.Validators;
using MazeV.MazeLogic.Visualizer;

namespace MazeV.MazeLogic.MazeNodes
{
    public class NodeBuilder
    {
        private readonly CoinBuilder _coinBuilder;
        private readonly NodeVisualizer _nodeVisualizer;
        private readonly DefaultSettings _settings;

        public NodeBuilder(DefaultSettings settings, CoinBuilder coinBuilder, NodeVisualizer nodeVisualizer)
        {
            _settings = settings;
            _coinBuilder = coinBuilder;
            _nodeVisualizer = nodeVisualizer;
        }

        public Node Build()
        {
            return new Node(_coinBuilder, _settings, _nodeVisualizer);
        }
    }
}