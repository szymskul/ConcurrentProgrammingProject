using Data;
using Logic;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Model
{
    internal class BallModel : IBall
    {

        private double positionX;
        private double positionY;
        public int Diameter { get; }
        public event PropertyChangedEventHandler PropertyChanged;

        public BallModel(double X, double Y, int radius)
        {
            PositionX = X;
            PositionY = Y;
            Diameter = radius * 2;
        }

        public double PositionX
        {
            get { return positionX; }
            set
            {
                if (positionX == value)
                    return;
                positionX = value;
                RaisePropertyChanged();
            }
        }


        public double PositionY
        {
            get { return positionY; }
            set
            {
                if (positionY == value)
                    return;
                positionY = value;
                RaisePropertyChanged();
            }
        }
        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Move(Vector2 position)
        {
            PositionX = position.X;
            PositionY = position.Y;
        }


    }
}
