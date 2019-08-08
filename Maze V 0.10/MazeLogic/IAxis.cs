using MathNet.Numerics.LinearAlgebra;
using MazeV.MazeLogic.Units;

namespace MazeV.MazeLogic
{
    public interface IAxis
    {
        Matrix<double> GetRotationMatrix(double angle);

        Vector<double> GetVectorToPlayerAxis(IUnit player);
    }
}