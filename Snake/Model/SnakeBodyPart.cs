using System;
using System.Linq;

namespace Snake.Model
{
    public class SnakeBodyPart : SnakePart
    {
        public SnakeBodyPart(SnakeBodyPart snakeBodyPart)
        {
            _gameBoardWidthPixels = snakeBodyPart.GameBoardWidthPixels;
            _gameBoardHeightPixels = snakeBodyPart.GameBoardHeightPixels;
            _xPosition = snakeBodyPart.XPosition;
            _yPosition = snakeBodyPart.YPosition;
            _width = snakeBodyPart.WidthPixels;
            _height = snakeBodyPart.HeightPixels;
            _directionOfTravel = snakeBodyPart.DirectionOfTravel;
        }
        public SnakeBodyPart(double gameBoardWidthPixels, double gameBoardHeightPixels, Snake theSnake)
        {
            // Set the gameboard width/height.
            _gameBoardWidthPixels = gameBoardWidthPixels;
            _gameBoardHeightPixels = gameBoardHeightPixels;

            // Set the body width/height.
            _width = Constants.BodyWidth;
            _height = Constants.BodyHeight;

            // Get the current last SnakePart.
            SnakePart currentLastSnakePart;
            try
            {
                currentLastSnakePart = theSnake.TheSnakeBody.Last();
            }
            catch
            {
                // The snake body is empty - use the snake head as the last body part.
                currentLastSnakePart = theSnake.TheSnakeHead;
            }

            // Attempt to find a valid location at the end of the snake.
            if (currentLastSnakePart.DirectionOfTravel == Direction.Up)
            {
                _xPosition = currentLastSnakePart.XPosition;
                _yPosition = currentLastSnakePart.YPosition + _height;
                _directionOfTravel = Direction.Up;

                if (CheckLocation(theSnake))
                {
                    // Location is valid.
                    return;
                }
            }
            else if (currentLastSnakePart.DirectionOfTravel == Direction.Right)
            {
                _xPosition = currentLastSnakePart.XPosition - _width;
                _yPosition = currentLastSnakePart.YPosition;
                _directionOfTravel = Direction.Right;

                if (CheckLocation(theSnake))
                {
                    // Location is valid.
                    return;
                }
            }
            else if (currentLastSnakePart.DirectionOfTravel == Direction.Down)
            {
                _xPosition = currentLastSnakePart.XPosition;
                _yPosition = currentLastSnakePart.YPosition - _height;
                _directionOfTravel = Direction.Down;

                if (CheckLocation(theSnake))
                {
                    // Location is valid.
                    return;
                }
            }
            else if (currentLastSnakePart.DirectionOfTravel == Direction.Left)
            {
                _xPosition = currentLastSnakePart.XPosition + _width;
                _yPosition = currentLastSnakePart.YPosition;
                _directionOfTravel = Direction.Left;

                if (CheckLocation(theSnake))
                {
                    // Location is valid.
                    return;
                }
            }
            else
            {
                throw new Exception("SnakeBodyPart(double gameBoardWidthPixels, double gameBoardHeightPixels, Snake theSnake): Unable to find valid location to grow snake.");
            }
        }

        /// <summary>
        /// 判断蛇身是否有效
        /// </summary>
        /// <param name="theSnake"></param>
        /// <returns></returns>
        private bool CheckLocation(Snake theSnake)
        {
            // 检查位置是否与蛇的头部不同
            if (_xPosition == theSnake.TheSnakeHead.XPosition && _yPosition == theSnake.TheSnakeHead.YPosition)
            {
                // 位置与蛇的头部不一样
            }
            else
            {
                return false;
            }

            // 检查位置是否与蛇的身体不同
            foreach (SnakeBodyPart bodyPart in theSnake.TheSnakeBody)
            {
                if (_xPosition == bodyPart.XPosition && _yPosition == bodyPart.YPosition)
                {
                    // 什么都不做
                }
                else
                {
                    return false;
                }
            }

            // 检测位置是否越界
            if (_xPosition - (_width / 2.0) < 0)
            {
                return false;
            }
            else if (_xPosition + (_width / 2.0) > Constants.GameBoardWidthScale)
            {
                return false;
            }
            else if (_yPosition - (_height / 2.0) < 0)
            {
                return false;
            }
            else if (_yPosition + (_height / 2.0) > Constants.GameBoardHeightScale)
            {
                return false;
            }

            return true;
        }



    }
}