namespace MazeV.Maze_Logic
{
    public class DefaultSettings
    {
        public static int NodeSize => 50;
        public static int CollectableSize => 6;
        public static int UnitSize => 10;

        public static int HalfOfCollectableSize => CollectableSize / 2;
        public static int HalfOfUnitSize => UnitSize / 2;               
        public static int HalfOfNodeSize => NodeSize / 2;
    }
}
