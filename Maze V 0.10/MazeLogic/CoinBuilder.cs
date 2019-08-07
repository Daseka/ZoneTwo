namespace MazeV.MazeLogic
{
    public class CoinBuilder
    {
        private readonly DefaultSettings _settings;

        public CoinBuilder(DefaultSettings defaultSettings)
        {
            _settings = defaultSettings;
        }

        public Coin Build()
        {
            return new Coin(_settings);
        }
    }
}