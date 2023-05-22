using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroNexus
{
    public class NecromancerMagic : Component
    {
        //Speed Value, used for velocity in the Move method
        private float speed;

        //An animator component to access animations
        private Animator animator;
        private Damage damage;
        private Vector2 velocity;
        private Vector2 startPosition;
        private int tier;
        private float timer;
        private bool homing = false;
        private bool split = false;
        private bool explode = false;
        private bool hasSplit = false;


        public override bool ToRemove { get; set; }

        public NecromancerMagic(int tier)
        {
            this.tier = tier;
            velocity = Direction(Globals.ReturnPlayerPosition());

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
                    break;
                case (3):
                    Tier3();
                    break;
            }
            speed = 800f;
            split = false;
        }

        public override void Start()
        {
            GameObject.Tag = "NecroMagic";

            if (hasSplit == false)
            {
                GameObject.Transform.Position = new Vector2(Globals.ReturnPlayerPosition().X, Globals.ReturnPlayerPosition().Y);
            }
            else if (hasSplit == true)
            {
                GameObject.Transform.Translate(startPosition);
            }
            
        }

        public override void Update()
        {
            Move();
            SplitCheck();
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

        protected Vector2 Direction(Vector2 playerPosition)
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
            damage = new Damage(DamageType.Magical, 2f);
            homing = true;
        }

        public void Tier2()
        {
            speed = 250f;
            damage = new Damage(DamageType.Magical, 3f);
            homing = true;
            split = true;
        }

        public void Tier3()
        {
            speed = 250f;
            damage = new Damage(DamageType.Magical, 4f);
            homing = true;
            split = true;
            explode = true;
        }

        public void Split()
        {
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

            GameObject bulletLeft = CreateOffSpring();
            GameObject bulletRight = CreateOffSpring();

            bulletLeft.AddComponent(new NecromancerMagic(tier, true, leftVelocity, GameObject.Transform.Position));
            bulletRight.AddComponent(new NecromancerMagic(tier, true, rightVelocity, GameObject.Transform.Position));
            LevelOne.AddObject(bulletLeft);
            LevelOne.AddObject(bulletRight);

            ToRemove = true;

        }

        public GameObject CreateOffSpring()
        {
            GameObject go = new GameObject();

            SpriteRenderer sr = (SpriteRenderer)go.AddComponent(new SpriteRenderer());

            Collider c = (Collider)go.AddComponent(new Collider());

            sr.SetSprite("placeholdersprites/EldenRingIcon", .1f, Globals.GetRotation(GameObject.Transform.Position), 0.5f);

            return go;
        }

        public void SplitCheck()
        {
            if (split == true)
            {  
                timer += GameWorld.DeltaTime;
                if (timer >= 0.5f)
                {
                    Split();
                    split = false;
                    timer = 0;
                }
            }
        }
    }
}
