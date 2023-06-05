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
        //--------------------------Nicolai Jensen----------------------------//

        //Adds the Ability to access the EnemyType enum down the line
        public EnemyType Type { get; set; }

        //A reference to a spriteRenderer
        protected SpriteRenderer sr;

        //The damage done to the Crypt when the enemies finish their path
        protected int baseDamage;

        //bools for controlling taking damage
        protected bool healthModified;
        protected bool hit;

        //Speed Value, used for velocity in the Move method
        protected float speed;

        //Timers used for certain methods
        protected float timer;
        protected float timerForFeedBack;

        //A Board reference to store a board & path on
        protected Board board;

        //The Enemy Objects Direction, position and next position
        protected Vector2 velocity;
        protected Vector2 currentPosition;
        protected Vector2 nextPosition;
        protected Vector2 position;

        //A list of Vector2s to follow
        protected List<Vector2> pathList = new List<Vector2>();

        //Virtual Resources to inheret 
        public virtual float Health { get; set; }
        public virtual float SoulDrop { get; set; }

        //A List of objects that have recently damaged the unit
        public List<GameObject> damagedList = new List<GameObject>();

        //A Dictionary that should be used to track time on the objects who have damaged the Enemy  (DIDN'T WORK)
        public Dictionary<GameObject, float> addToListDictionary = new Dictionary<GameObject, float>();

        /// <summary>
        /// Calls the MoveToNExtPosition Method
        /// This is what the enemies use for their Path
        /// </summary>
        public virtual void FindPath()
        {
            MoveToNextPosition();
        }

        /// <summary>
        /// This Method Checks if the pathList has positions in its list
        /// Then moves to the next position in the list
        /// </summary>
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
            else //If it has finished its path the enemy gets removed and does damage to the base
            {
                DrawingLevel.UpdateHealth(baseDamage);
                ToRemove = true;
            }
            
        }

        /// <summary>
        /// Standard Method for moving the Object with its Velocity
        /// </summary>
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

        /// <summary>
        /// This Method is used to track if an Enemy has Died or taken damage
        /// </summary>
        public void Death()
        {
            if (Health <= 0) //Object has Died, gets removed and drops its souls
            {
                ToRemove = true;
                DrawingLevel.UpdateSouls(SoulDrop);
            }
            if (healthModified == true) //Object has been hit, initiate hit feedback
            {
                hit = true;
                sr.Color = Color.Red;
                AudioEffect.HitDamageSound();
                healthModified = false;
            }
            if (hit == true) //The Timer for returning the Object to its correct color from red
            {
                timerForFeedBack += GameWorld.DeltaTime;
                if (timerForFeedBack > 0.1f)
                {
                    sr.Color = Color.White;
                    hit = false;
                    timerForFeedBack = 0;
                }
            }

        }

        /// <summary>
        /// A method that clears the List that tracks what has hit the object every Second
        /// </summary>
        public void UpdateDamagedList()
        {
            timer += GameWorld.DeltaTime;
            if (timer >= 1f)
            {
                damagedList.Clear();
                timer = 0;
            }

        }

        /// <summary>
        /// A method that adds an Object to its Damaged List, should be called when colliding with this object
        /// </summary>
        /// <param name="obj">The Object you want to add to the list that prevents damage</param>
        public void AddToList(GameObject obj)
        {
            if (!damagedList.Contains(obj))
            {
                damagedList.Add(obj);
            }
        }

        /// <summary>
        /// Checks if an Object is in the List of objects that have recently damaged this unit
        /// </summary>
        /// <param name="obj">The Object you want to check</param>
        /// <returns></returns>
        public bool IsInDamagedList(GameObject obj)
        {
            return damagedList.Contains(obj);
        }

        /// <summary>
        /// A method for doing Damage to this Unit, this is just the base one
        /// </summary>
        /// <param name="damage">A Damage Variable containing a DamageType and Value</param>
        public virtual void TakeDamage(Damage damage)
        {
            this.Health -= damage.Value;
            healthModified = true;
        }
    }
}
