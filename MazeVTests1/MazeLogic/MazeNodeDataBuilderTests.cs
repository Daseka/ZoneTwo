
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

using MazeV.MazeLogic;
using MazeV.MazeLogic.Settings;
using MazeV.MazeLogic.MazeNodes;
using MazeV.MazeLogic.CollectableItems;
using MazeV.MazeLogic.Validators;

namespace MazeVTests1.MazeLogic
{
    public class MazeNodeDataBuilderTests
    {    
        [Fact]
        public void GenerateNodeDataTest()
        {
            var settings = new DefaultSettings();
            var nodeBuilder = new NodeBuilder(settings, new CoinBuilder(settings), new Validator());
            var mazeNodeDataBuilderbuilder = new MazeNodeDataBuilder(new FakeMazeNodeDataBuilderSettings(5,3), new Randomizer(), nodeBuilder);
            IMazeNodeData data = mazeNodeDataBuilderbuilder.GenerateNodeData(12345);

            Assert.NotNull(data);
        }        
    }
}