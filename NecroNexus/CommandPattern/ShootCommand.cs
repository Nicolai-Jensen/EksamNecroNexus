using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroNexus
{
    public class ShootCommand : ICommand
    {
        public void Execute(Necromancer player)
        {
            int tier = player.Tier;
            switch (tier)
            {
                case 0:
                    player.Magic.Create(MagicLevel.BaseTier);
                    break;
                case 1:
                    player.Magic.Create(MagicLevel.Tier1);
                    break;
                case 2:
                    player.Magic.Create(MagicLevel.Tier2);
                    break;
                case 3:
                    player.Magic.Create(MagicLevel.Tier3);
                    break;
            }
            
        }
    }
}
