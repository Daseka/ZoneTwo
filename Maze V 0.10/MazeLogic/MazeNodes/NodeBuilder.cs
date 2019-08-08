using MazeV.MazeLogic.CollectableItems;
using MazeV.MazeLogic.Settings;
using MazeV.MazeLogic.Validators;

namespace MazeV.MazeLogic.MazeNodes
{
    public class NodeBuilder
    {
        private readonly DefaultSettings _settings;
        private readonly CoinBuilder _coinBuilder;
        private readonly Validator _validators;

        public NodeBuilder(DefaultSettings settings, CoinBuilder coinBuilder, Validator validators)
        {
            _settings = settings;
            _coinBuilder = coinBuilder;
            _validators = validators;
        }

        public Node Build()
        {
            return new Node(_coinBuilder, _settings, _validators);
        }
    }
}