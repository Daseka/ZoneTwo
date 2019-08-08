using MazeV.MazeLogic.MazeNodes;
using MazeV.MazeLogic.Rotation;
using System.Collections.Generic;

namespace MazeV.MazeLogic.MazeViews
{
    public interface IMazeViewData
    {
        IAxis LeftRightRotationAxis { get; set; }

        IList<INode> MazeNodes { get; set; }

        List<ILocation> MovementCube { get; set; }

        IAxis UpDownRotationAxis { get; set; }

        int ViewEnd { get; set; }

        int ViewSize { get; set; }

        int ViewStart { get; set; }

        INode GetNodeAt(ILocation location);
    }
}