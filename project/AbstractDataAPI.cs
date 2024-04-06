namespace Data
{
    public abstract class AbstractDataAPI
    {
        public static AbstractDataAPI createAPI()
        {
            return new DataLayerAPI();
        }

        public abstract void createTable(int heightOfTheBoard, int widthOfTheBoard);
        public abstract BallInterface createBall();
        public abstract int GetTableWidth();
        public abstract int GetTableHeight();

        private class DataLayerAPI : AbstractDataAPI
        {
            internal TableInterface? table { set; get; }
            internal Random randomNumber = new Random();
            public override void createTable(int heightOfTheBoard, int widthOfTheBoard)
            {
                this.table = TableInterface.createBoard(heightOfTheBoard, widthOfTheBoard);
            }
            public override BallInterface createBall()
            {
                double x = randomNumberInitilizar(0,GetTableWidth() + 1);
                double y = randomNumberInitilizar(0,GetTableHeight() + 1);
                BallInterface ball = BallInterface.CreateBall(x, y, 0, 0, false);
                return ball;
            }
            public double randomNumberInitilizar(int min, int max)
            {
                return randomNumber.Next(min, max + 1);
            }
            public override int GetTableWidth()
            {
                if (this.table == null)
                    return 0;
                return this.table.widht;
            }
            public override int GetTableHeight()
            {
                if (this.table == null)
                    return 0;
                return this.table.height;
            }
        }
    }
}

