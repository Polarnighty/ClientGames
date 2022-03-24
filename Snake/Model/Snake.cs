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
    public class Snake
    {
        private double _gameBoardWidthPixels;
        private double _gameBoardHeightPixels;
        private ObservableCollection<SnakeBodyPart> _snakeBody;
        private volatile bool _updatingSnake;
        private static object _itemsLock = new object();
        public SnakeHead TheSnakeHead { get; }
        public SnakeEye TheSnakeEye { get; }
        public double GameBoardHeightPixels { get; internal set; }
        public double GameBoardWidthPixels { get; internal set; }

        public static event HitBoundary OnHitBoundary;
        public static event HitSnake OnHitSnake;
        public static event EatCherry OnEatCherry;

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
