using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Forms;
using EnvDTE;
using MazeV.MazeLogic;
using MazeV.MazeLogic.Drawing;
using MazeV.MazeLogic.Keybindings;
using MazeV.MazeLogic.Keybindings.Commands;
using MazeV.MazeLogic.MazeNodes;
using MazeV.MazeLogic.MazeViews;
using MazeV.MazeLogic.Movement;
using MazeV.MazeLogic.Rotation;
using MazeV.MazeLogic.Visualizer;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace MazeV
{
    public partial class Form1 : Form
    {
        private readonly UnitMover _unitMover;
        private readonly IVisualizer _visualizer;
        private readonly MazeNodeDataBuilder _mazeNodeDataBuilder;
        private readonly Keybindings _keybindings;
        private readonly Timer fAnimator;
        private Maze _maze;

        public Form1(
            IAxisFactory axisFactory, 
            IMazeViewDataFactory mazeViewDataFactory, 
            MazeNodeDataBuilder mazeNodeDataBuilder, 
            IVisualizer visualizer,
            UnitMover unitMover,
            Maze maze,
            Keybindings keybindings)
        {
            _maze = maze;
            _unitMover = unitMover;
            _visualizer = visualizer;
            _mazeNodeDataBuilder = mazeNodeDataBuilder;
            _keybindings = keybindings;

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
            ICommand command = _keybindings.GetCommand(keyData);
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
            _keybindings.Add(Keys.Up, new RotateCommand(new UpRotation(), _maze.Hero, _maze.ViewData, _maze.NodeData, axisFactory));
            _keybindings.Add(Keys.Down, new RotateCommand(new DownRotation(), _maze.Hero, _maze.ViewData, _maze.NodeData, axisFactory));
            _keybindings.Add(Keys.Left, new RotateCommand(new LeftRotation(), _maze.Hero, _maze.ViewData, _maze.NodeData, axisFactory));
            _keybindings.Add(Keys.Right, new RotateCommand(new RightRotation(), _maze.Hero, _maze.ViewData, _maze.NodeData, axisFactory));

            _keybindings.Add(Keys.W, new MoveCommand(new UpDirection(), _maze.Hero));
            _keybindings.Add(Keys.S, new MoveCommand(new DownDirection(), _maze.Hero));
            _keybindings.Add(Keys.A, new MoveCommand(new LeftDirection(), _maze.Hero));
            _keybindings.Add(Keys.D, new MoveCommand(new RightDirection(), _maze.Hero));
        }

        private void InitializeMaze(IAxisFactory axisFactory, IMazeViewDataFactory mazeViewDataFactory)
        {            
            IMazeNodeData nodeData = _mazeNodeDataBuilder.GenerateNodeData(12345);
            IMazeViewData viewData = _mazeNodeDataBuilder.GenerateViewData(nodeData, axisFactory, mazeViewDataFactory);
            
            _maze.Initialize(nodeData, viewData);
        }

        private void MoveUnits()
        {            
            Player player = _maze.UnitList.GetPlayer();
            _unitMover.MovePlayer(player.FutureMovementDirection, player, _maze.NodeData, _maze.ViewData);
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            var graphic = new MazeGraphic(e.Graphics);
            _visualizer.Draw(graphic, _maze.ViewData, _maze.UnitList);
            _maze.ProcessPlayerInNode();
        }

        private void UpdateNodeScore()
        {
            int totalNodes = _maze.NodeData.GetTotalNodeCount();
            int collectableNodes = _maze.NodeData.GetCollectableNodes();

            this.Text = $" {collectableNodes} / {totalNodes} ";
        }
    }
}