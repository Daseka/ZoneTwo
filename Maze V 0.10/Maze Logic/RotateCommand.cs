using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MazeV.Maze_Logic
{

    public class RotateCommand : ICommand
    {
        private Rotation fRotation;
        private Player fPlayer;
        private MazeViewData fMazeView;
        private Dictionary<Location, Node> fNodesByLocation;

        public RotateCommand(Rotation rotation,Player player, MazeViewData mazeView, Dictionary<Location,Node> nodesByLocation)
        {
            fRotation = rotation;
            fPlayer = player;
            fMazeView = mazeView;
            fNodesByLocation = nodesByLocation;            
        }   
        public void Execute()
        {
            MazeViewRotator rotator = new MazeViewRotator();
            rotator.RotateView(fRotation, fPlayer, fMazeView, fNodesByLocation);
        }
    }

}