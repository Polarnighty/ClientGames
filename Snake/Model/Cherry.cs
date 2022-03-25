using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Model
{
    public class Cherry : GameBoardItem
    {
        private Random _randomNumber;

        public Cherry(double gameBoardWidthPixels, double gameBoardHeightPixels, double snakeXPosition, double snakeYPosition)
        : base(gameBoardWidthPixels, gameBoardHeightPixels)
        {
            _randomNumber = new Random((int)DateTime.Now.Ticks);
            _xPosition = _randomNumber.Next(Constants.MinimumPosition, Constants.MaximumPosition);
            _yPosition = _randomNumber.Next(Constants.MinimumPosition, Constants.MaximumPosition);
            _width = Constants.CherryWidth;
            _height = Constants.CherryHeight;
        }

        public void MoveCherry(Snake theSnake)
        {
            bool cherryMoved = false;
            double xDiff;
            double yDiff;

            while (!cherryMoved)
            {
                // 樱桃生成
                XPosition = _randomNumber.Next(Constants.MinimumPosition, Constants.MaximumPosition);
                YPosition = _randomNumber.Next(Constants.MinimumPosition, Constants.MaximumPosition);
                // 
                xDiff = Math.Abs(_xPosition - theSnake.TheSnakeHead.XPosition);
                yDiff = Math.Abs(_yPosition - theSnake.TheSnakeHead.YPosition);
                if (xDiff > Constants.PlacementBuffer * _width || yDiff > Constants.PlacementBuffer * _height)
                {
                    // 生成的樱桃离蛇头有距离
                    foreach (SnakeBodyPart bodyPart in theSnake.TheSnakeBody)
                    {
                        xDiff = Math.Abs(_xPosition - bodyPart.XPosition);
                        yDiff = Math.Abs(_yPosition - bodyPart.YPosition);
                        if (xDiff > Constants.PlacementBuffer * _width || yDiff > Constants.PlacementBuffer * _height)
                        {
                            cherryMoved = true;
                        }
                        else
                        {
                            cherryMoved = false;
                            break;
                        }
                    }
                }
            }
        }

    }
}
