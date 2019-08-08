
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

using MazeV.MazeLogic;

namespace MazeVTests1.MazeLogic
{
    public class MazeNodeDataBuilderTests
    {    
        [Fact]
        public void GenerateNodeDataTest()
        {
            var settings = new DefaultSettings();
            var nodeBuilder = new NodeBuilder(settings, new CoinBuilder(settings));
            var mazeNodeDataBuilderbuilder = new MazeNodeDataBuilder(new FakeMazeNodeDataBuilderSettings(5,3), new Randomizer(), nodeBuilder);
            IMazeNodeData data = mazeNodeDataBuilderbuilder.GenerateNodeData(12345);

            Assert.NotNull(data);
        }        
    }
}