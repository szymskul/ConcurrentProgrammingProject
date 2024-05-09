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
        public static bool IsCollision(IBall current, IBall other)
        {
            Vector2 currentDelta = current.Position - other.Position;
            float distance = currentDelta.Length();

            if(Math.Abs(distance) <= current.r + other.r)
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

        public static void collisionProblem(IBall current, IBall other)
        {
            Vector2 currentVelocity = current.Velocity;
            Vector2 currentPosition = current.Position;
            Vector2 otherVelocity = other.Velocity;
            Vector2 otherPosition = other.Position;

            Vector2 deltaPosition = currentPosition - otherPosition;
            float distance = deltaPosition.Length();

            Vector2 normal = Vector2.Normalize(deltaPosition);
            Vector2 tangent = new Vector2(-normal.Y, normal.X);

            float dpTan1 = Vector2.Dot(currentVelocity, tangent);
            float dpTan2 = Vector2.Dot(otherVelocity, tangent);

            float dpNorm1 = Vector2.Dot(currentVelocity, normal);
            float dpNorm2 = Vector2.Dot(otherVelocity, normal);

            float m1 = (2.0f * 10 * dpNorm2) / (20);
            float m2 = (2.0f * 10 * dpNorm1) / (20);

            Vector2 currentNewVelocity = tangent * dpTan1 + normal * m1;
            Vector2 otherNewVelocity = tangent * dpTan2 + normal * m2;

            current.Velocity = currentNewVelocity;
            other.Velocity = otherNewVelocity;
        }
    }
}