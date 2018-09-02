using System;
using System.Drawing;

namespace MazeV.Maze_Logic
{
    public class CanvasVisualizer : IVisualizer
    {
        private readonly Graphics fGraphic;

        public CanvasVisualizer(Graphics graphic)
        {                          
            fGraphic = graphic?? throw new ArgumentException("No Graphic found cant draw"); 
        }

        public void Draw(MazeViewData mazeView, UnitList unitList)
        {       
            fGraphic.Clear(Color.White);

            int index = 0;

            for (int y = mazeView.ViewStart; y <= mazeView.ViewEnd; y++)
            {
                for (int x = mazeView.ViewStart; x <= mazeView.ViewEnd; x++)
                {
                    Node node = mazeView[index];
                    DrawNode(x, y, node, fGraphic, mazeView, unitList);
                    index++;
                }
            }
        }

        private int GetNodePoint(int xOrY, int nodeSquareSize, int mazeViewStart)
        {
            int graphicOffset = 1 - mazeViewStart;
            return (xOrY + graphicOffset) * nodeSquareSize;
        }

        private void DrawNode(int x, int y, Node node, Graphics grapic, MazeViewData mazeView, UnitList unitList)
        {
            int indeX = GetNodePoint(x, node.SquareSize, mazeView.ViewStart);
            int indeY = GetNodePoint(y, node.SquareSize, mazeView.ViewStart);

            DrawNode(node, grapic, mazeView, indeX, indeY);
            DrawCollectable(node, grapic, indeX, indeY);            
            DrawPlayer(node, grapic, unitList, indeX, indeY);                       
        }

        private static void DrawNode(Node node, Graphics grapic, MazeViewData mazeView, int indeX, int indeY)
        {
            int HalfOfNodeSize = DefaultSettings.HalfOfNodeSize;
            Point topLeft = new Point(indeX - HalfOfNodeSize, indeY - HalfOfNodeSize);
            Point topRight = new Point(indeX + HalfOfNodeSize, indeY - HalfOfNodeSize);
            Point bottomLeft = new Point(indeX - HalfOfNodeSize, indeY + HalfOfNodeSize);
            Point bottomRight = new Point(indeX + HalfOfNodeSize, indeY + HalfOfNodeSize);
            node.Draw(node, grapic, mazeView, topLeft, topRight, bottomLeft, bottomRight);
        }

        private static void DrawPlayer(Node node, Graphics grapic, UnitList unitList, int indeX, int indeY)
        {
            Player player = unitList.GetPlayer();
            Rectangle playerRect = new Rectangle(indeX - DefaultSettings.HalfOfUnitSize, indeY - DefaultSettings.HalfOfUnitSize, DefaultSettings.UnitSize, DefaultSettings.UnitSize);
            player.Draw(grapic, playerRect, node);
        }

        private static void DrawCollectable(Node node, Graphics grapic, int indeX, int indeY)
        {
            Rectangle rectangle = new Rectangle(indeX - DefaultSettings.HalfOfCollectableSize, indeY - DefaultSettings.HalfOfCollectableSize, DefaultSettings.CollectableSize, DefaultSettings.CollectableSize);
            node.CollectablePoint.Draw(grapic, rectangle);
        }
    }
}