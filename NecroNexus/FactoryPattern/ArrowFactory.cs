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

        public override GameObject Create(Enum type, Vector2 pos)
        {
            GameObject go = new GameObject();

            SpriteRenderer sr = (SpriteRenderer)go.AddComponent(new SpriteRenderer());

            Collider c = (Collider)go.AddComponent(new Collider());


            switch (type)
            {
                case ArrowTier.Tier0:
                    sr.SetSprite("Projectiles/Arrows/tile002", .1f, Globals.GetRotation(Globals.ReturnPlayerPosition()), 0.5f);
                    go.AddComponent(new NecromancerMagic(0));
                    break;
                case ArrowTier.Tier1:
                    sr.SetSprite("Projectiles/Arrows/tile002", .1f, Globals.GetRotation(Globals.ReturnPlayerPosition()), 0.5f);
                    go.AddComponent(new NecromancerMagic(1));
                    break;
                case ArrowTier.Tier2:
                    sr.SetSprite("Projectiles/Arrows/tile005", .3f, Globals.GetRotation(Globals.ReturnPlayerPosition()), 0.5f);
                    go.AddComponent(new NecromancerMagic(2));
                    break;
                case ArrowTier.Tier3:
                    sr.SetSprite("Projectiles/Arrows/tile006", .3f, Globals.GetRotation(Globals.ReturnPlayerPosition()), 0.5f);
                    go.AddComponent(new NecromancerMagic(3));
                    break;
            }

            return go;
        }

    }
}
