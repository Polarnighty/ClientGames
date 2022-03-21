namespace Snake.Model
{
    public abstract class SnakePart: GameBoardItem
    {
        protected Direction _directionOfTravel;

        public SnakePart(double gameBoardWidthPixels, double gameBoardHeightPixels, Direction direction)
         : base(gameBoardWidthPixels, gameBoardHeightPixels)
        {
            _directionOfTravel = direction;
        }

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

        public double DirectionOfTravelDegrees
        {
            get
            {
                double direction = 0;
                switch (_directionOfTravel)
                {
                    case Direction.Up:
                        direction = Constants.DirectionUpDegrees;
                        break;
                    case Direction.Down:
                        direction = Constants.DirectionDownDegrees;
                        break;
                    case Direction.Left:
                        direction = Constants.DirectionLeftDegrees;
                        break;
                    case Direction.Right:
                        direction = Constants.DirectionRightDegrees;
                        break;
                    default:
                        break;
                }               
                return direction;
            }
        }

        public void UpdatePosition()
        {
            switch (_directionOfTravel)
            {
                case Direction.Up:
                    YPosition = YPosition - Constants.StepSize;
                    break;
                case Direction.Down:
                    YPosition = YPosition + Constants.StepSize;
                    break;
                case Direction.Left:
                    XPosition = XPosition - Constants.StepSize;
                    break;
                case Direction.Right:
                    XPosition = XPosition + Constants.StepSize;
                    break;
                default:
                    break;
            }  
        }

    }
}