namespace MazeV.MazeLogic.Settings
{
    public class DefaultSettings
    {
        public int CollectableSize => 6;

        public int HalfOfCollectableSize => CollectableSize / 2;

        public int HalfOfNodeSize => NodeSize / 2;

        public int HalfOfUnitSize => UnitSize / 2;

        public int NodeSize => 50;

        public int UnitSize => 10;
    }
}