using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}
