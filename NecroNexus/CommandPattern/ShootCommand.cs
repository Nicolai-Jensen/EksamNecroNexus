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
            player.ActivateMagicCast();
        }
    }
}
