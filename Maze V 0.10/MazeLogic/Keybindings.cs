using System.Collections.Generic;
using System.Windows.Forms;

namespace MazeV.MazeLogic
{
    public class Keybindings
    {
        private Dictionary<Keys, ICommand> fBindingList = new Dictionary<Keys, ICommand>();

        public void Add(Keys key, ICommand command)
        {
            fBindingList[key] = command;
        }

        public ICommand GetCommand(Keys key)
        {
            fBindingList.TryGetValue(key, out ICommand command);
            return command ?? new EmptyCommand();
        }
    }
}