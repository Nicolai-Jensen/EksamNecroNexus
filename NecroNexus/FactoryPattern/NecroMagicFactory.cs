using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroNexus
{
    public enum MagicLevel { BaseTier, Tier1, Tier2, Tier3 }

    public class NecroMagicFactory : Factory
    {

        public override GameObject Create(Enum type, Vector2 pos)
        {
            GameObject go = new GameObject();

            SpriteRenderer sr = (SpriteRenderer)go.AddComponent(new SpriteRenderer());

            Collider c = (Collider)go.AddComponent(new Collider());
           

            switch (type)
            {
                case MagicLevel.BaseTier:
                    sr.SetSprite("placeholdersprites/EldenRingIcon", .1f, Globals.GetRotation(Globals.ReturnPlayerPosition()), 0.5f);
                    go.AddComponent(new NecromancerMagic(0));
                    break;
                case MagicLevel.Tier1:
                    sr.SetSprite("placeholdersprites/EldenRingIcon", .1f, Globals.GetRotation(Globals.ReturnPlayerPosition()), 0.5f);
                    go.AddComponent(new NecromancerMagic(1));
                    break;
                case MagicLevel.Tier2:
                    sr.SetSprite("placeholdersprites/EldenRingIcon", .3f, Globals.GetRotation(Globals.ReturnPlayerPosition()), 0.5f);
                    go.AddComponent(new NecromancerMagic(2));
                    break;
                case MagicLevel.Tier3:
                    sr.SetSprite("placeholdersprites/EldenRingIcon", .3f, Globals.GetRotation(Globals.ReturnPlayerPosition()), 0.5f);
                    go.AddComponent(new NecromancerMagic(3));
                    break;
            }

            return go;
        }
    }
}
