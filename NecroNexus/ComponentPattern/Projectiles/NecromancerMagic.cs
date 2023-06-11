using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace NecroNexus
{
    //--------------------------Nicolai Jensen----------------------------//

    /// <summary>
    /// This class controls the functionality of the NecromancerMagic
    /// </summary>
    public class NecromancerMagic : Component, IGameListener
    {
        //Speed Value, used for velocity in the Move method
        private float speed;
        
        //A Factory of itself so it can spawn itself
        private NecroMagicFactory magicFac = new NecroMagicFactory();

        //A Damage and SpriteRenderer attachment
        private Damage damage;
        private SpriteRenderer sr;

        //The Objects Moving Direction and its startPosition
        private Vector2 velocity;
        private Vector2 startPosition;

        //Which Tier the Magic is
        private int tier;

        //The Timers used by the Magic
        private float splitTimer;
        private float homeTimer;

        //A ton of bools that control the functionality of the Object
        private bool homing = false;
        private bool split = false;
        private bool explode = false;
        private bool hasSplit = false;
        private bool hasExploded = false;
        private bool willHome = false;

        //Indicates if the Object should be removed or not
        public override bool ToRemove { get; set; }

        /// <summary>
        /// Standard Constructor that takes an int value and spawns magic with it, the Necromancer uses this one
        /// </summary>
        /// <param name="tier">the Tier of the Magic</param>
        public NecromancerMagic(int tier)
        {
            this.tier = tier;

            //Makes the Direction the Magic is flying from the Necromancer towards the mouse
            velocity = DirectionToMouse(Globals.ReturnPlayerPosition());

            //Applies a Tier
            switch (this.tier)
            {
                case (0):
                    TierZero();
                    break;
                case (1):
                    Tier1();
                    break;
                case (2):
                    Tier2();
                    break;
                case (3):
                    Tier3();
                    break;
            }
        }

        /// <summary>
        /// This Constructor is for the 2 smaller fireballs that are spawned by the big fireball
        /// </summary>
        /// <param name="tier">The Tier of the Magic</param>
        /// <param name="isSplit">A bool to indicate that the Magic is post split</param>
        /// <param name="velocity">The direction of the new fireball</param>
        /// <param name="startPosition">The new Fireballs StartPosition</param>
        public NecromancerMagic(int tier, bool isSplit, Vector2 velocity, Vector2 startPosition)
        {
            this.tier = tier;
            hasSplit = isSplit;
            this.velocity = velocity;
            this.startPosition = startPosition;


            switch (this.tier)
            {
                case (2):
                    Tier2();
                    willHome = true;
                    break;
                case (3):
                    Tier3();
                    willHome = true;
                    break;
            }
            speed = 800f;
            split = false; //Split is set to false here as when applying tiers above it is set to true and becomes an infinite loop
        }

        /// <summary>
        /// The 3rd Consturctor for NecromancerMagic, this one is used to spawn the Explosion when the final tier small Fireballs hit an opponent
        /// </summary>
        /// <param name="velocity">Sets the Velocity of the fireball</param>
        /// <param name="startPosition">Sets the StartPosition of the fireball</param>
        public NecromancerMagic(Vector2 velocity, Vector2 startPosition)
        {
            hasExploded = true;
            this.velocity = velocity;
            this.startPosition = startPosition;
            AudioEffect.PlayExplosion2();
            damage = new Damage(DamageType.Magical, 2f);
        }

        public override void Start()
        {
            GameObject.Tag = "NecroMagic";
            sr = GameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;
            if (hasSplit == false && hasExploded == false) //If it is a standard fireball made from the Necromancer apply the starting position as the Necromancers position
            {
                GameObject.Transform.Position = new Vector2(Globals.ReturnPlayerPosition().X, Globals.ReturnPlayerPosition().Y);
            }
            else if (hasSplit == true || hasExploded == true) //If it is a uniquely spawned fireball set the startPosition gotten from the constructor it used
            {
                GameObject.Transform.Translate(startPosition);
            }
            
        }

        public override void Update()
        {
            Move();
            SplitCheck();
            Homing();
            HomeCheck();

            if (sr.Sprite == Globals.Content.Load<Texture2D>("Necromancer/Magic/Explosion/1_23")) //Removes the explosion on a certain frame of its animation
            {
                ToRemove = true;
            }

        }


        public void Move()
        {
            if (velocity != Vector2.Zero)
            {
                velocity.Normalize();
            }

            velocity *= speed;
            GameObject.Transform.Translate(velocity * GameWorld.DeltaTime);     
        }


        /// <summary>
        /// This returns a Vector2 that acts as the Direction towards the mouse
        /// </summary>
        /// <param name="playerPosition"></param>
        /// <returns></returns>
        protected Vector2 DirectionToMouse(Vector2 playerPosition)
        {
            Vector2 direction;
            
            MouseState mouseState = Mouse.GetState();
            Vector2 mousePosition = mouseState.Position.ToVector2();

            //We get the correct direction by Subtracting the player Position with the Mouse Point, same goes for Rotation
            direction = mousePosition - playerPosition;
            direction.Normalize();

            return direction;
        }


        //METHODS FOR APPLYING VALUES TO THE DIFFERENT TIERS

        public void TierZero()
        {
            speed = 500f;
            damage = new Damage(DamageType.Magical, 1f);
        }

        public void Tier1()
        {
            speed = 700f;
            damage = new Damage(DamageType.Magical, 1.5f);
            willHome = true;
        }

        public void Tier2()
        {
            speed = 250f;
            damage = new Damage(DamageType.Magical, 1f);
            split = true;
        }

        public void Tier3()
        {
            speed = 250f;
            damage = new Damage(DamageType.Magical, 1.5f);
            split = true;
            explode = true;
        }


        /// <summary>
        /// This Method alculates using Math.Atan2 and MathHelper the Angles and direction the 2 small fireballs fire off as
        /// It then uses a MagicFactory to spawn the smaller fireballs before applying that direction to them
        /// </summary>
        public void Split()
        {
            NecromancerMagic ml;
            NecromancerMagic mr;

            //Calculate the angle of the original velocity vector
            float originalAngle = (float)Math.Atan2(velocity.Y, velocity.X);

            //Define the deviation angle for the split bullets
            float deviationAngle = MathHelper.ToRadians(10f); // Adjust this angle as desired

            //Calculate the left and right angles based on the original angle and deviation angle
            float leftAngle = originalAngle + deviationAngle;
            float rightAngle = originalAngle - deviationAngle;

            //Convert the angles back to velocity vectors
            Vector2 leftVelocity = Globals.FromAngle(leftAngle) * velocity.Length();
            Vector2 rightVelocity = Globals.FromAngle(rightAngle) * velocity.Length();

            //Call the Factory to Create the smaller fireballs
            GameObject bulletLeft = magicFac.CreateOffSpring(GameObject.Transform.Position);
            GameObject bulletRight = magicFac.CreateOffSpring(GameObject.Transform.Position);

            //Apply a NecromancerMagic Component with the second Constructor and applies a collider
            ml = (NecromancerMagic)bulletLeft.AddComponent(new NecromancerMagic(tier, true, leftVelocity, GameObject.Transform.Position));
            mr = (NecromancerMagic)bulletRight.AddComponent(new NecromancerMagic(tier, true, rightVelocity, GameObject.Transform.Position));
            Collider cl = (Collider)bulletLeft.GetComponent<Collider>();
            Collider cr = (Collider)bulletRight.GetComponent<Collider>();
            cl.CollisionEvent.Attach(ml);
            cr.CollisionEvent.Attach(mr);
            
            //Adds the Objects to the gameObjects list in the Level
            LevelOne.AddObject(bulletLeft);
            LevelOne.AddObject(bulletRight);

            //Removes the original Fireball
            ToRemove = true;

        }

        /// <summary>
        /// This Method Constantly updates what the closest object is and applies the velocity to go towards them
        /// </summary>
        public void Homing()
        {
            if (homing == true)
            {
                if (Globals.FindClosestObject(LevelOne.gameObjects, GameObject.Transform.Position) != null)
                {
                    velocity = Globals.Direction(Globals.FindClosestObject(LevelOne.gameObjects, GameObject.Transform.Position).Transform.Position, GameObject.Transform.Position);
                }
                else
                {
                    return;
                }    
            }
        }


        /// <summary>
        /// Checks if the split bool is true then applies the Split method after half a second
        /// </summary>
        public void SplitCheck()
        {
            if (split == true)
            {  
                splitTimer += GameWorld.DeltaTime;
                if (splitTimer >= 0.5f)
                {
                    Split();
                    split = false;
                    splitTimer = 0;
                }
            }
        }

        /// <summary>
        /// Checks if the the WillHome bool is set to true and applies homing after half a second
        /// </summary>
        public void HomeCheck()
        {
            if (willHome == true)
            {
                homeTimer += GameWorld.DeltaTime;
                if (homeTimer >= 0.5f)
                {
                    homing = true;
                    willHome = false;
                }
            }
        }

        /// <summary>
        /// Notified is used to track GameEvents
        /// The Functionality of this Notify changes if the Fireball has Explode or not
        /// If it doesn't have explode it hits the target and gets removed
        /// if it has Explode it spawns an explosion and gets removed
        /// if it is an explosion it uses the Enemies damagedList to not multihit
        /// </summary>
        /// <param name="gameEvent"></param>
        public void Notify(GameEvent gameEvent)
        {
            
            if (gameEvent is CollisionEvent)
            {
                GameObject other = (gameEvent as CollisionEvent).Other;

                if (other.Tag == "Enemy")
                {
                    if (other.HasComponent<Grunt>())
                    {
                        if (explode == true)
                        {
                            Explode();
                        }
                        else
                        {
                            Grunt enemy = (Grunt)other.GetComponent<Grunt>();
                            if (hasExploded == true)
                            {
                                if(enemy.IsInDamagedList(this.GameObject) == false)
                                {
                                    enemy.TakeDamage(damage);
                                    enemy.AddToList(this.GameObject);
                                }
                            }
                            else
                            {
                                enemy.TakeDamage(damage);
                                ToRemove = true;
                            }
                            
                        }
                    }
                    else if (other.HasComponent<ArmoredGrunt>())
                    {
                        if (explode == true)
                        {
                            Explode();
                        }
                        else
                        {
                            ArmoredGrunt enemy = (ArmoredGrunt)other.GetComponent<ArmoredGrunt>();
                            if (hasExploded == true)
                            {
                                if (enemy.IsInDamagedList(this.GameObject) == false)
                                {
                                    enemy.TakeDamage(damage);
                                    enemy.AddToList(this.GameObject);
                                }
                            }
                            else
                            {
                                enemy.TakeDamage(damage);
                                ToRemove = true;
                            }
                        }
                    }
                    else if (other.HasComponent<Knight>())
                    {
                        if (explode == true)
                        {
                            Explode();
                        }
                        else
                        {
                            Knight enemy = (Knight)other.GetComponent<Knight>();
                            if (hasExploded == true)
                            {
                                if (enemy.IsInDamagedList(this.GameObject) == false)
                                {
                                    enemy.TakeDamage(damage);
                                    enemy.AddToList(this.GameObject);
                                }
                            }
                            else
                            {
                                enemy.TakeDamage(damage);
                                ToRemove = true;
                            }
                        }
                    }
                    else if (other.HasComponent<HorseRider>())
                    {
                        if (explode == true)
                        {
                            Explode();
                        }
                        else
                        {
                            HorseRider enemy = (HorseRider)other.GetComponent<HorseRider>();
                            if (hasExploded == true)
                            {
                                if (enemy.IsInDamagedList(this.GameObject) == false)
                                {
                                    enemy.TakeDamage(damage);
                                    enemy.AddToList(this.GameObject);
                                }
                            }
                            else
                            {
                                enemy.TakeDamage(damage);
                                ToRemove = true;
                            }
                        }
                    }
                    else if (other.HasComponent<Cleric>())
                    {
                        if (explode == true)
                        {
                            Explode();
                        }
                        else
                        {
                            Cleric enemy = (Cleric)other.GetComponent<Cleric>();
                            if (hasExploded == true)
                            {
                                if (enemy.IsInDamagedList(this.GameObject) == false)
                                {
                                    enemy.TakeDamage(damage);
                                    enemy.AddToList(this.GameObject);
                                }
                            }
                            else
                            {
                                enemy.TakeDamage(damage);
                                ToRemove = true;
                            }
                        }
                    }
                    else if (other.HasComponent<Paladin>())
                    {
                        if (explode == true)
                        {
                            Explode();
                        }
                        else
                        {
                            Paladin enemy = (Paladin)other.GetComponent<Paladin>();
                            if (hasExploded == true)
                            {
                                if (enemy.IsInDamagedList(this.GameObject) == false)
                                {
                                    enemy.TakeDamage(damage);
                                    enemy.AddToList(this.GameObject);
                                }
                            }
                            else
                            {
                                enemy.TakeDamage(damage);
                                ToRemove = true;
                            }
                        }
                    }
                    else if (other.HasComponent<Valkyrie>())
                    {
                        if (explode == true)
                        {
                            Explode();
                        }
                        else
                        {
                            Valkyrie enemy = (Valkyrie)other.GetComponent<Valkyrie>();
                            if (hasExploded == true)
                            {
                                if (enemy.IsInDamagedList(this.GameObject) == false)
                                {
                                    enemy.TakeDamage(damage);
                                    enemy.AddToList(this.GameObject);
                                }
                            }
                            else
                            {
                                enemy.TakeDamage(damage);
                                ToRemove = true;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// This method spawns an explosion at this objects position and then removes this object
        /// </summary>
        public void Explode()
        {
            if (explode == true)
            {
                LevelOne.AddObject(magicFac.Create(MagicLevel.Explosion, GameObject.Transform.Position));
                ToRemove = true;
            }
        }

    }
}
