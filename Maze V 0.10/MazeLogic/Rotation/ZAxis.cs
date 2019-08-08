using System;
using MathNet.Numerics.LinearAlgebra;
using MazeV.MazeLogic.Units;

namespace MazeV.MazeLogic.Rotation
{
    public class ZAxis : IAxis
    {
        public Matrix<double> GetRotationMatrix(double angle)
        {
            return Matrix<double>.Build.DenseOfColumnArrays(new[] { Math.Cos(angle), Math.Sin(angle), 0.0 }
                                                            , new[] { -Math.Sin(angle), Math.Cos(angle), 0.0 }
                                                            , new[] { 0.0, 0.0, 1.0 });
        }

        public Vector<double> GetVectorToPlayerAxis(IUnit player)
        {
            return Vector<double>.Build.Dense(new double[] { 0, 0, player.CurrentLocation.PointZ });
        }
    }
}