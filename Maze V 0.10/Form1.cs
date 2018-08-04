using MazeV.Maze_Logic;
using System.Windows.Forms;

namespace MazeV
{
    public partial class Form1 : Form
    {
        private Maze fMaze = new Maze();
        private Timer fAnimator;

        public Form1()
        {
            InitializeComponent();
            InitializeMaze();
            InitializeKeybindings();
            
            this.DoubleBuffered = true;

            fAnimator = new Timer();
            fAnimator.Interval = 500;
            fAnimator.Tick += FAnimator_Tick1;
            fAnimator.Start();
        }

        private void InitializeMaze()
        {
            fMaze.Initialize(11, 3);
            fMaze.GenerateLayoutData(12345);
        }

        private void InitializeKeybindings()
        {
            Keybindings.Add(Keys.Up, new RotateCommand(Rotation.Up, fMaze.Hero, fMaze.MazeView, fMaze.NodesByLocation));
            Keybindings.Add(Keys.Down, new RotateCommand(Rotation.Down, fMaze.Hero, fMaze.MazeView, fMaze.NodesByLocation));
            Keybindings.Add(Keys.Left, new RotateCommand(Rotation.Left, fMaze.Hero, fMaze.MazeView, fMaze.NodesByLocation));
            Keybindings.Add(Keys.Right, new RotateCommand(Rotation.Right, fMaze.Hero, fMaze.MazeView, fMaze.NodesByLocation));

            Keybindings.Add(Keys.W, new MoveCommand(Direction.Up, fMaze.Hero));
            Keybindings.Add(Keys.S, new MoveCommand(Direction.Down, fMaze.Hero));
            Keybindings.Add(Keys.A, new MoveCommand(Direction.Left, fMaze.Hero));
            Keybindings.Add(Keys.D, new MoveCommand(Direction.Right, fMaze.Hero));
        }

        private void MoveUnits()
        {
            UnitMover unitMove = new UnitMover();
            Player player = fMaze.UnitList.GetPlayer();
            unitMove.MovePlayer(player.FutureMovementDirection, player, fMaze.NodesByIndex, fMaze.MazeView);
        }

        private void FAnimator_Tick1(object sender, System.EventArgs e)                    
        {
            MoveUnits();

            pictureBox1.Refresh();
        }
    
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            ICommand command = Keybindings.GetCommand(keyData);
            command.Execute();
            
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            IVisualizer visualizer = new CanvasVisualizer(e.Graphics);

            visualizer.Draw(fMaze.MazeView, fMaze.UnitList);            
            fMaze.ProcessPlayerInNode();
        }
    }
}
