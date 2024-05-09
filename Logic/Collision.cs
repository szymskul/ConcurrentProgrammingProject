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

        public static void changingPosition(IBall ball, int height, int width)
        {
            Vector2 newPosition = ball.Position;

            if (newPosition.X + ball.r > width || newPosition.X < 0)
            {
                ball.Velocity *= new Vector2(-1, 1);
            }
            if (newPosition.Y + ball.r > height || newPosition.Y < 0)
            {
                ball.Velocity *= new Vector2(1, -1);
            }
        }

        public static void collisionProblem(IBall first, IBall second)
        {
            Vector2 FirstVelocity = first.Velocity;
            Vector2 FirstPosition = first.Position;
            float FirstMass = (float)first.Mass;
            Vector2 SecondVelocity = second.Velocity;
            Vector2 SecondPosition = second.Position;
            float SecondMass = (float)second.Mass;

            Vector2 deltaPosition = FirstPosition - SecondPosition;
            float distance = deltaPosition.Length();

            Vector2 normalization = Vector2.Normalize(deltaPosition);
            Vector2 tangent = new Vector2(-normalization.Y, normalization.X);

            float dpTan1 = Vector2.Dot(FirstVelocity, tangent);
            float dpTan2 = Vector2.Dot(SecondVelocity, tangent);

            float dpNorm1 = Vector2.Dot(FirstVelocity, normalization);
            float dpNorm2 = Vector2.Dot(SecondVelocity, normalization);

            float p1 = (dpNorm1 * (FirstMass - SecondMass) + 2.0f * SecondMass * dpNorm2) / (FirstMass + SecondMass);
            float p2 = (dpNorm2 * (SecondMass - FirstMass) + 2.0f * FirstMass * dpNorm1) / (FirstMass + SecondMass);

            Vector2 FirstNewVelocity = tangent * dpTan1 + normalization * p1;
            Vector2 SecondNewVelocity = tangent * dpTan2 + normalization * p2;

            first.Velocity = FirstNewVelocity;
            second.Velocity = SecondNewVelocity;
        }
    }
}