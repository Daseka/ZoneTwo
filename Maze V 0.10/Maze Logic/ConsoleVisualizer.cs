using System;
using System.Text;

namespace MazeV.Maze_Logic
{
    public class ConsoleVisualizer : IVisualizer
    {
        /// <summary>
        /// returns true if vertex has edge to its horizontal neighbour in x + 1 direction
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        private bool ShouldDrawHorizontalPath(INode current, INode next)
        {
            return Validator.DoesPathToNodeExist(current, next);
        }

        /// <summary>
        /// returns true if vertex has edge to neighbour in y + 1 direction
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        private bool ShouldDrawVerticalPath(INode current, INode bottom)
        {
            return Validator.DoesPathToNodeExist(current, bottom);
        }

        public void Draw(IMazeViewData mazeView, UnitList unitList)
        {
            int counter = 0;
            string symbol = string.Empty;
            StringBuilder horizontalEdges = new StringBuilder();
            StringBuilder verticalEdges = new StringBuilder();

            Console.Clear();
            Console.WriteLine();

            for (int y = mazeView.ViewStart; y <= mazeView.ViewEnd; y++)
            {
                for (int x = mazeView.ViewStart; x <= mazeView.ViewEnd; x++)
                {
                    INode current = null;
                    if (counter < mazeView.MazeNodes.Count)
                        current = mazeView.MazeNodes[counter];

                    ILocation locationToRight = current.Location.Add( mazeView.MovementCube[new RightDirection().Value]);
                    ILocation locationAtBottom = current.Location.Add( mazeView.MovementCube[new LeftDirection().Value]);

                    INode right = mazeView.GetNodeAt(locationToRight);
                    INode bottom = mazeView.GetNodeAt(locationAtBottom);

                    Player player = unitList.GetPlayer();
                    symbol = ShouldDrawHorizontalPath(current, right) ? "---" : "   ";
                    if (current.Location == player.CurrentLocation)
                    {
                        horizontalEdges.AppendFormat("{1}{0}", symbol, "X");
                    }
                    else
                    {                        
                        horizontalEdges.AppendFormat($"{symbol}{current.CollectablePoint.IsCollected}");
                    }

                    symbol = ShouldDrawVerticalPath(current, bottom) ? "|" : " ";
                    verticalEdges.AppendFormat("{0}   ", symbol);

                    counter++;
                }

                Console.WriteLine(horizontalEdges);
                Console.WriteLine(verticalEdges);
                Console.WriteLine(verticalEdges);

                horizontalEdges.Clear();
                verticalEdges.Clear();
            }
        }
    }
}