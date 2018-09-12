using System;
using MathNet.Numerics.LinearAlgebra;

namespace MazeV.Maze_Logic
{
    public class YAxis : IAxis
    {
        public Matrix<double> GetRotationMatrix(double angle)
        {
            return Matrix<double>.Build.DenseOfColumnArrays(new[] { Math.Cos(angle), 0.0, -Math.Sin(angle) }
                                                            , new[] { 0.0, 1.0, 0.0 }
                                                            , new[] { Math.Sin(angle), 0.0, Math.Cos(angle) });
        }

        public Vector<double> GetVectorToPlayerAxis(IUnit player)
        {
            return Vector<double>.Build.Dense(new double[] { 0, player.CurrentLocation.PointY, 0 });
        }
    }
}