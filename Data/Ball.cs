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
        private Vector2 velocity;
        private readonly object lock_pos = new object();
        private readonly object lock_vel = new object();
        internal readonly IList<IObserver<IBall>> observers;
        Stopwatch stopwatch;
        private Task movingTask;
        private DAO dao;

        public override Vector2 Velocity
        {
            get
            {
                lock (lock_vel)
                {
                    return velocity;
                }
            }
            set
            {
                lock (lock_vel)
                {
                    velocity = value;
                }
            }
        }

        public override Vector2 Position
        {
            get
            {
                lock (lock_pos)
                {
                    return position;
                }
            }
        }

        internal Ball(int id)
        {
            this.Id = id;
            dao = DAO.CreateInstance();
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
            stopwatch.Start();
            float prev_time = 0.0f;
            while (isRunning)
            {
                float c_time = stopwatch.ElapsedMilliseconds;
                float delta_time = c_time - prev_time;
                const float timeOfTravel = 1f / 60f;

                if(delta_time >= timeOfTravel) 
                {
                    lock (lock_pos) 
                    {
                        position += velocity * delta_time;
                    }

                    foreach (var observer in observers.ToList())
                    {
                        if (observer != null)
                        {
                            observer.OnNext(this);
                        }
                    }
                    prev_time = c_time;
                    dao.Add(this, DateTime.Now);
                }

                await Task.Delay(TimeSpan.FromSeconds(timeOfTravel));

            }
            stopwatch.Stop();
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
            this.velocity = new Vector2((float)(random.NextDouble() * (0.2 - 0) + 0), (float)(random.NextDouble() * (0.2 - 0) + 0));
        }

       
        #endregion
    }
}