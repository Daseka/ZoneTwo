using MathNet.Numerics.LinearAlgebra;
using MazeV.MazeLogic.Units;

namespace MazeV.MazeLogic.Rotation
{
    public interface IAxis
    {
        Matrix<double> GetRotationMatrix(double angle);

        Vector<double> GetVectorToPlayerAxis(IUnit player);
    }
}