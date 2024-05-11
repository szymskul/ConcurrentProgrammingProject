using Data;
using System;
using System.Reactive;
using System.Reactive.Linq;
namespace Logic
{
    public abstract class LogicAbstractAPI : IObserver<IBall>, IObservable<IBall>
    {
        public abstract void AddStart(int ballsCount);
        public abstract void OnCompleted();
        public abstract void OnError(Exception error);
        public abstract void OnNext(IBall ball);

        public abstract IDisposable Subscribe(IObserver<IBall> observer);
        public static LogicAbstractAPI CreateAPI(DataAbstractAPI data = default(DataAbstractAPI))
        {
            return new LogicLayerAPI(data == null ? DataAbstractAPI.CreateApi() : data);
        }

        public class BallChaneEventArgs : EventArgs
        {
            public IBall ball { get; set; }
        }

        private class LogicLayerAPI : LogicAbstractAPI,IObservable<IBall>
        {
            private readonly DataAbstractAPI dataAPI;
            private IDisposable unsubscriber;

            private IObservable<EventPattern<BallChaneEventArgs>> eventObservable = null;
            public event EventHandler<BallChaneEventArgs> BallChanged;
            Dictionary<int, IBall> ballTree;
            Barrier barrier;

            public LogicLayerAPI(DataAbstractAPI dataAPI)
            {
                eventObservable = Observable.FromEventPattern<BallChaneEventArgs>(this, "BallChanged");
                this.dataAPI = dataAPI;
                Subscribe(dataAPI);
                ballTree = new Dictionary<int, IBall>();
            }

            public override void AddStart(int ballsCount)
            {
                dataAPI.createBalls(ballsCount);
                barrier = new Barrier(ballsCount);
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
                Monitor.Enter(ball.getLock());
                try
                {
                    if(!ballTree.ContainsKey(ball.Id))
                    {
                        ballTree.Add(ball.Id, ball);
                    }

                    foreach(var item in ballTree)
                    {
                        if(item.Key != ball.Id)
                        {
                            if (Collision.IsCollision(ball, item.Value))
                            {
                                Collision.collisionProblem(ball, item.Value);
                            }
                        }
                    }
                    Collision.changingPosition(ball, dataAPI.getHeightSize(), dataAPI.getWidthSize());
                    BallChanged?.Invoke(this, new BallChaneEventArgs() { ball = ball });
                }
                catch(SynchronizationLockException exception)
                {
                    throw new Exception("Checking collision synchronization lock not working", exception);
                }
                finally
                {
                    Monitor.Exit(ball.getLock());
                }
            }

            public virtual void Unsubscribe()
            {
                unsubscriber.Dispose();
            }

            #endregion

            #region observable

            public override IDisposable Subscribe(IObserver<IBall> observer)
            {
                return eventObservable.Subscribe(x => observer.OnNext(x.EventArgs.ball), ex => observer.OnError(ex), () => observer.OnCompleted());
            }

            #endregion

        }
    }
}