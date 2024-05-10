using System;
using Logic;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Reactive.Linq;
using System.Reactive;

namespace Model
{
    public interface IBall : INotifyPropertyChanged
    {
        double Top { get; }
        double Left { get; }
        int Diameter { get; }
    }

    public class BallChaneEventArgs : EventArgs
    {
        public IBall Ball { get; set; }
    }

    public abstract class ModelAbstractApi : IObservable<IBall>
    {

        public abstract void GenerateBalls(int counts);

        public static ModelAbstractApi CreateApi()
        {
            return new ModelLayerApi();
        }
        #region IObservable

        public abstract IDisposable Subscribe(IObserver<IBall> observer);
        #endregion IObservable

    }

    internal class ModelLayerApi : ModelAbstractApi
    {
        private LogicAbstractAPI logicApi;
        public event EventHandler<BallChaneEventArgs> BallChanged;
        private IObservable<EventPattern<BallChaneEventArgs>> eventObservable = null;
        private List<BallModel> Balls = new List<BallModel>();

        public ModelLayerApi()
        {
            logicApi = logicApi ?? LogicAbstractAPI.CreateAPI();
            IDisposable observer = logicApi.Subscribe(x => Balls[x.Id - 1].Move(x.Position));
            eventObservable = Observable.FromEventPattern<BallChaneEventArgs>(this, "BallChanged");
        }

        public override void GenerateBalls(int counts)
        {
            //logicApi.createBalls(counts);
            //logicApi.start();
            for (int i = 0; i < counts; i++)
            {
                BallModel newBall = new BallModel(0, 0, 15);
                Balls.Add(newBall);
                BallChanged?.Invoke(this, new BallChaneEventArgs() { Ball = newBall });
            }
            logicApi.AddStart(counts);

        }

        public override IDisposable Subscribe(IObserver<IBall> observer)
        {
            return eventObservable.Subscribe(x => observer.OnNext(x.EventArgs.Ball), ex => observer.OnError(ex), () => observer.OnCompleted());
        }
    }
}
