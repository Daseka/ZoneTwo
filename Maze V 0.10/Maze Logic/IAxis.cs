using MathNet.Numerics.LinearAlgebra;

namespace MazeV.Maze_Logic
{
    public interface IAxis
    {
        Matrix<double> GetRotationMatrix(double angle);

        Vector<double> GetVectorToPlayerAxis(IUnit player);
    }
}