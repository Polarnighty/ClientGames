using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Model
{
    public class SnakeEye: SnakePart
    {
        public SnakeEye(double gameBoardWidthPixels, double gameBoardHeightPixels, double initialXPosition, double initialYPosition, Direction direction)
    : base(gameBoardWidthPixels, gameBoardHeightPixels, direction)
        {
            _xPosition = initialXPosition;
            _yPosition = initialYPosition - Constants.EyeOffet;
            _width = Constants.EyeWidth;
            _height = Constants.EyeHeight;
        }

    }

}
