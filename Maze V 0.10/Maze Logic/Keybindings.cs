using System.Collections.Generic;
using System.Windows.Forms;

namespace MazeV.Maze_Logic
{
    public static class Keybindings
    {
        private static Dictionary<Keys, ICommand> fBindingList = new Dictionary<Keys, ICommand>();

        public static void Add(Keys key, ICommand command)
        {
            fBindingList[key] = command;
        }

        public static ICommand GetCommand(Keys key)
        {
            fBindingList.TryGetValue(key, out ICommand command);
            return command ?? new EmptyCommand();
        }
    }
}