using System;


namespace Data
{
    public abstract class DataAbstractAPI : IObserver<IBall>, IObservable<IBall>
    {
        public abstract int getHeightSize();
        public abstract int getWidthSize();
        public abstract void createBalls(int ballsCount);
        public abstract int getBallsAmount();

        public abstract void OnCompleted();
        public abstract void OnError(Exception error);
        public abstract void OnNext(IBall ball);

        public abstract IDisposable Subscribe(IObserver<IBall> observer);

        public static DataAbstractAPI CreateApi()
        {
            return new DataAPI();
        }

        public class BallChaneEventArgs : EventArgs
        {
            public IBall newBall { get; set; }
        }

        private class DataAPI : DataAbstractAPI
        {
            private Table table;
            private IDisposable unsubscriber;
            private IList<IObserver<IBall>> observers;

            public DataAPI()
            {
                this.table = new Table();
                observers = new List<IObserver<IBall>>();
            }

            public override int getHeightSize()
            {
                return table.height;
            }

            public override int getWidthSize()
            {
                return table.width;
            }

            public override int getBallsAmount()
            {
                return table.balls.Count;
            }

            public override void createBalls(int ballsCount)
            {
                table.createBalls(ballsCount);

                foreach(var ball in table.balls)
                {
                    Subscribe(ball);
                    ball.startMovingTask();
                }
            }

            #region observer

            public virtual void Subscribe(IObservable<IBall> provider)
            {
                if (provider != null)
                    unsubscriber = provider.Subscribe(this);
            }

            public override void OnCompleted()
            {
                Unsubscribe();
            }

            public override void OnError(Exception error)
            {
                throw error;
            }

            public override void OnNext(IBall ball)
            {
                foreach(var observer in observers)
                {
                    observer.OnNext(ball);
                }
            }

            public virtual void Unsubscribe()
            {
                unsubscriber.Dispose();
            }

            #endregion

            #region provider

            public override IDisposable Subscribe(IObserver<IBall> observer)
            {
                if (!observers.Contains(observer))
                    observers.Add(observer);
                return new Unsubscriber(observers, observer);
            }

            private class Unsubscriber : IDisposable
            {
                private IList<IObserver<IBall>> _observers;
                private IObserver<IBall> _observer;

                public Unsubscriber(IList<IObserver<IBall>> observers, IObserver<IBall> observer)
                {
                    _observer = observer;
                    _observers = observers;
                }

                public void Dispose()
                {
                    if (_observer != null && _observers.Contains(_observer))
                        _observers.Remove(_observer);
                }
            }
            #endregion
        }
    }
}
