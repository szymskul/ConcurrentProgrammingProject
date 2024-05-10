using Data;
using Logic;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Model
{
    internal class BallModel : IBall
    {
        //private IBall ball;

        ///public BallModel(IBall ball)
        //{
        //this.ball = ball;
        //}

        //public override double Xposition
        //{
        //get { return ball.X; }
        //}

        //public override double Yposition
        //{
        //get { return ball.Y; }
        //}
        public int Diameter { get; }
        public event PropertyChangedEventHandler PropertyChanged;
        private double top;
        private double left;

        public BallModel(double top, double left, int radius)
        {
            Top = top;
            Left = left;
            Diameter = radius * 2;
        }

        public double Top
        {
            get { return top; }
            set
            {
                if (top == value)
                    return;
                top = value;
                RaisePropertyChanged();
            }
        }


        public double Left
        {
            get { return left; }
            set
            {
                if (left == value)
                    return;
                left = value;
                RaisePropertyChanged();
            }
        }

        public void Move_V(Vector2 position)
        {
            Left = position.X;
            Top = position.Y;
        }

        public void Move(double poitionX, double positionY)
        {
            Left = poitionX;
            Top = positionY;
        }


        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public void Move(Vector2 position)
        {
            Left = position.X;
            Top = position.Y;
        }


    }
}
