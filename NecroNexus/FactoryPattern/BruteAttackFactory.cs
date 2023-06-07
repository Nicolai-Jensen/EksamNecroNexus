using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NecroNexus
{
    //***********//KASPER KNUDSEN//***********//

    //Enumerations for the different tiers of arrows.
    public enum BruteAttackTier { Tier0, Tier1, Tier2, Tier3 }
    public class BruteAttackFactory : Factory
    {

        /// <summary>
        /// NOT BEING USED.
        /// This method is just here to satisfy the virtual method inside the factory class.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public override GameObject Create(Enum type, Vector2 pos)
        {
            GameObject go = new GameObject();
            return go;
        }


        /// <summary>
        /// The reason for this extra create method, is that I needed the enemyPosition.
        /// The method uses a switch case of enums, and then instanciates a gameobject, which is then given spriterenderer component, and a collider component.
        /// The collision gets attached, and so on for the rest of the tiers.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="pos"></param>
        /// <param name="enemyPosition"></param>
        /// <returns></returns>
        public GameObject Create(Enum type, Vector2 pos, Vector2 enemyPosition)
        {
            GameObject go = new GameObject();

            SpriteRenderer sr = (SpriteRenderer)go.AddComponent(new SpriteRenderer());

            Collider c;

            BruteAttack b;


            switch (type)
            {
                case BruteAttackTier.Tier0:
                    sr.SetSprite("Projectiles/Arrows/tile002", 2f, Globals.GetRotationNoMouse(enemyPosition, pos), 0.5f);
                    c = (Collider)go.AddComponent(new Collider());
                    b = (BruteAttack)go.AddComponent(new BruteAttack(0, pos, Globals.Direction(enemyPosition, pos)));
                    c.CollisionEvent.Attach(b);
                    break;
                case ArrowTier.Tier1:
                    sr.SetSprite("Projectiles/Arrows/tile002", 2f, Globals.GetRotationNoMouse(enemyPosition, pos), 0.5f);
                    c = (Collider)go.AddComponent(new Collider());
                    b = (BruteAttack)go.AddComponent(new BruteAttack(1, pos, Globals.Direction(enemyPosition, pos)));
                    c.CollisionEvent.Attach(b);

                    break;
                case ArrowTier.Tier2:
                    sr.SetSprite("Projectiles/Arrows/tile005", 2f, Globals.GetRotationNoMouse(enemyPosition, pos), 0.5f);
                    c = (Collider)go.AddComponent(new Collider());
                    b = (BruteAttack)go.AddComponent(new BruteAttack(2, pos, Globals.Direction(enemyPosition, pos)));
                    c.CollisionEvent.Attach(b);

                    break;
                case ArrowTier.Tier3:
                    sr.SetSprite("Projectiles/Arrows/tile006", 2f, Globals.GetRotationNoMouse(enemyPosition, pos), 0.5f);
                    c = (Collider)go.AddComponent(new Collider());
                    b = (BruteAttack)go.AddComponent(new BruteAttack(3, pos, Globals.Direction(enemyPosition, pos)));
                    c.CollisionEvent.Attach(b);

                    break;
            }

            return go;
        }




    }
}
