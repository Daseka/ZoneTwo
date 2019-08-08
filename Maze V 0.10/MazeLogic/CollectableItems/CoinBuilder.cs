using MazeV.MazeLogic.Settings;

namespace MazeV.MazeLogic.CollectableItems
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