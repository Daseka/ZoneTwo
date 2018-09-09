using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;

namespace MazeV.Maze_Logic
{
    public class MazeViewRotator
    {
        public void RotateView(Rotation rotation, IUnit player, IMazeViewData mazeView, Dictionary<ILocation, INode> nodesByLocation)
        {
            player.ViewingAxis = GetFreeRotationAxis(mazeView);
            DoMoveViewToOrigin(mazeView, nodesByLocation);

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
            DoMoveViewToPlayer(player, mazeView, nodesByLocation);
        }

        private void ApplyRotation(List<ILocation> vectorsToRotate, Matrix<double> rotationMatrix)
        {
            foreach (ILocation item in vectorsToRotate)
            {
                Vector<double> rotatedVector = rotationMatrix * Vector<double>.Build.Dense(new[]{item.PointX.Value
                                                                                                ,item.PointY.Value
                                                                                                ,item.PointZ.Value});
                item.PointX = Math.Round(rotatedVector[0], 2);
                item.PointY = Math.Round(rotatedVector[1], 2);
                item.PointZ = Math.Round(rotatedVector[2], 2);
            }
        }

        private double DegreesToRadial(double degrees)
        {
            return degrees * Math.PI / 180.0;
        }

        private void DoMoveView(Vector<double> vectorToMoveBy, IMazeViewData mazeView, Dictionary<ILocation, INode> nodesByLocation)
        {
            for (int i = 0; i < mazeView.MazeNodes.Count; i++)
            {
                INode item = mazeView.MazeNodes[i];

                Vector<double> movedVector = vectorToMoveBy + Vector<double>.Build.Dense(new[]{item.Location.PointX.Value
                                                                                                ,item.Location.PointY.Value
                                                                                                ,item.Location.PointZ.Value});
                double pointX = Math.Round(movedVector[0], 2);
                double pointY = Math.Round(movedVector[1], 2);
                double pointZ = Math.Round(movedVector[2], 2);

                Location temp = new Location(pointX, pointY, pointZ);

                nodesByLocation.TryGetValue(temp, out INode node);
                mazeView.MazeNodes[i] = node;
            }
        }

        private void DoMoveViewToOrigin(IMazeViewData mazeView, Dictionary<ILocation, INode> nodesByLocation)
        {
            int middle = (int)Math.Round((double)mazeView.MazeNodes.Count / 2);

            Vector<double> centerOfView = Vector<double>.Build.Dense(new double[] { mazeView.MazeNodes[middle].Location.PointX
                                                                                    , mazeView.MazeNodes[middle].Location.PointY
                                                                                    , mazeView.MazeNodes[middle].Location.PointZ });

            Vector<double> origin = Vector<double>.Build.Dense(new double[] { 0, 0, 0 });

            Vector<double> movement = origin - centerOfView;

            DoMoveView(movement, mazeView, nodesByLocation);
        }

        private void DoMoveViewToPlayer(IUnit player, IMazeViewData mazeView, Dictionary<ILocation, INode> nodesByLocation)
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

                case Axis.ZAxis:
                    movement = Vector<double>.Build.Dense(new double[] { 0, 0, player.CurrentLocation.PointZ });
                    break;
            }

            DoMoveView(movement, mazeView, nodesByLocation);
        }

        private void DoRotate(Axis axis, double degrees, IMazeViewData mazeView, Dictionary<ILocation, INode> nodesByLocation)
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

                case Axis.ZAxis:
                    rotationMatrix = Matrix<double>.Build.DenseOfColumnArrays(new[] { Math.Cos(angle), Math.Sin(angle), 0.0 }
                                                                            , new[] { -Math.Sin(angle), Math.Cos(angle), 0.0 }
                                                                            , new[] { 0.0, 0.0, 1.0 });
                    break;

                default:
                    return;
            }

            DoRotateView(rotationMatrix, mazeView, nodesByLocation);
            ApplyRotation(mazeView.MovementCube, rotationMatrix);
        }

        private void DoRotateView(Matrix<double> rotationMatrix, IMazeViewData mazeView, Dictionary<ILocation, INode> nodesByLocation)
        {
            for (int i = 0; i < mazeView.MazeNodes.Count; i++)
            {
                INode item = mazeView.MazeNodes[i];

                Vector<double> rotatedVector = rotationMatrix * Vector<double>.Build.Dense(new[]{item.Location.PointX.Value
                                                                                                ,item.Location.PointY.Value
                                                                                                ,item.Location.PointZ.Value});
                double pointX = Math.Round(rotatedVector[0], 2);
                double pointY = Math.Round(rotatedVector[1], 2);
                double pointZ = Math.Round(rotatedVector[2], 2);

                ILocation temp = new Location(pointX, pointY, pointZ);

                nodesByLocation.TryGetValue(temp, out INode node);
                mazeView.MazeNodes[i] = node;
            }
        }

        private Axis GetFreeRotationAxis(IMazeViewData mazeView)
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
    }
}