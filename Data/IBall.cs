using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public abstract class IBall : IObservable<IBall>
    {
        public abstract int Id { get; }
        public abstract int r { get; }
        public abstract double Mass { get; }
        public abstract Vector2 Position { get; }
        public abstract Vector2 Velocity { get; set; }
        public abstract object getLock();
        public abstract IDisposable Subscribe(IObserver<IBall> observer);
    }
}
