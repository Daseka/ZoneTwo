namespace MazeV.MazeLogic.Movement.Directions
{
    public class DirectionRetriever
    {
        private readonly LeftDirection _leftDirection;
        private readonly RightDirection _rightDirection;
        private readonly UpDirection _upDirection;
        private readonly DownDirection _downDirection;

        public DirectionRetriever(
            LeftDirection leftDirection, 
            RightDirection rightDirection, 
            UpDirection upDirection, 
            DownDirection downDirection)
        {
            _leftDirection = leftDirection;
            _rightDirection = rightDirection;
            _upDirection = upDirection;
            _downDirection = downDirection;
        }

        public IDirection GetUp()
        {
            return _upDirection;
        }

        public IDirection GetDown()
        {
            return _downDirection;
        }

        public IDirection GetLeft()
        {
            return _leftDirection;
        }

        public IDirection GetRight()
        {
            return _rightDirection;
        }
    }
}