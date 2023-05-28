using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

        protected SpriteRenderer sr;
        protected int baseDamage;
        //Speed Value, used for velocity in the Move method
        protected float speed;
        protected float timer;
        protected Board board;
        protected Vector2 velocity;
        protected Vector2 currentPosition;
        protected Vector2 nextPosition;
        protected Vector2 position;
        protected List<Vector2> pathList = new List<Vector2>();

        public virtual float Health { get; set; }
        public virtual float SoulDrop { get; set; }

        public List<GameObject> damagedList = new List<GameObject>();
        public Dictionary<GameObject, float> addToListDictionary = new Dictionary<GameObject, float>();


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
            else
            {
                LevelOne.UpdateHealth(1);
                ToRemove = true;
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

            //Checks the direction the Object is moving and flips the sprite correctly
            if (velocity.X > 0)
            {
                sr.SpriteEffects = SpriteEffects.None;

            }
            else if (velocity.X < 0)
            {
                sr.SpriteEffects = SpriteEffects.FlipHorizontally;

            }
        }


        public void Death()
        {
            if (Health <= 0)
            {
                ToRemove = true;
                LevelOne.UpdateSouls(SoulDrop);
            }
        }

        public void UpdateDamagedList()
        {
            timer += GameWorld.DeltaTime;
            if (timer >= 1f)
            {
                damagedList.Clear();
                timer = 0;
            }

        }

        public void AddToList(GameObject obj)
        {
            if (!damagedList.Contains(obj))
            {
                damagedList.Add(obj);
            }
        }

        public bool IsInDamagedList(GameObject obj)
        {
            return damagedList.Contains(obj);
        }

        public virtual void TakeDamage(Damage damage)
        {
            this.Health -= damage.Value;
        }
    }
}
