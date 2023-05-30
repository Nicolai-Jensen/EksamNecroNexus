using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroNexus
{
    //--------------------------Nicolai Jensen----------------------------//
    /// <summary>
    /// An Interface used for executing Command Patterns
    /// </summary>
    public interface ICommand
    {
        void Execute(Necromancer player);
    }
}
