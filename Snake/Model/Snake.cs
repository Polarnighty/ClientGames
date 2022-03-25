using Snake.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Snake.Model
{
    public class Snake : NotificationBase
    {
        private double _gameBoardWidthPixels;
        private double _gameBoardHeightPixels;
        private ObservableCollection<SnakeBodyPart> _snakeBody;
        private volatile bool _updatingSnake;
        private static object _itemsLock = new object();


        public static event HitBoundary OnHitBoundary;
        public static event HitSnake OnHitSnake;
        public static event EatCherry OnEatCherry;


        /// <summary>
        /// 获取游戏宽度
        /// </summary>
        public double GameBoardWidthPixels
        {
            get
            {
                return _gameBoardWidthPixels;
            }
            set
            {
                _gameBoardWidthPixels = value;
                RaisePropertyChanged();

                // 更新蛇
                TheSnakeHead.GameBoardWidthPixels = value;
                TheSnakeEye.GameBoardWidthPixels = value;
                foreach (SnakeBodyPart bodyPart in _snakeBody)
                {
                    bodyPart.GameBoardWidthPixels = value;
                }
            }
        }

        /// <summary>
        /// 获取游戏高度
        /// </summary>
        public double GameBoardHeightPixels
        {
            get
            {
                return _gameBoardHeightPixels;
            }
            set
            {
                _gameBoardHeightPixels = value;
                RaisePropertyChanged();

                // Update the snake.
                TheSnakeHead.GameBoardHeightPixels = value;
                TheSnakeEye.GameBoardHeightPixels = value;
                foreach (SnakeBodyPart bodyPart in _snakeBody)
                {
                    bodyPart.GameBoardHeightPixels = value;
                }
            }
        }

        public SnakeHead TheSnakeHead { get; }

        public SnakeEye TheSnakeEye { get; }

        public ObservableCollection<SnakeBodyPart> TheSnakeBody
        {
            get
            {
                if (_snakeBody == null)
                {
                    _snakeBody = new ObservableCollection<SnakeBodyPart>();
                }

                return _snakeBody;
            }
        }


        /// <summary>
        /// 绘制画板
        /// </summary>
        /// <param name="gameBoardWidthPixels"></param>
        /// <param name="gameBoardHeightPixels"></param>
        public Snake(double gameBoardWidthPixels, double gameBoardHeightPixels)
        {
            _gameBoardWidthPixels = gameBoardWidthPixels;
            _gameBoardHeightPixels = gameBoardHeightPixels;
            TheSnakeHead = new SnakeHead(gameBoardWidthPixels, gameBoardHeightPixels, Constants.DefaultXposition, Constants.DefaultYposition, Constants.DefaultDirection);
            TheSnakeEye = new SnakeEye(gameBoardWidthPixels, gameBoardHeightPixels, Constants.DefaultXposition, Constants.DefaultYposition, Constants.DefaultDirection);
            _snakeBody = new ObservableCollection<SnakeBodyPart>();
            BindingOperations.EnableCollectionSynchronization(_snakeBody, _itemsLock);
            _updatingSnake = false;
        }

        /// <summary>
        /// 设置蛇头朝向
        /// </summary>
        /// <param name="direction"></param>
        public void SetSnakeDirection(Direction direction)
        {
            while (_updatingSnake)
            {
                Thread.Sleep(50);
            }

            _updatingSnake = true;
            TheSnakeHead.DirectionOfTravel = direction;
            TheSnakeEye.DirectionOfTravel = direction;
            _updatingSnake = false;
        }

        public void UpdateSnakeStatus(Cherry theCherry)
        {
            while (_updatingSnake)
            {
                Thread.Sleep(50);
            }

            _updatingSnake = true;

            TheSnakeHead.UpdatePosition();    // 更新蛇头位置
            TheSnakeEye.UpdatePosition();     // 更新蛇眼位置

            // 更新蛇身位置
            Direction previousDirection;
            Direction nextDirection = TheSnakeHead.DirectionOfTravel;
            foreach (SnakeBodyPart bodyPart in _snakeBody)
            {
                bodyPart.UpdatePosition();
                previousDirection = bodyPart.DirectionOfTravel;
                bodyPart.DirectionOfTravel = nextDirection;
                nextDirection = previousDirection;
            }




            // 检查蛇是否触及边界。
            if (TheSnakeHead.HitBoundary())
            {
                // 蛇触及边界 - 引发 OnHitBoundary 事件。
                OnHitBoundary?.Invoke();
            }

            // 检查蛇是否撞到自己
            if (TheSnakeHead.HitSelf(_snakeBody))
            {
                // 蛇撞到了自己 - 引发 OnHitSnake 事件
                OnHitSnake?.Invoke();
            }

            // 检查蛇是否吃到樱桃.
            if (TheSnakeHead.EatCherry(theCherry))
            {
                // 蛇吃了樱桃——增加蛇的长度
                SnakeBodyPart snakeBodyPart = new SnakeBodyPart(_gameBoardWidthPixels, _gameBoardHeightPixels, this);
                _snakeBody.Add(snakeBodyPart);

                // 蛇吃了樱桃--触发OnEatCherry 事件.
                OnEatCherry?.Invoke();
            }

            _updatingSnake = false;
        }

    }
}
