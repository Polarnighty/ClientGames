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


    }
}
