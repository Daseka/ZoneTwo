using System.Collections.Generic;

namespace MazeV.Maze_Logic
{
    public interface IMazeViewData
    {
        Axis LeftRightRotationAxis { get; set; }
        List<ILocation> MovementCube { get; set; }
        Axis UpDownRotationAxis { get; set; }
        int ViewEnd { get; set; }
        int ViewSize { get; set; }
        int ViewStart { get; set; }
        IList<INode> MazeNodes {get; set;}

        INode GetNodeAt(ILocation location);
    }
}