using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NecroNexus
{

    public enum ArrowTier { Tier0, Tier1, Tier2, Tier3 }
    public class ArrowFactory : Factory
    {

        //Bliver ikke brugt
        public override GameObject Create(Enum type, Vector2 pos)
        {
            GameObject go = new GameObject();

            SpriteRenderer sr = (SpriteRenderer)go.AddComponent(new SpriteRenderer());

            Collider c = (Collider)go.AddComponent(new Collider());


            //switch (type)
            //{
            //    case ArrowTier.Tier0:
            //        sr.SetSprite("Projectiles/Arrows/tile002", 2f,0, 0.5f);
            //        go.AddComponent(new ArcherArrow(0,pos,new Vector2(200,200)));

            //        break;
            //    case ArrowTier.Tier1:
            //        sr.SetSprite("Projectiles/Arrows/tile002", 2f, Globals.GetRotation(pos), 0.5f);
            //        go.AddComponent(new ArcherArrow(1, pos, new Vector2(200, 200)));
            //        break;
            //    case ArrowTier.Tier2:
            //        sr.SetSprite("Projectiles/Arrows/tile005", 2f, Globals.GetRotation(pos), 0.5f);
            //        go.AddComponent(new ArcherArrow(2, pos, new Vector2(200, 200)));
            //        break;
            //    case ArrowTier.Tier3:
            //        sr.SetSprite("Projectiles/Arrows/tile006", 2f, Globals.GetRotation(pos), 0.5f);
            //        go.AddComponent(new ArcherArrow(3, pos, new Vector2(200, 200)));
            //        break;
            //}

            return go;
        }

        public GameObject Create(Enum type, Vector2 pos, Vector2 enemyPosition)
        {
            GameObject go = new GameObject();

            SpriteRenderer sr = (SpriteRenderer)go.AddComponent(new SpriteRenderer());

            Collider c;

            ArcherArrow a;


            switch (type)
            {
                case ArrowTier.Tier0:
                    sr.SetSprite("Projectiles/Arrows/tile002", 2f, Globals.GetRotationNoMouse(enemyPosition, pos), 0.5f);
                    c = (Collider)go.AddComponent(new Collider());
                    a = (ArcherArrow)go.AddComponent(new ArcherArrow(0, pos, Globals.Direction(enemyPosition, pos)));
                    c.CollisionEvent.Attach(a);
                    break;
                case ArrowTier.Tier1:
                    sr.SetSprite("Projectiles/Arrows/tile002", 2f, Globals.GetRotationNoMouse(enemyPosition , pos), 0.5f);
                    c = (Collider)go.AddComponent(new Collider());
                    a = (ArcherArrow)go.AddComponent(new ArcherArrow(1, pos, Globals.Direction(enemyPosition, pos)));
                    c.CollisionEvent.Attach(a);

                    break;
                case ArrowTier.Tier2:
                    sr.SetSprite("Projectiles/Arrows/tile005", 2f, Globals.GetRotationNoMouse(enemyPosition, pos), 0.5f);
                    c = (Collider)go.AddComponent(new Collider());
                    a = (ArcherArrow)go.AddComponent(new ArcherArrow(2, pos, Globals.Direction(enemyPosition, pos)));
                    c.CollisionEvent.Attach(a);

                    break;
                case ArrowTier.Tier3:
                    sr.SetSprite("Projectiles/Arrows/tile006", 2f, Globals.GetRotationNoMouse(enemyPosition, pos), 0.5f);
                    c = (Collider)go.AddComponent(new Collider());
                    a = (ArcherArrow)go.AddComponent(new ArcherArrow(3, pos, Globals.Direction(enemyPosition, pos)));
                    c.CollisionEvent.Attach(a);

                    break;
            }

            return go;
        }

        


    }
}
