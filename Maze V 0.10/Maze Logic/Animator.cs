using System.Timers;

namespace MazeV.Maze_Logic
{
    internal class Animator
    {
        private readonly Timer fTimer;

        public Animator()
        {
            fTimer = new Timer();
        }

        public void Start()
        {
            fTimer.Start();
        }

        public void Stop()
        {
            fTimer.Stop();
        }
    }
}