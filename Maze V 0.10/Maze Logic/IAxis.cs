using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;

namespace MazeV.Maze_Logic
{
    public interface IAxis
    {
        Vector<double> GetVectorToPlayerAxis(IUnit player);
        Matrix<double> GetRotationMatrix(double angle);
    }
}
