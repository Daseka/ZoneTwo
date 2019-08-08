namespace MazeV.MazeLogic
{
    public class NodeBuilder
    {
        private readonly DefaultSettings _settings;
        private readonly CoinBuilder _coinBuilder;

        public NodeBuilder(DefaultSettings settings, CoinBuilder coinBuilder)
        {
            _settings = settings;
            _coinBuilder = coinBuilder;
        }

        public Node Build()
        {
            return new Node(_coinBuilder, _settings);
        }
    }
}