using Data;
namespace Logic
{
    public abstract class LogicAbstractAPI
    {
        public abstract void createTable(int widght, int height);
        public abstract void createBalls(int count);
        public abstract List<List<double>> getAllBalls();


    }
    internal class LogicLayerrAPI : LogicAbstractAPI
    {
        internal List<BallInterface> ListOfActiveBalls {  get; set; }
        public override void createBalls(int count)
        {
            throw new NotImplementedException();
        }

        public override void createTable(int widght, int height)
        {
            throw new NotImplementedException();
        }

        public override List<List<double>> getAllBalls()
        {
            List<List<double>> ListOfBalls = new List<List<double>>();
            for (int i = 0; i < ListOfActiveBalls.Count; i++) 
            { 
                double X = ListOfActiveBalls[i].X;
                double Y = ListOfActiveBalls[i].Y;

                List<double> BallCoords = new List<double>()
                {
                   X,
                   Y
                };
                ListOfBalls.Add(BallCoords);
            }
            return ListOfBalls;
        }

        
    }
}