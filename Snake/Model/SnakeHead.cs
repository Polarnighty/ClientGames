namespace Snake.Model
{
    public class SnakeHead : SnakePart
    {
        private double gameBoardWidthPixels;
        private double gameBoardHeightPixels;
        private double defaultXposition;
        private double defaultYposition;
        private Direction defaultDirection;

        public SnakeHead(double gameBoardWidthPixels, double gameBoardHeightPixels, double defaultXposition, double defaultYposition, Direction defaultDirection)
        {
            this.gameBoardWidthPixels = gameBoardWidthPixels;
            this.gameBoardHeightPixels = gameBoardHeightPixels;
            this.defaultXposition = defaultXposition;
            this.defaultYposition = defaultYposition;
            this.defaultDirection = defaultDirection;
        }

    }
}