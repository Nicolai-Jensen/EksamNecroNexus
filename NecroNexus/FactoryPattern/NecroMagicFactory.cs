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
        public override GameObject Create(Enum type)
        {
            GameObject go = new GameObject();

            SpriteRenderer sr = (SpriteRenderer)go.AddComponent(new SpriteRenderer());

            Collider c = (Collider)go.AddComponent(new Collider());
            c.Size1 = 2;
            c.Size2 = 2;
            c.Size3 = 1;
            c.Size4 = 1;

            switch (type)
            {
                case MagicLevel.BaseTier:
                    sr.SetSprite("placeholdersprites/EldenRingIcon", .5f, 0.5f);
                    go.AddComponent(new NecromancerMagic());
                    break;
            }

            return go;
        }
    }
}
