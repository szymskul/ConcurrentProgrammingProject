using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Text;

namespace Data
{
    internal class Ball : IBall
    {
        public override int Id { get; }
        public override int r { get; } = 10;
        public override double Mass { get; } = 10;

        private bool isRunning = true;
        private Vector2 position;
        public override Vector2 Position { get => position; }
        public override Vector2 Velocity { get; set; }
        private static object _lock = new object();
        internal readonly IList<IObserver<IBall>> observers;
        Stopwatch stopwatch;
        private Task movingTask;
        Random random = new Random();

        internal Ball(int id)
        {
            this.Id = id;
            stopwatch = new Stopwatch();
            observers = new List<IObserver<IBall>>();
            createBall();
        }

        public void startMovingTask()
        {
            this.movingTask = new Task(MovingBall);
            movingTask.Start();
        }

        private async void MovingBall()
        {
            while (isRunning)
            {
                long time = stopwatch.ElapsedMilliseconds;
                stopwatch.Restart();
                stopwatch.Start();
                ChangingPosition(time);
                stopwatch.Stop();
            }
        }

        private void ChangingPosition(long time)
        {
            Vector2 Move = default;

            Monitor.Enter(_lock);
            try
            {
                if(time > 0)
                {
                    Move += Velocity * time;
                }
                else
                {
                    Move = Velocity;
                }
                position += Move;
            }
            catch (SynchronizationLockException exception)
            {
                throw new Exception("Synchronization lock not working", exception);
            }
            finally
            {
                Monitor.Exit(_lock);
            }

            foreach (var observer in observers.ToList())
            {
                if(observer != null)
                {
                    observer.OnNext(this);
                }
            }
        }

        #region provider

        private class Unsubscriber : IDisposable
        {
            private IList<IObserver<IBall>> _observers;
            private IObserver<IBall> _observer;

            public Unsubscriber(IList<IObserver<IBall>> observers, IObserver<IBall> observer)
            {
                _observers = observers;
                _observer = observer;
            }

            public void Dispose()
            {
                if(_observer != null && _observers.Contains(_observer))
                    _observers.Remove(_observer);
            }
        }

        public override IDisposable Subscribe(IObserver<IBall> observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);
            return new Unsubscriber(observers, observer);
        }

        private double getRandomNumber(double min, double max)
        {
            if (min > 0 && max > 0)
                return random.NextDouble() * (max - min) + min;
            else
                return 1;
        }

        private void createBall()
        {
            this.position = new Vector2((float)getRandomNumber(21.0, 679.0), (float)getRandomNumber(21.0, 479.0));
            this.Velocity = new Vector2((float)getRandomNumber(1, 3), (float)getRandomNumber(1, 3));
        }

        public override object getLock()
        {
            return _lock;
        }
        #endregion
    }
}