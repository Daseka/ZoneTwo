using System;
using MathNet.Numerics.LinearAlgebra;

namespace MazeV.Maze_Logic
{
    public class XAxis : IAxis
    {
        public Matrix<double> GetRotationMatrix(double angle)
        {
            return Matrix<double>.Build.DenseOfColumnArrays(new[] { 1.0, 0.0, 0.0 }
                                                            , new[] { 0.0, Math.Cos(angle), Math.Sin(angle) }
                                                            , new[] { 0.0, -Math.Sin(angle), Math.Cos(angle) });
        }

        public Vector<double> GetVectorToPlayerAxis(IUnit player)
        {
            return Vector<double>.Build.Dense(new double[] { player.CurrentLocation.PointX, 0, 0 });
        }
    }
}