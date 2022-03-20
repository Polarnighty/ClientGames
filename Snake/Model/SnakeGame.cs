using Microsoft.VisualBasic;
using Snake.Common;
using System.Windows.Threading;

namespace Snake.Model
{
    public class SnakeGame : NotificationBase
    {

        private Snake _theSnake;
        private Cherry _theCherry;
        private double _gameBoardWidthPixels;
        private double _gameBoardHeightPixels;
        private DispatcherTimer _gameTimer;
        private int _gameStepMilliSeconds;
        private int _gameLevel;
        private bool _isGameOver;
        private int _restartCountdownSeconds;
        private DispatcherTimer _restartTimer;

        public SnakeGame()
        {
            // Initialise the game board.
            GameBoardWidthPixels = Constants.DefaultGameBoardWidthPixels;
            GameBoardHeightPixels = Constants.DefaultGameBoardHeightPixels;

            // Listen for events from the snake.
            Snake.OnHitBoundary += new HitBoundary(HitBoundaryEventHandler);
            Snake.OnHitSnake += new HitSnake(HitSnakeEventHandler);
            Snake.OnEatCherry += new EatCherry(EatCherryEventHandler);

        }
        public Snake TheSnake
        {
            get
            {
                if (_theSnake == null)
                {
                    _theSnake = new Snake(GameBoardWidthPixels, GameBoardHeightPixels);
                }

                return _theSnake;
            }
            private set
            {
                _theSnake = value;
                RaisePropertyChanged();
            }
        }

        public double GameBoardWidthPixels
        {
            get
            {
                return (int)_gameBoardWidthPixels;
            }
            set
            {
                _gameBoardWidthPixels = value;
                RaisePropertyChanged();

                TheSnake.GameBoardWidthPixels = value;
            }
        }


        /// <summary>
        /// Gets or sets the game over boolean flag.
        /// </summary>
        public bool IsGameOver
        {
            get
            {
                return _isGameOver;
            }
            private set
            {
                _isGameOver = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(IsGameRunning));
            }
        }

        /// <summary>
        /// Gets the game running boolean flag.
        /// </summary>
        public bool IsGameRunning
        {
            get
            {
                return !IsGameOver;
            }
        }

        public void ProcessKeyboardEvent(Direction direction)
        {
            TheSnake.SetSnakeDirection(direction);
        }

    }
}
