using System;
using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra;
using MazeV.MazeLogic.MazeNodes;
using MazeV.MazeLogic.MazeViews;
using MazeV.MazeLogic.Units;

namespace MazeV.MazeLogic.Rotation
{
    public class MazeViewRotator
    {
        private readonly IAxisFactory fAxisFactory;

        public MazeViewRotator(IAxisFactory factory)
        {
            fAxisFactory = factory;
        }

        public void RotateView(IRotation rotation, IUnit player, IMazeViewData mazeView, Dictionary<ILocation, INode> nodesByLocation)
        {
            player.ViewingAxis = GetFreeRotationAxis(mazeView);

            DoMoveViewToOrigin(mazeView, nodesByLocation);
            DoRotate(rotation, mazeView, nodesByLocation);

            player.ViewingAxis = GetFreeRotationAxis(mazeView);
            DoMoveViewToPlayer(player, mazeView, nodesByLocation);
        }

        private void ApplyRotation(IEnumerable<ILocation> vectorsToRotate, Matrix<double> rotationMatrix)
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

        private void DoMoveView(Vector<double> vectorToMoveBy, IMazeViewData mazeView, IReadOnlyDictionary<ILocation, INode> nodesByLocation)
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

                var temp = new Location(pointX, pointY, pointZ);

                nodesByLocation.TryGetValue(temp, out INode node);
                mazeView.MazeNodes[i] = node;
            }
        }

        private void DoMoveViewToOrigin(IMazeViewData mazeView, IReadOnlyDictionary<ILocation, INode> nodesByLocation)
        {
            int middle = (int)Math.Round((double)mazeView.MazeNodes.Count / 2);
            Vector<double> centerOfView = Vector<double>.Build.Dense(new double[] { mazeView.MazeNodes[middle].Location.PointX
                                                                                    , mazeView.MazeNodes[middle].Location.PointY
                                                                                    , mazeView.MazeNodes[middle].Location.PointZ });

            Vector<double> origin = Vector<double>.Build.Dense(new double[] { 0, 0, 0 });
            Vector<double> movement = origin - centerOfView;
            DoMoveView(movement, mazeView, nodesByLocation);
        }

        private void DoMoveViewToPlayer(IUnit player, IMazeViewData mazeView, IReadOnlyDictionary<ILocation, INode> nodesByLocation)
        {
            Vector<double> movement = player.ViewingAxis.GetVectorToPlayerAxis(player);
            DoMoveView(movement, mazeView, nodesByLocation);
        }

        private void DoRotate(IRotation rotation, IMazeViewData mazeView, IReadOnlyDictionary<ILocation, INode> nodesByLocation)
        {
            IAxis axis = rotation.GetRotationAxis(mazeView);
            double angle = DegreesToRadial(rotation.GetAngle());

            Matrix<double> rotationMatrix = axis.GetRotationMatrix(angle);
            DoRotateView(rotationMatrix, mazeView, nodesByLocation);
            ApplyRotation(mazeView.MovementCube, rotationMatrix);

            rotation.SetRotationAxisForDirection(mazeView, GetFreeRotationAxis(mazeView));
        }

        private void DoRotateView(Matrix<double> rotationMatrix, IMazeViewData mazeView, IReadOnlyDictionary<ILocation, INode> nodesByLocation)
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

        private IAxis GetFreeRotationAxis(IMazeViewData mazeView)
        {
            IList<IAxis> allAxis = new List<IAxis>() { fAxisFactory.CreateZAxis(), fAxisFactory.CreateXAxis(), fAxisFactory.CreateYAxis() };

            IAxis rotationAxis = fAxisFactory.CreateXAxis();
            foreach (IAxis axis in allAxis)
            {
                Type axisType = axis.GetType();
                Type leftRightAxisType = mazeView.LeftRightRotationAxis.GetType();
                Type upDownAxisType = mazeView.UpDownRotationAxis.GetType();

                if (leftRightAxisType == axisType || upDownAxisType == axisType)
                    continue;

                return axis;
            }

            return rotationAxis;
        }
    }
}