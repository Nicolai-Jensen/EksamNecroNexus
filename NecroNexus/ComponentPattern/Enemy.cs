using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroNexus
{
    public class Enemy : Component
    {
        //Speed Value, used for velocity in the Move method
        protected float speed;
        protected Board board;
        protected Vector2 velocity;
        protected Vector2 currentPosition;
        protected Vector2 nextPosition;
        public virtual void FindPath(Board board)
        {
            MoveToNextPosition(board, 0);
        }

        private void MoveToNextPosition(Board board, int currentPositionIndex)
        {
            if (currentPositionIndex >= board.PositionList.Count)
                return;

            nextPosition = board.PositionList[currentPositionIndex];
            Vector2 outputVelocity = nextPosition - currentPosition;
            outputVelocity.Normalize();
            velocity = outputVelocity;

            Move(); // Call the Move method to update the position

            // Check if currentPosition has reached or exceeded nextPosition
            if (Vector2.DistanceSquared(currentPosition, nextPosition) <= 1f)
            {
                currentPosition = nextPosition; // Set currentPosition to nextPosition
                MoveToNextPosition(board, currentPositionIndex + 1); // Move to the next position index
            }
        }
        protected void Move()
        {
            if (velocity != Vector2.Zero)
            {
                velocity.Normalize();
            }

            velocity *= speed;
            GameObject.Transform.Translate(velocity * GameWorld.DeltaTime);

            currentPosition = GameObject.Transform.Position;
        }
    }
}
