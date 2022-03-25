namespace Snake.Model
{
    public abstract class SnakePart: GameBoardItem
    {
        protected Direction _directionOfTravel;
        public SnakePart()
        {
        }

        public SnakePart(double gameBoardWidthPixels, double gameBoardHeightPixels, Direction direction)
         : base(gameBoardWidthPixels, gameBoardHeightPixels)
        {
            _directionOfTravel = direction;
        }
        /// <summary>
        /// 蛇头当前朝向
        /// </summary>
        public Direction DirectionOfTravel
        {
            get
            {
                return _directionOfTravel;
            }
            set
            {
                _directionOfTravel = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(DirectionOfTravelDegrees));
            }
        }
        /// <summary>
        /// 蛇头朝向
        /// </summary>
        public double DirectionOfTravelDegrees
        {
            get
            {
                double direction = 0;
                if (_directionOfTravel == Direction.Up)
                {
                    direction = Constants.DirectionUpDegrees;
                }
                else if (_directionOfTravel == Direction.Right)
                {
                    direction = Constants.DirectionRightDegrees;
                }
                else if (_directionOfTravel == Direction.Down)
                {
                    direction = Constants.DirectionDownDegrees;
                }
                else if (_directionOfTravel == Direction.Left)
                {
                    direction = Constants.DirectionLeftDegrees;
                }
                return direction;
            }
        }

        public void UpdatePosition()
        {
            if (_directionOfTravel == Direction.Up)
            {
                YPosition = YPosition - Constants.StepSize;
            }
            else if (_directionOfTravel == Direction.Right)
            {
                XPosition = XPosition + Constants.StepSize;
            }
            else if (_directionOfTravel == Direction.Down)
            {
                YPosition = YPosition + Constants.StepSize;
            }
            else if (_directionOfTravel == Direction.Left)
            {
                XPosition = XPosition - Constants.StepSize;
            }
        }

    }
}