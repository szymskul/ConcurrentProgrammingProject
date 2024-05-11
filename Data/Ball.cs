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
                Vector2 _speed = Velocity;
                int sleepTime = (int)(1 / Math.Sqrt(Math.Pow(_speed.X, 2) + Math.Pow(_speed.Y, 2)));
                await Task.Delay(sleepTime);
                stopwatch.Stop();
            }
        }

        private void ChangingPosition(long time)
        {
            Vector2 Move = default;

            Monitor.Enter(_lock);
            try
            {
                Move += Velocity * time;
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

        private void createBall()
        {
            Random random = new Random();
            this.position = new Vector2(random.Next(1, 500), random.Next(1, 500));
            this.Velocity = new Vector2((float)(random.NextDouble() * (0.2 - 0) + 0), (float)(random.NextDouble() * (0.2 - 0) + 0));
        }

        public override object getLock()
        {
            return _lock;
        }
        #endregion
    }
}