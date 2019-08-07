using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Forms;
using EnvDTE;
using MazeV.MazeLogic;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace MazeV
{
    public partial class Form1 : Form
    {
        private readonly Timer fAnimator;
        private Maze fMaze;

        public Form1(IAxisFactory axisFactory, IMazeViewDataFactory mazeViewDataFactory)
        {
            InitializeComponent();                       
            InitializeMaze(axisFactory, mazeViewDataFactory);
            InitializeKeybindings(axisFactory);

            DoubleBuffered = true;

            fAnimator = new Timer() { Interval = 500, };
            fAnimator.Tick += FAnimator_Tick1;
            fAnimator.Start();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            ICommand command = Keybindings.GetCommand(keyData);
            command.Execute();

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void FAnimator_Tick1(object sender, System.EventArgs e)
        {
            MoveUnits();
            UpdateNodeScore();

            pictureBox1.Refresh();
        }

        private void InitializeKeybindings(IAxisFactory axisFactory)
        {
            Keybindings.Add(Keys.Up, new RotateCommand(new UpRotation(), fMaze.Hero, fMaze.ViewData, fMaze.NodeData, axisFactory));
            Keybindings.Add(Keys.Down, new RotateCommand(new DownRotation(), fMaze.Hero, fMaze.ViewData, fMaze.NodeData, axisFactory));
            Keybindings.Add(Keys.Left, new RotateCommand(new LeftRotation(), fMaze.Hero, fMaze.ViewData, fMaze.NodeData, axisFactory));
            Keybindings.Add(Keys.Right, new RotateCommand(new RightRotation(), fMaze.Hero, fMaze.ViewData, fMaze.NodeData, axisFactory));

            Keybindings.Add(Keys.W, new MoveCommand(new UpDirection(), fMaze.Hero));
            Keybindings.Add(Keys.S, new MoveCommand(new DownDirection(), fMaze.Hero));
            Keybindings.Add(Keys.A, new MoveCommand(new LeftDirection(), fMaze.Hero));
            Keybindings.Add(Keys.D, new MoveCommand(new RightDirection(), fMaze.Hero));
        }

        private void InitializeMaze(IAxisFactory axisFactory, IMazeViewDataFactory mazeViewDataFactory)
        {
            MazeNodeDataBuilder nodeBuilder = new MazeNodeDataBuilder(11, 3);
            IMazeNodeData nodeData = nodeBuilder.GenerateNodeData(12345);
            IMazeViewData viewData = nodeBuilder.GenerateViewData(nodeData, axisFactory, mazeViewDataFactory);

            fMaze = new Maze(nodeData, viewData);
            fMaze.Initialize();
        }

        private void MoveUnits()
        {
            UnitMover unitMove = new UnitMover(new DefaultMovementLogic());

            Player player = fMaze.UnitList.GetPlayer();
            unitMove.MovePlayer(player.FutureMovementDirection, player, fMaze.NodeData, fMaze.ViewData);
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            IVisualizer visualizer = new CanvasVisualizer(e.Graphics);

            visualizer.Draw(fMaze.ViewData, fMaze.UnitList);
            fMaze.ProcessPlayerInNode();
        }

        private void UpdateNodeScore()
        {
            int totalNodes = fMaze.NodeData.GetTotalNodeCount();
            int collectableNodes = fMaze.NodeData.GetCollectableNodes();

            this.Text = $" {collectableNodes} / {totalNodes} ";
        }
    }
}