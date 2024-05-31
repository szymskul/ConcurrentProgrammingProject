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

            if ((newPosition.X + ball.r >= heightSize && velocity.X > 0) || (newPosition.X < 0 && velocity.X < 0))
            {
                if(Math.Sign(velocity.X) != Math.Sign(heightSize - newPosition.X))
                {
                    ball.Velocity *= new Vector2(-1, 1);
                }
            }
            if ((newPosition.Y + ball.r >= widthSize  && velocity.Y > 0) || (newPosition.Y < 0 && velocity.Y < 0))
            {
                if (Math.Sign(velocity.Y) != Math.Sign(widthSize - newPosition.Y))
                {
                    ball.Velocity *= new Vector2(1,-1);
                }
            }
        }

        public static void collisionProblem(IBall first, IBall second)
        {
                int FirstMass = (int)first.Mass;
                int SecondMass = (int)second.Mass;
                var distanceVector = second.Position - first.Position;
                float minDistance = first.r + second.r;

                if (!(distanceVector.LengthSquared() < minDistance * minDistance)) return;
                var collisionNormal = Vector2.Normalize(distanceVector);

                var relativeVelocity = second.Velocity - first.Velocity;

                var impulseMagnitude = Vector2.Dot(relativeVelocity, collisionNormal);

                if (impulseMagnitude > 0)
                    return;

                var newVelocityFirst = (FirstMass - SecondMass) / (FirstMass + SecondMass) * first.Velocity +
                                   2 * FirstMass / (FirstMass + SecondMass) * second.Velocity;
                var newVelocitySecond = 2 * FirstMass / (FirstMass + SecondMass) * first.Velocity +
                                   (FirstMass - SecondMass) / (FirstMass + SecondMass) * second.Velocity;

                first.Velocity = newVelocityFirst;
                second.Velocity = newVelocitySecond;
           
        }
    }
}