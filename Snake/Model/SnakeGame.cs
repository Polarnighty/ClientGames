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

        private void HitBoundaryEventHandler()
        {
            IsGameOver = true;
        }

        /// <summary>
        /// The HitSnakeEventHandler is called to process an OnHitSnake event.
        /// </summary>
        private void HitSnakeEventHandler()
        {
            IsGameOver = true;
        }
        private void EatCherryEventHandler()
        {
            //// Move the cherry to a new location, away from the snake.
            //TheCherry.MoveCherry(TheSnake);

            //// Increase the game level and speed.
            //_gameLevel++;
            //RaisePropertyChanged(nameof(TitleText));
            //if (_gameLevel < Constants.EndLevel)
            //{
            //    _gameStepMilliSeconds = _gameStepMilliSeconds - Constants.DecreaseGameStepMilliSeconds;
            //    _gameTimer.Interval = TimeSpan.FromMilliseconds(_gameStepMilliSeconds);
            //}
            //else
            //{
            //    // Maximum level reached - game is complete.
            //    IsGameOver = true;
            //}
        }
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
        public double GameBoardHeightPixels
        {
            get
            {
                return (int)_gameBoardHeightPixels;
            }
            set
            {
                _gameBoardHeightPixels = value;
                RaisePropertyChanged();

                TheSnake.GameBoardHeightPixels = value;
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
