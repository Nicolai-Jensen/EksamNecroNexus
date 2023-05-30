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
    public class NecromancerMagic : Component, IGameListener
    {
        //Speed Value, used for velocity in the Move method
        private float speed;
        //An animator component to access animations
        private NecroMagicFactory magicFac = new NecroMagicFactory();
        private Damage damage;
        private Animator animator;
        private SpriteRenderer sr;
        private Vector2 velocity;
        private Vector2 startPosition;
        private int tier;
        private float splitTimer;
        private float homeTimer;
        private bool homing = false;
        private bool split = false;
        private bool explode = false;
        private bool hasSplit = false;
        private bool hasExploded = false;
        private bool willHome = false;


        public override bool ToRemove { get; set; }

        public NecromancerMagic(int tier)
        {
            this.tier = tier;
            velocity = DirectionToMouse(Globals.ReturnPlayerPosition());

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
            split = false;
        }

        public NecromancerMagic(Vector2 velocity, Vector2 startPosition)
        {
            hasExploded = true;
            this.velocity = velocity;
            this.startPosition = startPosition;
            AudioEffect.playExplosion2();
            damage = new Damage(DamageType.Magical, 4f);
        }

        public override void Start()
        {
            GameObject.Tag = "NecroMagic";
            sr = GameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;
            if (hasSplit == false && hasExploded == false)
            {
                GameObject.Transform.Position = new Vector2(Globals.ReturnPlayerPosition().X, Globals.ReturnPlayerPosition().Y);
            }
            else if (hasSplit == true || hasExploded == true)
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

            if (sr.Sprite == Globals.Content.Load<Texture2D>("Necromancer/Magic/Explosion/1_23"))
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

        public void TierZero()
        {
            speed = 500f;
            damage = new Damage(DamageType.Magical, 1f);
        }

        public void Tier1()
        {
            speed = 700f;
            damage = new Damage(DamageType.Magical, 1.5f);
            homing = true;
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

        public void Split()
        {
            NecromancerMagic ml;
            NecromancerMagic mr;

            // Calculate the angle of the original velocity vector
            float originalAngle = (float)Math.Atan2(velocity.Y, velocity.X);

            // Define the deviation angle for the split bullets
            float deviationAngle = MathHelper.ToRadians(10f); // Adjust this angle as desired

            // Calculate the left and right angles based on the original angle and deviation angle
            float leftAngle = originalAngle + deviationAngle;
            float rightAngle = originalAngle - deviationAngle;

            // Convert the angles back to velocity vectors
            Vector2 leftVelocity = Globals.FromAngle(leftAngle) * velocity.Length();
            Vector2 rightVelocity = Globals.FromAngle(rightAngle) * velocity.Length();

            GameObject bulletLeft = magicFac.CreateOffSpring(GameObject.Transform.Position);
            GameObject bulletRight = magicFac.CreateOffSpring(GameObject.Transform.Position);

            ml = (NecromancerMagic)bulletLeft.AddComponent(new NecromancerMagic(tier, true, leftVelocity, GameObject.Transform.Position));
            mr = (NecromancerMagic)bulletRight.AddComponent(new NecromancerMagic(tier, true, rightVelocity, GameObject.Transform.Position));
            Collider cl = (Collider)bulletLeft.GetComponent<Collider>();
            Collider cr = (Collider)bulletRight.GetComponent<Collider>();
            cl.CollisionEvent.Attach(ml);
            cr.CollisionEvent.Attach(mr);

            LevelOne.AddObject(bulletLeft);
            LevelOne.AddObject(bulletRight);

            ToRemove = true;

        }

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
