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
        private bool ShouldDrawHorizontalPath(Node current, Node next)
        {
            return Validator.DoesPathToNodeExist(current, next);
        }

        /// <summary>
        /// returns true if vertex has edge to neighbour in y + 1 direction
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        private bool ShouldDrawVerticalPath(Node current, Node bottom)
        {
            return Validator.DoesPathToNodeExist(current, bottom);
        }

        public void Draw(MazeViewData mazeView, UnitList unitList)
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
                    Node current = null;
                    if (counter < mazeView.Count)
                        current = mazeView[counter];

                    Location locationToRight = current.Location + mazeView.MovementCube[(int)Direction.Right];
                    Location locationAtBottom = current.Location + mazeView.MovementCube[(int)Direction.Down];

                    Node right = mazeView.GetNodeAt(locationToRight);
                    Node bottom = mazeView.GetNodeAt(locationAtBottom);

                    Player player = unitList.GetPlayer();
                    symbol = ShouldDrawHorizontalPath(current, right) ? "---" : "   ";
                    if (current.Location == player.CurrentLocation)
                    {
                        horizontalEdges.AppendFormat("{1}{0}", symbol, "X");
                    }
                    else
                    {
                        int point = current.CollectablePoint;
                        horizontalEdges.AppendFormat("{1}{0}", symbol, point);
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