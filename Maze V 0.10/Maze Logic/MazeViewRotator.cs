using System;
using System.Linq;
using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra;
using System.Text;
using System.Threading.Tasks;

namespace MazeV.Maze_Logic
{
    public class MazeViewRotator
    {
        private double DegreesToRadial(double degrees)
        {
            return degrees * Math.PI / 180.0;
        }

        private void ApplyRotation(List<Location> vectorsToRotate, Matrix<double> rotationMatrix)
        {
            foreach (Location item in vectorsToRotate)
            {
                Vector<double> rotatedVector = rotationMatrix * Vector<double>.Build.Dense(new[]{item.PointX
                                                                                                ,item.PointY
                                                                                                ,item.PointZ});
                item.PointX = Math.Round(rotatedVector[0], 2);
                item.PointY = Math.Round(rotatedVector[1], 2);
                item.PointZ = Math.Round(rotatedVector[2], 2);
            }
        }

        private void DoMoveViewToPlayer(Player player, MazeViewData mazeView, Dictionary<Location,Node> nodesByLocation)
        {
            Vector<double> movement = null;

            switch (player.ViewingAxis)
            {
                case Axis.XAxis:
                    movement = Vector<double>.Build.Dense(new double[] { player.CurrentLocation.PointX, 0, 0 });
                    break;
                case Axis.YAxis:
                    movement = Vector<double>.Build.Dense(new double[] { 0, player.CurrentLocation.PointY, 0 });
                    break;
                case Axis.Zaxis:
                    movement = Vector<double>.Build.Dense(new double[] { 0, 0, player.CurrentLocation.PointZ });
                    break;
                default:
                    break;
            }

            DoMoveView(movement, mazeView, nodesByLocation);
        }

        private void DoRotateView(Matrix<double> rotationMatrix, MazeViewData mazeView,Dictionary<Location,Node> nodesByLocation)
        {
            for (int i = 0; i < mazeView.Count; i++)
            {
                Node item = mazeView[i];

                Vector<double> rotatedVector = rotationMatrix * Vector<double>.Build.Dense(new[]{item.Location.PointX
                                                                                                ,item.Location.PointY
                                                                                                ,item.Location.PointZ});
                double pointX = Math.Round(rotatedVector[0], 2);
                double pointY = Math.Round(rotatedVector[1], 2);
                double pointZ = Math.Round(rotatedVector[2], 2);

                Location temp = new Location(pointX, pointY, pointZ);

                Node node = null;
                if (nodesByLocation.TryGetValue(temp, out node))
                    mazeView[i] = node;
                else
                    throw new Exception("Cant do Rotate");                
            }
        }

        private void DoMoveView(Vector<double> vectorToMoveBy, MazeViewData mazeView,Dictionary<Location,Node> nodesByLocation)
        {
            for (int i = 0; i < mazeView.Count; i++)
            {
                Node item = mazeView[i];

                Vector<double> movedVector = vectorToMoveBy + Vector<double>.Build.Dense(new[]{item.Location.PointX
                                                                                                ,item.Location.PointY
                                                                                                ,item.Location.PointZ});
                double pointX = Math.Round(movedVector[0], 2);
                double pointY = Math.Round(movedVector[1], 2);
                double pointZ = Math.Round(movedVector[2], 2);

                Location temp = new Location(pointX, pointY, pointZ);

                Node node = null;
                if (nodesByLocation.TryGetValue(temp, out node))
                    mazeView[i] = node;
                else
                    throw new Exception("Cant do move");                
            }
        }

        private void DoMoveViewToOrigin(MazeViewData mazeView,Dictionary<Location,Node> nodesByLocation)
        {
            int middle = (int)Math.Round((double)mazeView.Count / 2);

            Vector<double> centerOfView = Vector<double>.Build.Dense(new double[] { mazeView[middle].Location.PointX
                                                                                    , mazeView[middle].Location.PointY
                                                                                    , mazeView[middle].Location.PointZ });

            Vector<double> origin = Vector<double>.Build.Dense(new double[] { 0, 0, 0 });

            Vector<double> movement = origin - centerOfView;

            DoMoveView(movement, mazeView, nodesByLocation);
        }

        private Axis GetFreeRotationAxis(MazeViewData mazeView)
        {
            Axis rotationAxis = Axis.XAxis;
            foreach (Axis axis in Enum.GetValues(typeof(Axis)))
            {
                if (mazeView.LeftRightRotationAxis == axis || mazeView.UpDownRotationAxis == axis)
                    continue;

                return axis;
            }

            return rotationAxis;
        }

        private void DoRotate(Axis axis, double degrees, MazeViewData mazeView,Dictionary<Location,Node> nodesByLocation)
        {
            double angle = DegreesToRadial(degrees);

            Matrix<double> rotationMatrix = null;

            switch (axis)
            {
                case Axis.XAxis:
                    rotationMatrix = Matrix<double>.Build.DenseOfColumnArrays(new[] { 1.0, 0.0, 0.0 }
                                                                            , new[] { 0.0, Math.Cos(angle), Math.Sin(angle) }
                                                                            , new[] { 0.0, -Math.Sin(angle), Math.Cos(angle) });
                    break;
                case Axis.YAxis:
                    rotationMatrix = Matrix<double>.Build.DenseOfColumnArrays(new[] { Math.Cos(angle), 0.0, -Math.Sin(angle) }
                                                                            , new[] { 0.0, 1.0, 0.0 }
                                                                            , new[] { Math.Sin(angle), 0.0, Math.Cos(angle) });
                    break;
                case Axis.Zaxis:
                    rotationMatrix = Matrix<double>.Build.DenseOfColumnArrays(new[] { Math.Cos(angle), Math.Sin(angle), 0.0 }
                                                                            , new[] { -Math.Sin(angle), Math.Cos(angle), 0.0 }
                                                                            , new[] { 0.0, 0.0, 1.0 });
                    break;
                default:
                    return;
            }

            DoRotateView(rotationMatrix, mazeView,nodesByLocation);
            ApplyRotation(mazeView.MovementCube, rotationMatrix);
        }

        public void RotateView(Rotation rotation, Player player, MazeViewData mazeView,Dictionary<Location,Node> nodesByLocation)
        {
            player.ViewingAxis = GetFreeRotationAxis(mazeView);
            DoMoveViewToOrigin(mazeView,nodesByLocation);

            switch (rotation)
            {
                case Rotation.Down:
                    DoRotate(mazeView.UpDownRotationAxis, -90, mazeView, nodesByLocation);
                    mazeView.LeftRightRotationAxis = GetFreeRotationAxis(mazeView);
                    break;
                case Rotation.Left:
                    DoRotate(mazeView.LeftRightRotationAxis, -90, mazeView, nodesByLocation);
                    mazeView.UpDownRotationAxis = GetFreeRotationAxis(mazeView);
                    break;
                case Rotation.Right:
                    DoRotate(mazeView.LeftRightRotationAxis, 90, mazeView, nodesByLocation);
                    mazeView.UpDownRotationAxis = GetFreeRotationAxis(mazeView);
                    break;
                case Rotation.Up:
                    DoRotate(mazeView.UpDownRotationAxis, 90, mazeView, nodesByLocation);
                    mazeView.LeftRightRotationAxis = GetFreeRotationAxis(mazeView);
                    break;
                default:
                    break;
            }

            player.ViewingAxis = GetFreeRotationAxis(mazeView);
            DoMoveViewToPlayer(player, mazeView,nodesByLocation);
        }
    }
}