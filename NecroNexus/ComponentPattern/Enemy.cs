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

        public EnemyType Type { get; set; }

        //Speed Value, used for velocity in the Move method
        protected float speed;
        protected Board board;
        protected Vector2 velocity;
        protected Vector2 currentPosition;
        protected Vector2 nextPosition;
        protected Vector2 position;
        protected List<Vector2> pathList = new List<Vector2>();

        protected float health;
        protected float soulDrop;

        public virtual void FindPath()
        {
            MoveToNextPosition();
        }

        private void MoveToNextPosition()
        {

            if (pathList.Count > 0)
            {
                nextPosition = pathList[0];

                // Check if currentPosition has reached or exceeded nextPosition
                if (Vector2.DistanceSquared(currentPosition, nextPosition) <= 10f)
                {
                    currentPosition = nextPosition; // Set currentPosition to nextPosition
                    pathList.Remove(pathList[0]);
                }
                Vector2 outputVelocity = nextPosition - currentPosition;
                outputVelocity.Normalize();
                velocity = outputVelocity;
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
