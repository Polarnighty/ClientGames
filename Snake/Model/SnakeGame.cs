using Microsoft.VisualBasic;
using Snake.Common;
using System;
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


        /// <summary>
        /// 获取游戏宽度
        /// </summary>
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
        /// 获取游戏高度
        /// </summary>
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

        public Cherry TheCherry
        {
            get
            {
                if (_theCherry == null)
                {
                    _theCherry = new Cherry(_gameBoardWidthPixels, _gameBoardHeightPixels, TheSnake.TheSnakeHead.XPosition, TheSnake.TheSnakeHead.YPosition);
                }

                return _theCherry;
            }
            private set
            {
                _theCherry = value;
                RaisePropertyChanged();
            }
        }

        public string TitleText
        {
            get
            {
                return "Snake " + _gameLevel + "/" + Constants.EndLevel;
            }
        }

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

        public bool IsGameRunning
        {
            get
            {
                return !IsGameOver;
            }
        }

        /// <summary>
        /// 重置计时器
        /// </summary>
        public int RestartCountdownSeconds
        {
            get
            {
                return _restartCountdownSeconds;
            }
            private set
            {
                _restartCountdownSeconds = value;
                RaisePropertyChanged();
            }
        }

        private void HitBoundaryEventHandler()
        {
            IsGameOver = true;
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

            StartNewGame();
        }

        #region 游戏开始与结束
        /// <summary>
        /// The HitSnakeEventHandler is called to process an OnHitSnake event.
        /// </summary>
        private void HitSnakeEventHandler()
        {
            IsGameOver = true;
        }

        private void GameTimerEventHandler(object sender, EventArgs e)
        {
            if (IsGameOver)
            {
                // 游戏结束
                if (_gameTimer.IsEnabled)
                {
                    _gameTimer.Stop();  // 停止计时
                    RestartGame();      // 重启游戏
                }
            }
            else
            {
                // 游戏运行,更新樱桃位置
                TheSnake.UpdateSnakeStatus(TheCherry);
            }
        }
        private void RestartGame()
        {
            // 初始化倒计时
            RestartCountdownSeconds = Constants.RestartCountdownStartSeconds;
            _restartTimer = new DispatcherTimer();
            _restartTimer.Interval = TimeSpan.FromMilliseconds(Constants.RestartStepMilliSeconds);
            _restartTimer.Tick += new EventHandler(RestartTimerEventHandler);
            _restartTimer.Start();
        }
        private void RestartTimerEventHandler(object sender, EventArgs e)
        {
            RestartCountdownSeconds--;

            if (RestartCountdownSeconds == 0)
            {
                _restartTimer.Stop();   // Stop the restart timer.
                StartNewGame();         // Start a new game.
            }
        }

        private void StartNewGame()
        {
            // 初始化蛇和樱桃
            TheSnake = new Snake(_gameBoardWidthPixels, _gameBoardHeightPixels);
            TheCherry = new Cherry(_gameBoardWidthPixels, _gameBoardHeightPixels, TheSnake.TheSnakeHead.XPosition, TheSnake.TheSnakeHead.YPosition);

            // Set the game over flag.
            IsGameOver = false;

            // Reset the restart timer.
            RestartCountdownSeconds = Constants.RestartCountdownStartSeconds;

            // Initialise the game timer.
            _gameLevel = Constants.StartLevel;
            RaisePropertyChanged(nameof(TitleText));
            _gameStepMilliSeconds = Constants.DefaultGameStepMilliSeconds;
            _gameTimer = new DispatcherTimer();
            _gameTimer.Interval = TimeSpan.FromMilliseconds(_gameStepMilliSeconds);
            _gameTimer.Tick += new EventHandler(GameTimerEventHandler);
            _gameTimer.Start();
        }
        #endregion

        private void EatCherryEventHandler()
        {
            // Move the cherry to a new location, away from the snake.
            TheCherry.MoveCherry(TheSnake);

            // Increase the game level and speed.
            _gameLevel++;
            RaisePropertyChanged(nameof(TitleText));
            if (_gameLevel < Constants.EndLevel)
            {
                _gameStepMilliSeconds = _gameStepMilliSeconds - Constants.DecreaseGameStepMilliSeconds;
                _gameTimer.Interval = TimeSpan.FromMilliseconds(_gameStepMilliSeconds);
            }
            else
            {
                // Maximum level reached - game is complete.
                IsGameOver = true;
            }
        }

        public void ProcessKeyboardEvent(Direction direction)
        {
            TheSnake.SetSnakeDirection(direction);
        }

    }
}
