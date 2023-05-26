using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace NecroNexus
{
    public static class Globals
    {
        public static ContentManager Content;

        public static Vector2 ToVector2(this Point point)
        {
            return new Vector2(point.X, point.Y);
        }

        public static Vector2 ReturnPlayerPosition()
        {
            GameObject necroObject = LevelOne.gameObjects.FirstOrDefault(obj => obj.GetComponent<Necromancer>() != null);

            if (necroObject != null)
            {
                return necroObject.Transform.Position;
            }

            else
            {
                return new Vector2(GameWorld.ScreenSize.X / 2, GameWorld.ScreenSize.Y / 2);
            }

        }


        /// <summary>
        /// This method is specifically tied to the mouses position and gets a float value that can translate into the proper rotation from the object towards the mousePos
        /// </summary>
        /// <param name="objectPosition">The Anchor object you want the Rotation to be based from</param>
        /// <returns></returns>
        public static float GetRotation(Vector2 objectPosition)
        {
            MouseState mouseState = Mouse.GetState();
            Vector2 mousePosition = mouseState.Position.ToVector2();

            float dx = mousePosition.X - objectPosition.X;
            float dy = mousePosition.Y - objectPosition.Y;

            float rotation = (float)Math.Atan2(dy, dx);

            if (dx < 0)
            {
                rotation += MathHelper.Pi;
            }

            //float rotation = (float)Math.Atan2(mousePosition.Y - objectPosition.Y, mousePosition.X - objectPosition.X) + MathHelper.PiOver2;
            return rotation;
        }


        public static float GetRotationNoMouse(Vector2 targetPosition, Vector2 objectPosition)
        {
            

            float dx = targetPosition.X - objectPosition.X;
            float dy = targetPosition.Y - objectPosition.Y;

            float rotation = (float)Math.Atan2(dy, dx);

            if (dx < 0)
            {
                rotation += MathHelper.Pi;
            }

            //float rotation = (float)Math.Atan2(mousePosition.Y - objectPosition.Y, mousePosition.X - objectPosition.X) + MathHelper.PiOver2;
            return rotation;
        }


        public static Vector2 Direction(Vector2 posTarget, Vector2 yourPos)
        {
            //Makes a Vector2 variable
            Vector2 direction;

            //gives the Vector2 direction a value that when normalized can be used as velocity to go in the desired direction
            direction = posTarget - yourPos;
            direction.Normalize();

            

            return direction;
        }


        /// <summary>
        /// A Method that converts a float angle into a Vector2. Typically used to get a direction from an angle of an object
        /// </summary>
        /// <param name="angle">The angle you want to convert to vector</param>
        /// <returns></returns>
        public static Vector2 FromAngle(float angle)
        {
            return new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
        }

        public static GameObject FindClosestObject(List<GameObject> objects, Vector2 position)
        {
            GameObject closestObj = null;
            float closestDistance = float.MaxValue;

            foreach (GameObject obj in objects)
            {
                if (obj.Tag == "Enemy")
                {
                    float distance = Vector2.Distance(position, obj.Transform.Position);
                    if (distance < closestDistance)
                    {
                        closestObj = obj;
                        closestDistance = distance;
                    }
                }
            }

            return closestObj;
        }

    }
}
