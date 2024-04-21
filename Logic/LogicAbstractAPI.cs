using Data;
namespace Logic
{
    public abstract class LogicAbstractAPI
    {
        public abstract void createBalls(int count);
        public abstract List<IBall> getAllBalls();
        public abstract void start();
        public static LogicAbstractAPI CreateAPI(DataAbstractAPI data = default(DataAbstractAPI))
        {
            return new LogicLayerAPI(data == null ? DataAbstractAPI.CreateAPI() : data);
        }




    }
    internal class LogicLayerAPI : LogicAbstractAPI
    {
        private DataAbstractAPI data;
        private Table table;
        private Task movingTask;

        public LogicLayerAPI(DataAbstractAPI data)
        {
            this.data = data;
            table = new Table(500, 700);
        }

        public override void createBalls(int count)
        {
            table.AddBalls(count);
        }

        public override List<IBall> getAllBalls()
        {
            return table.balls;
        }

        public override void start()
        {
            if (table.balls.Count > 0)
            {
                movingTask = Task.Run(table.ConstanceMove);
            }
        }
    }
}