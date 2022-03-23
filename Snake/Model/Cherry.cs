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

    }
}
