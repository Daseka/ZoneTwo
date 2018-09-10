﻿using System;
using System.Collections.Generic;

namespace MazeV.Maze_Logic
{
    public interface ILocation
    {
        RoundedDouble PointX { get; set; }
        RoundedDouble PointY { get; set; }
        RoundedDouble PointZ { get; set; }
        
        bool Equals(object obj);
        List<ILocation> GetAllPossibleNeighbours();
        ILocation GetCopy();
        int GetHashCode();
        string ToString();
        ILocation Add(ILocation location);
    }
}