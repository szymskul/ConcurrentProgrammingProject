using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Data;
using System.Numerics;

namespace Logic
{
    internal class Collision
    {
        public static bool IsCollision(IBall first, IBall second)
        {
            Vector2 currentDelta = first.Position - second.Position;
            float distance = currentDelta.Length();

            if(Math.Abs(distance) <= first.r + second.r)
            {
                return true;
            }

            return false;
        }

        public static void changingPosition(IBall ball, int heightSize, int widthSize)
        {
            Vector2 newPosition = ball.Position;
            Vector2 velocity = ball.Velocity;

            if ((newPosition.X > heightSize - ball.r && velocity.X > 0) || (newPosition.X < 0 && velocity.X < 0))
            {
                if(Math.Sign(velocity.X) != Math.Sign(heightSize - newPosition.X))
                {
                    ball.Velocity *= new Vector2(-1, 1);
                }
            }
            if ((newPosition.Y > widthSize - ball.r && velocity.Y > 0) || (newPosition.Y < 0 && velocity.Y < 0))
            {
                if (Math.Sign(velocity.Y) != Math.Sign(widthSize - newPosition.Y))
                {
                    ball.Velocity *= new Vector2(1,-1);
                }
            }
        }

        public static void collisionProblem(IBall first, IBall second)
        {
            float FirstMass = (float)first.Mass;
            float SecondMass = (float)second.Mass;
            float x1 = first.Velocity.X;
            float y1 = first.Velocity.Y;
            float x2 = second.Velocity.X;
            float y2 = second.Velocity.Y;


            float newX1 = (x1 * (FirstMass - SecondMass) + 2.0f * SecondMass * x2) / (FirstMass + SecondMass);
            float newY1 = (y1 * (FirstMass - SecondMass) + 2.0f * SecondMass * y2) / (FirstMass + SecondMass);

            float newX2 = (x2 * (SecondMass - FirstMass) + 2.0f * FirstMass * x1) / (FirstMass + SecondMass);
            float newY2 = (y2 * (SecondMass - FirstMass) + 2.0f * FirstMass * y1) / (FirstMass + SecondMass);

            Vector2 FirstNewVelocity = new Vector2(newX1, newY1);
            Vector2 SecondNewVelocity = new Vector2(newX2, newY2);

            first.Velocity = FirstNewVelocity;
            second.Velocity = SecondNewVelocity;
        }
    }
}