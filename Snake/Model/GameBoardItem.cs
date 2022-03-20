using Microsoft.VisualBasic;
using Snake.Common;

namespace Snake.Model
{
    public abstract class GameBoardItem : NotificationBase
    {
        protected double _gameBoardWidthPixels;
        protected double _gameBoardHeightPixels;
        protected double _xPosition;
        protected double _yPosition;
        protected double _width;
        protected double _height;

        public GameBoardItem()
        {
        }

        public GameBoardItem(double gameBoardWidthPixels, double gameBoardHeightPixels)
        {
            _gameBoardWidthPixels = gameBoardWidthPixels;
            _gameBoardHeightPixels = gameBoardHeightPixels;
        }

                        
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
                RaisePropertyChanged(nameof(Width));
                RaisePropertyChanged(nameof(XPosition));
                RaisePropertyChanged(nameof(XPositionPixels));
                RaisePropertyChanged(nameof(XPositionPixelsScreen));
            }
        }
                        
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
                RaisePropertyChanged(nameof(Height));
                RaisePropertyChanged(nameof(YPosition));
                RaisePropertyChanged(nameof(YPositionPixels));
                RaisePropertyChanged(nameof(YPositionPixelsScreen));
            }
        }
                        
        public double XPosition
        {
            get
            {
                return _xPosition;
            }
            protected set
            {
                _xPosition = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(XPosition));
                RaisePropertyChanged(nameof(XPositionPixels));
                RaisePropertyChanged(nameof(XPositionPixelsScreen));
            }
        }
                        
        public double YPosition
        {
            get
            {
                return _yPosition;
            }
            protected set
            {
                _yPosition = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(YPosition));
                RaisePropertyChanged(nameof(YPositionPixels));
                RaisePropertyChanged(nameof(YPositionPixelsScreen));
            }
        }
                        
        public double XPositionPixels
        {
            get
            {
                return (_xPosition / Constants.GameBoardWidthScale) * _gameBoardWidthPixels;
            }
        }
                        
        public double YPositionPixels
        {
            get
            {
                return (_yPosition / Constants.GameBoardHeightScale) * _gameBoardHeightPixels;
            }
        }
                        
        public double XPositionPixelsScreen
        {
            get
            {
                return ((_xPosition - (_width / 2.0)) / Constants.GameBoardWidthScale) * _gameBoardWidthPixels;
            }
        }
                        
        public double YPositionPixelsScreen
        {
            get
            {
                return ((_yPosition - (_height / 2.0)) / Constants.GameBoardHeightScale) * _gameBoardHeightPixels;
            }
        }
                        
        public double Width
        {
            get
            {
                return _width;
            }
        }
                        
        public double Height
        {
            get
            {
                return _height;
            }
        }
                        
        public double WidthPixels
        {
            get
            {
                return (_width / Constants.GameBoardWidthScale) * _gameBoardWidthPixels;
            }
        }
                        
        public double HeightPixels
        {
            get
            {
                return (_height / Constants.GameBoardHeightScale) * _gameBoardHeightPixels;
            }
        }

    }
}