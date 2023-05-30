using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NecroNexus
{
    //--------------------------Nicolai----------------------------//

    /// <summary>
    /// The Globals Class is used as a place to set up useful Methods or information that all classes want to access.
    /// We didn't want to make GameWorld or the States that use it Singletons so this is our solution to getting access to stuff like content and Locks
    /// </summary>
    public static class Globals
    {
        public static ContentManager Content; //A parsed in Variable from GameWorld that will let Globals access the content variable in GameWorld
        public static readonly object lockObject = new object(); //A Lock since we use a thread, being in globals lets the AutoSave thread and normal game Thread access it from anyway. This is used to prevent any form of locks, Race Conditions or Reading problems


        /// <summary>
        /// A Method for getting access to the Necromancers Position when in the game.
        /// </summary>
        /// <returns></returns>
        public static Vector2 ReturnPlayerPosition()
        {
            //Checks the Levels List and finds the first object with the Necromancer componenet, Since there is only ever one Necromancer in the list it will never return the incorrect Position
            GameObject necroObject = LevelOne.gameObjects.FirstOrDefault(obj => obj.GetComponent<Necromancer>() != null);

            //This If statement protects against cases where you would end up having a null factor
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
            //Access a Vector2 Value using the Mouses position and the ToVector2() method
            MouseState mouseState = Mouse.GetState();
            Vector2 mousePosition = mouseState.Position.ToVector2();

            //Uses the X and Y Position of our 2 Vectors to get the directional value difference, except saving it in floats
            float dx = mousePosition.X - objectPosition.X;
            float dy = mousePosition.Y - objectPosition.Y;

            //Uses the Math.Atan2 Method to solve and get the correct rotation for the object
            float rotation = (float)Math.Atan2(dy, dx);

            //Flips the Objects rotation depending on which side of the objects original position the direction is heading
            if (dx < 0)
            {
                rotation += MathHelper.Pi;
            }

            //This Crossed out Kode is the first implementation of the rotation, we keep it cause it works better for sprites that aren't drawn left -> right, but instead up -> down or vice versa
            //float rotation = (float)Math.Atan2(mousePosition.Y - objectPosition.Y, mousePosition.X - objectPosition.X) + MathHelper.PiOver2;

            return rotation;
        }

        /// <summary>
        /// A simple Modified version of the above Method but instead uses 2 vectors instead of the Mouses Position
        /// </summary>
        /// <param name="targetPosition"></param>
        /// <param name="objectPosition"></param>
        /// <returns></returns>
        public static float GetRotationNoMouse(Vector2 targetPosition, Vector2 objectPosition)
        {
            float dx = targetPosition.X - objectPosition.X;
            float dy = targetPosition.Y - objectPosition.Y;

            float rotation = (float)Math.Atan2(dy, dx);

            return rotation;
        }

        /// <summary>
        /// A Method that we use to calculate the Directional Vector between 2 objects
        /// </summary>
        /// <param name="posTarget">The Targetted Position you want the the object to move to</param>
        /// <param name="yourPos">The Objects current Position you want it to move from</param>
        /// <returns></returns>
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
        /// This is used for the lvl2 and 3 NecroMagic in the game where the big fireball splits into 2 smaller ones that go in seperate directions in relation to the big ones direction
        /// </summary>
        /// <param name="angle">The angle you want to convert to vector</param>
        /// <returns></returns>
        public static Vector2 FromAngle(float angle)
        {
            return new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
        }


        /// <summary>
        /// A Method used to find the Closest object in a list to a predetermined postiion
        /// </summary>
        /// <param name="objects">A List that contains different GameObjects</param>
        /// <param name="position">An Anchor position that you want to use as a base for where to look from</param>
        /// <returns></returns>
        public static GameObject FindClosestObject(List<GameObject> objects, Vector2 position)
        {
            GameObject closestObj = null;
            float closestDistance = float.MaxValue; //We set float to max so that it can always be overwritten later

            foreach (GameObject obj in objects)
            {
                if (obj.Tag == "Enemy") //Uses the Tag to only look for Enemies, since the Summons are the ones using this for their range
                {
                    float distance = Vector2.Distance(position, obj.Transform.Position);
                    if (distance < closestDistance) //Checks if the distance of the Object is closer than the current saved closest Value
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
