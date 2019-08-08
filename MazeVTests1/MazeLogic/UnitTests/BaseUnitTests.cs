using MazeV.MazeLogic;
using MazeV.MazeLogic.CollectableItems;
using MazeV.MazeLogic.MazeNodes;
using MazeV.MazeLogic.Settings;
using MazeV.MazeLogic.Units;
using MazeV.MazeLogic.Validators;
using System.Drawing;
using Xunit;

namespace MazeVTests1.MazeLogic
{
    public class BaseUnitTests
    {
        [Fact]
        public void ShouldAssignLocation()
        {
            var baseUnit = new BaseUnit();
            var newLocation = new Location(1, 2, 3);

            baseUnit.AssignLocation(newLocation);

            Assert.True(baseUnit.CurrentLocation.Equals(newLocation));
        }

        [Fact]
        public void ShouldDraw()
        {
            var baseUnit = new BaseUnit();
            var fakeGrapic = new FakeGrapics();
            var fakeRectangle = new Rectangle();
            var settings = new DefaultSettings();
            var fakeNode = new Node(new CoinBuilder(settings),settings, new Validator());

            baseUnit.Draw(fakeGrapic, fakeRectangle, fakeNode);
            var expectedMethodcalled = nameof(FakeGrapics.FillRectangle);

            Assert.True(fakeGrapic.MethodCalled == expectedMethodcalled);
        }

        [Fact]
        public void ShouldNotDraw()
        {
            var baseUnit = new BaseUnit();
            var fakeGrapic = new FakeGrapics();
            var fakeRectangle = new Rectangle();
            var settings = new DefaultSettings();
            var fakeNode = new Node(new CoinBuilder(settings), settings, new Validator())
            {
                Location = new Location(2,2,2)
            };

            baseUnit.Draw(fakeGrapic, fakeRectangle, fakeNode);
            var expectedMethodcalled = nameof(FakeGrapics.FillRectangle);

            Assert.False(fakeGrapic.MethodCalled == expectedMethodcalled);
        }
    }
}