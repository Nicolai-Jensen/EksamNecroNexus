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
        protected float rotation;

        //An animator component to access animations
        private Animator animator;
        private Vector2 velocity;

        public NecromancerMagic()
        {
            speed = 100f;
            velocity = Direction(ReturnPlayerPostition());
        }

        public override void Start()
        {
            GameObject.Tag = "NecroMagic";
        }

        public override void Update()
        {
            Move();
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
        protected Vector2 ReturnPlayerPostition()
        {
            foreach (GameObject go in LevelOne.gameObjects)
            {

                if (go is Necromancer)
                {
                    return go.Transform.Position;
                }
            }

            return new Vector2(GameObject.Transform.Position.X, -100);
        }
        protected Vector2 Direction(Vector2 playerPosition)
        {
            Vector2 direction;
            
            MouseState mouseState = Mouse.GetState();
            Vector2 mousePosition = mouseState.Position.ToVector2();

            //We get the correct direction by Subtracting the player Position with the Mouse Point, same goes for Rotation
            direction = mousePosition - playerPosition;
            direction.Normalize();
            rotation = (float)Math.Atan2(mousePosition.Y - GameObject.Transform.Position.Y, mousePosition.X - GameObject.Transform.Position.X) - 0.1f;

            return direction;
        }
    }
}
