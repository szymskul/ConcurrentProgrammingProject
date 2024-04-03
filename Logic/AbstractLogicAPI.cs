

namespace Logic
{
    public abstract class LogicAbstractAPI
    {
        public abstract void createTable(int widght, int height);
        public abstract void createBalls(int count);
        public abstract List<Ball> getAllBalls();


    }
    internal class LogicLayerrAPI : LogicAbstractAPI
    {
        public override void createBalls(int count)
        {
            throw new NotImplementedException();
        }

        public override void createTable(int widght, int height)
        {
            throw new NotImplementedException();
        }

        public override List<Ball> getAllBalls()
        {
            throw new NotImplementedException();
        }
    }
}