using MazeV.Maze_Logic;
using System.Windows.Forms;

namespace MazeV
{
    public partial class Form1 : Form
    {
        private Maze fMaze;
        private readonly Timer fAnimator;

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
            MazeNodeDataBuilder nodeBuilder = new MazeNodeDataBuilder(11, 3);
            MazeNodeData nodeData = nodeBuilder.GenerateNodeData(12345);
            MazeViewData viewData = nodeBuilder.GenerateViewData(nodeData);

            fMaze = new Maze(nodeData, viewData);
            fMaze.Initialize();
        }

        private void InitializeKeybindings()
        {
            Keybindings.Add(Keys.Up, new RotateCommand(Rotation.Up, fMaze.Hero, fMaze.ViewData, fMaze.NodeData));
            Keybindings.Add(Keys.Down, new RotateCommand(Rotation.Down, fMaze.Hero, fMaze.ViewData, fMaze.NodeData));
            Keybindings.Add(Keys.Left, new RotateCommand(Rotation.Left, fMaze.Hero, fMaze.ViewData, fMaze.NodeData));
            Keybindings.Add(Keys.Right, new RotateCommand(Rotation.Right, fMaze.Hero, fMaze.ViewData, fMaze.NodeData));

            Keybindings.Add(Keys.W, new MoveCommand(Direction.Up, fMaze.Hero));
            Keybindings.Add(Keys.S, new MoveCommand(Direction.Down, fMaze.Hero));
            Keybindings.Add(Keys.A, new MoveCommand(Direction.Left, fMaze.Hero));
            Keybindings.Add(Keys.D, new MoveCommand(Direction.Right, fMaze.Hero));
        }

        private void MoveUnits()
        {
            UnitMover unitMove = new UnitMover();
            Player player = fMaze.UnitList.GetPlayer();
            unitMove.MovePlayer(player.FutureMovementDirection, player, fMaze.NodeData, fMaze.ViewData);
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

            visualizer.Draw(fMaze.ViewData, fMaze.UnitList);
            fMaze.ProcessPlayerInNode();
        }
    }
}